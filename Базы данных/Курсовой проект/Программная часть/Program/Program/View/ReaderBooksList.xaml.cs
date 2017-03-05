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
	public partial class ReaderBooksList : Window
	{
		private List<Instance> instances;
		private Reader reader;
		public ReaderBooksList(Reader info)
		{
			InitializeComponent();
			reader = info;
			instances = Instance.Get(reader);
			readerBooksGrid.ItemsSource = instances;
		}

		private void readerBooksGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var hiddenColumns = new List<string>() { "Reader", "Available", "Objects", "Values" };

			var renamedColumns = new List<string[]>() { new string[] { "ID", "ИД" },
														new string[] { "Book", "Книга" },
														new string[] { "Issue", "Дата выдачи" } };

			var resizedColumns = new List<string[]>() { new string[] { "ID", "1" },
														new string[] { "Book", "5" },
														new string[] { "Issue", "3" }};

			e.Modify(hiddenColumns, renamedColumns, resizedColumns);
		}

		private void readerBooksGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (MessageBox.Show("Книга сдана?", "Подтвердите действие", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				var selected = (Instance)readerBooksGrid.SelectedItem;

				if (!selected.Return())
				{
					MessageBox.Show("При сдаче книги произошла ошибка!");
				}
				else
				{
					instances.Remove(selected);
					readerBooksGrid.Items.Refresh();
				}
			}
		}

		private void AddInstance_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.BookList(instances, reader);
			if (vb.ShowDialog() == true)
			{
				readerBooksGrid.Items.Refresh();
			}
		}
	}
}
