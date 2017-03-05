using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Program
{
	public partial class SubjectView : Window
	{
		private bool isNew;
		private Subject element;
		private Subject dump;
		private bool dialogresult = false;
		private bool modified = false;

		public SubjectView(Subject info = null)
		{
			InitializeComponent();

			Section.ItemsSource = Globals.Sections;
			element = info;
			isNew = element == null;

			if (!isNew)
			{
				dump = Subject.GetCopy(element);

				element.ChangeView(ID);
			}
			else
			{
				element = new Subject();

				Globals.HideViewElements(IDLabel, Delete);
			}

			element.BindView(Title);
			Section.BindSelectedItem(element, vrule: new NotNullRule());
		}

		private void Complete_Click(object sender, RoutedEventArgs e)
		{
			Globals.UpdateSources(Title, Section);

			if (!(sender as Button).IsEnabled) return;

			modified = true;

			if (!isNew)
			{
				if (!element.Update())
				{
					MessageBox.Show("При обновлении жанра «" + element.ToString() + "» произошла ошибка!");
				}
			}
			else
			{
				if (!element.Insert())
				{
					MessageBox.Show("При добавлении нового жанра произошла ошибка!");
				}
				else
				{
					Globals.Subjects.Add(element);
					dialogresult = true;
				}
			}

			this.Close();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (!element.Delete())
			{
				MessageBox.Show("При удалении жанра «" + element.ToString() + "» произошла ошибка!");
			}
			else
			{
				Globals.Subjects.Remove(element);
				dialogresult = true;
			}
			
			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dialogresult) this.DialogResult = true;

			if (!isNew && !modified)
			{
				dump.ChangeView(Title, Section);
			}
		}

		private void EditSections_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.SectionsList();
			if (vb.ShowDialog() == true)
			{
				Section.Items.Refresh();
				dialogresult = true;
			}
		}
	}
}
