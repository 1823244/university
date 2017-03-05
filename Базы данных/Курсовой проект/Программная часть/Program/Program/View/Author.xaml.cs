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
	public partial class AuthorView : Window
	{
		private bool isNew;
		private Author element;
		private Author dump;
		private bool dialogresult = false;
		private bool modified = false;

		public AuthorView(Author info = null)
		{
			InitializeComponent();
			
			element = info;
			isNew = element == null;

			if (!isNew)
			{
				dump = Author.GetCopy(element);

				element.ChangeView(ID);
			}
			else
			{
				element = new Author();
				
				Globals.HideViewElements(IDLabel, Delete);
			}

			element.BindView(FirstName, MiddleName, LastName, Birth);
		}

		private void Complete_Click(object sender, RoutedEventArgs e)
		{
			Globals.UpdateSources(FirstName, MiddleName, LastName, Birth);

			if (!(sender as Button).IsEnabled) return;

			modified = true;

			if (!isNew)
			{
				if (!element.Update())
				{
					MessageBox.Show("При обновлении автора «" + element.ToString() + "» произошла ошибка!");
				}
			}
			else
			{
				if (!element.Insert())
				{
					MessageBox.Show("При добавлении нового автора произошла ошибка!");
				}
				else
				{
					Globals.Authors.Add(element);
					dialogresult = true;
				}
			}

			this.Close();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (!element.Delete())
			{
				MessageBox.Show("При удалении автора «" + element.ToString() + "» произошла ошибка!");
			}
			else
			{
				Globals.Authors.Remove(element);
				dialogresult = true;
			}

			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dialogresult) this.DialogResult = true;

			if (!isNew && !modified)
			{
				dump.ChangeView(FirstName, MiddleName, LastName, Birth);
			}
		}
	}
}
