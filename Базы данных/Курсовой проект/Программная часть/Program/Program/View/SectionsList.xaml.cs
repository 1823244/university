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
	public partial class SectionsList : Window
	{
		private bool dialogresult = false;

		public SectionsList()
		{
			InitializeComponent();
			sectionGrid.ItemsSource = Globals.Sections;
		}

		private void sectionGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var hiddenColumns = new List<string>() { "ID", "Objects", "Values" };

			var renamedColumns = new List<string[]>() { new string[] { "Title", "Название" },
														new string[] { "Location", "Расположение" }};

			var resizedColumns = new List<string[]>() { new string[] { "Title", "2" },
														new string[] { "Location", "1" }};

			e.Modify(hiddenColumns, renamedColumns, resizedColumns);
		}

		private void sectionGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var vb = new Program.SectionView((Section)sectionGrid.SelectedItem);
			if (vb.ShowDialog() == true)
			{
				sectionGrid.Items.Refresh();
				dialogresult = true;
			}
		}

		private void AddSection_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.SectionView();
			if (vb.ShowDialog() == true)
			{
				sectionGrid.Items.Refresh();
				dialogresult = true;
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dialogresult) this.DialogResult = true;
		}
	}
}
