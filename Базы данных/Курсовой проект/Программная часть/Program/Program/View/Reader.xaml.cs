using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Program
{
	public partial class ReaderView : Window
	{
		private bool isNew;
		private Reader element;
		private Reader dump;
		private SolidColorBrush red = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DC0E0E"));
		private SolidColorBrush gray = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ACACAC"));
		private ListBox LBoxinCBox;
		private ToggleButton TButtoninCBox;
		private ToolTip sectionsError = new ToolTip { Content = "Читатель должен иметь доступ хотя бы к одному залу." };
		private bool modified = false;
		private bool dialogresult = false;

		public ReaderView(Reader info = null)
		{
			InitializeComponent();
			
			element = info;
			isNew = element == null;

			if (!isNew)
			{
				dump = Reader.GetCopy(element);

				element.ChangeView(ID);
			}
			else
			{
				element = new Reader();

				Globals.HideViewElements(IDLabel, Books);
			}

			element.BindView(FirstName, MiddleName, LastName, FormatedPhone, FormatedPassport, Group, Birth);
		}

		private void Complete_Click(object sender, RoutedEventArgs e)
		{
			Globals.UpdateSources(FirstName, MiddleName, LastName, FormatedPhone, FormatedPassport, Group, Birth);

			if (LBoxinCBox.SelectedItems.Count == 0)
			{
				Sections.BorderBrush = red;
				Sections.ToolTip = sectionsError;

				return;
			}

			if (!(sender as Button).IsEnabled) return;

			modified = true;

			var sections = new List<Section>();
			foreach (var item in LBoxinCBox.SelectedItems)
			{
				sections.Add((Section)item);
			}

			element.Sections = sections;

			if (!isNew)
			{
				if (!element.Update())
				{
					MessageBox.Show("При обновлении читателя «" + element.ToString() + "» произошла ошибка!");
				}
			}
			else
			{
				if (!element.Insert())
				{
					MessageBox.Show("При добавлении нового читателя произошла ошибка!");
				}
				else
				{
					Globals.Readers.Add(element);
					dialogresult = true;
				}
			}

			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dialogresult) this.DialogResult = true;

			if (!isNew && !modified)
			{
				dump.ChangeView(FirstName, MiddleName, LastName, Birth, FormatedPhone, FormatedPassport, Group);

				LBoxinCBox.SelectedItems.Clear();
				foreach (Section section in dump.Sections)
				{
					LBoxinCBox.SelectedItems.Add(Globals.Sections.First(x => x.ID == section.ID));
				}
			}
		}

		private void Sections_Loaded(object sender, RoutedEventArgs e)
		{
			Sections.ItemsSource = Globals.Sections;
			LBoxinCBox = ((ListBox)Sections.Template.FindName("lstBox", Sections));
			TButtoninCBox = ((ToggleButton)Sections.Template.FindName("tgButton", Sections));

			if (!isNew)
			{
				foreach (Section section in element.Sections)
				{
					LBoxinCBox.SelectedItems.Add(Globals.Sections.First(x => x.ID == section.ID));
				}
			}
		}

		private void Sections_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (LBoxinCBox.SelectedItems.Count == 0)
			{
				Sections.BorderBrush = red;
				Sections.ToolTip = sectionsError;
			}
			else
			{
				Sections.BorderBrush = gray;
				Sections.ToolTip = null;
			}
		}

		private void Books_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.ReaderBooksList(element);
			vb.ShowDialog();
		}
	}
}
