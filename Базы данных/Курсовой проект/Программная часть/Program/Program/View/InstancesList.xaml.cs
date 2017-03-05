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
	public partial class InstancesList : Window
	{
		private List<Instance> instances;
		private Book parent;

		public InstancesList(Book book, bool isNew = false)
		{
			InitializeComponent();
			parent = book;

			if (!isNew)
			{
				instances = Instance.Get(parent);
			}

			instanceGrid.ItemsSource = instances;
		}

		private void instanceGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var hiddenColumns = new List<string>() { "Reader", "Book", "Objects", "Values" };

			var renamedColumns = new List<string[]>() { new string[] { "ID", "ИД" },
														new string[] { "Issue", "Взят" },
														new string[] { "Return", "Возвращён" },
														new string[] { "Available", "Доступен" }};

			var resizedColumns = new List<string[]>() { new string[] { "ID", "1" },
														new string[] { "Issue", "3" },
														new string[] { "Return", "3" },
														new string[] { "Available", "2" }};

			e.Modify(hiddenColumns, renamedColumns, resizedColumns);
		}

		private void instanceGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var vb = new Program.InstanceView(info: (Instance)instanceGrid.SelectedItem, infos: instances);
			if (vb.ShowDialog() == true)
			{
				instanceGrid.Items.Refresh();
			}
		}

		private void AddInstance_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.InstanceView(infos: instances, parentinfo: parent);
			if (vb.ShowDialog() == true)
			{
				instanceGrid.Items.Refresh();
			}
		}
	}
}
