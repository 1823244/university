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
	public partial class AuthorsList : Window
	{
		private bool dialogresult = false;

		public AuthorsList()
		{
			InitializeComponent();
			authorGrid.ItemsSource = Globals.Authors;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dialogresult) this.DialogResult = true;
		}

		private void authorGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var hiddenColumns = new List<string>() { "ID", "Objects", "Values" };

			var renamedColumns = new List<string[]>() { new string[] { "FirstName", "Имя" },
														new string[] { "MiddleName", "Отчество" },
														new string[] { "LastName", "Фамилия" },
														new string[] { "Birth", "День рождения" }};

			var resizedColumns = new List<string[]>() { new string[] { "FirstName", "1" },
														new string[] { "MiddleName", "1" },
														new string[] { "LastName", "1" },
														new string[] { "Birth", "1" }};

			e.Modify(hiddenColumns, renamedColumns, resizedColumns);
		}

		private void authorGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var vb = new Program.AuthorView((Author)authorGrid.SelectedItem);
			if (vb.ShowDialog() == true)
			{
				authorGrid.Items.Refresh();
				dialogresult = true;
			}
		}

		private void AddAuthor_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.AuthorView();
			if (vb.ShowDialog() == true)
			{
				authorGrid.Items.Refresh();
				dialogresult = true;
			}
		}
	}
}
