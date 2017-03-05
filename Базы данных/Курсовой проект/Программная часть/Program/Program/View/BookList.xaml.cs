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
	public partial class BookList : Window
	{
		private List<Book> books;
		private Reader reader;
		private List<Instance> instances;
		private bool dialogresult = false;

		public BookList(List<Instance> infos, Reader r)
		{
			InitializeComponent();
			reader = r;
			instances = infos;
			books = Globals.Books.FindAll(x => x.Amount > 0);
			bookGrid.ItemsSource = books;
		}

		private void bookGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var hiddenColumns = new List<string>() { "ID", "ISBN", "Language", "Pages", "Popularity", "Subject", "SubjectID", "Authors" };

			var renamedColumns = new List<string[]>() { new string[] { "Title", "Название" },
														new string[] { "Publisher", "Издательство" },
														new string[] { "Year", "Год" },
														new string[] { "Amount", "Кол-во" }};

			var resizedColumns = new List<string[]>() { new string[] { "Title", "9" },
														new string[] { "Publisher", "4" },
														new string[] { "ISBN", "4" },
														new string[] { "Year", "2" },
														new string[] { "Amount", "3" } };

			e.Modify(hiddenColumns, renamedColumns, resizedColumns);
		}

		private void bookGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (MessageBox.Show("Выдать книгу?", "Подтвердите действие", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				var selected = (Book)bookGrid.SelectedItem;
				var instance = Instance.Get(selected).Find(x => x.Available == true);
				
				instance.Reader = reader;

				if (!instance.Give())
				{
					MessageBox.Show("При выдаче книги произошла ошибка!");
				}
				else
				{
					instances.Add(instance);
					if (selected.Amount == 0)
					{
						books.Remove(selected);
					}
					bookGrid.Items.Refresh();
					dialogresult = true;
				}
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dialogresult) this.DialogResult = true;
		}
	}
}
