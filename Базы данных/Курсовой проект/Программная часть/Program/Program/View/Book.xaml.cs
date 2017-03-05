using System;
using System.Collections.Generic;
using System.Globalization;
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
	public partial class BookView : Window
	{
		private bool isNew;
		private Book element;
		private Book dump;
		private bool modified = false;
		private SolidColorBrush red = (SolidColorBrush)(new BrushConverter().ConvertFrom("#DC0E0E"));
		private SolidColorBrush gray = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ACACAC"));
		private ListBox LBoxinCBox;
		private ToggleButton TButtoninCBox;
		private ToolTip authorsError = new ToolTip { Content = "У книги должен быть хотя бы один автор." };
		private bool dialogresult = false;

		public BookView(Book info = null)
		{
			InitializeComponent();

			element = info;
			Subject.ItemsSource = Globals.Subjects;
			isNew = element == null;

			if (!isNew)
			{
				dump = Book.GetCopy(element);

				element.ChangeView(ID, Amount);
			}
			else
			{
				element = new Book();

				Globals.HideViewElements(IDLabel, AmountLabel);
			}

			element.BindView(Title, Publisher, Year, Pages, ISBN, Language);
			Subject.BindSelectedItem(element, vrule: new NotNullRule());
		}

		private void SetRu_Click(object sender, RoutedEventArgs e)
		{
			Language.Text = "ru_RU";
		}

		private void SetEn_Click(object sender, RoutedEventArgs e)
		{
			Language.Text = "en_GB";
		}

		private void Complete_Click(object sender, RoutedEventArgs e)
		{
			Globals.UpdateSources(Title, Publisher, Year, Pages, ISBN, Language, Subject);
			
			if (LBoxinCBox.SelectedItems.Count == 0)
			{
				Authors.BorderBrush = red;
				Authors.ToolTip = authorsError;

				return;
			}

			if (!(sender as Button).IsEnabled) return;

			modified = true;

			var authors = new List<Author>();
			foreach (var item in LBoxinCBox.SelectedItems)
			{
				authors.Add((Author)item);
			}

			element.Authors = authors;

			if (!isNew)
			{
				if (!element.Update())
				{
					MessageBox.Show("При обновлении книги «" + element.ToString() + "» произошла ошибка!");
				}
			}
			else
			{
				if (!element.Insert())
				{
					MessageBox.Show("При добавлении новой книги произошла ошибка!");
				}
				else
				{
					Globals.Books.Add(element);
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
				dump.ChangeView(Title, Publisher, Year, Pages, ISBN, Language, Subject);

				LBoxinCBox.SelectedItems.Clear();
				foreach (Author author in dump.Authors)
				{
					LBoxinCBox.SelectedItems.Add(Globals.Authors.First(x => x.ID == author.ID));
				}
			}
		}

		private void Authors_Loaded(object sender, RoutedEventArgs e)
		{
			Authors.ItemsSource = Globals.Authors;

			LBoxinCBox = ((ListBox)Authors.Template.FindName("lstBox", Authors));
			TButtoninCBox = ((ToggleButton)Authors.Template.FindName("tgButton", Authors));

			if (!isNew)
			{
				foreach (Author author in element.Authors)
				{
					LBoxinCBox.SelectedItems.Add(Globals.Authors.First(x => x.ID == author.ID));
				}
			}
		}

		private void Authors_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (LBoxinCBox.SelectedItems.Count == 0)
			{
				Authors.BorderBrush = red;
				Authors.ToolTip = authorsError;
			}
			else
			{
				Authors.BorderBrush = gray;
				Authors.ToolTip = null;
			}
		}

		private void EditSubjects_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.SubjectsList();
			if (vb.ShowDialog() == true)
			{
				Subject.Items.Refresh();
				dialogresult = true;
			}
		}

		private void EditAuthors_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.AuthorsList();
			if (vb.ShowDialog() == true)
			{
				Authors.Items.Refresh();
				dialogresult = true;
			}
		}

		private void Instances_Click(object sender, RoutedEventArgs e)
		{
			InstancesList vb;
			vb = new Program.InstancesList(element, isNew);
			vb.ShowDialog();
		}
	}
}
