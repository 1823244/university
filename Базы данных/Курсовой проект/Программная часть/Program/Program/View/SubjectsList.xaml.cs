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
	public partial class SubjectsList : Window
	{
		private bool dialogresult = false;

		public SubjectsList()
		{
			InitializeComponent();
			subjectGrid.ItemsSource = Globals.Subjects;
		}

		private void subjectGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var hiddenColumns = new List<string>() { "ID", "Objects", "Values" };

			var renamedColumns = new List<string[]>() { new string[] { "Title", "Название" },
														new string[] { "Section", "Расположение" }};

			var resizedColumns = new List<string[]>() { new string[] { "Title", "1" },
														new string[] { "Section", "1" }};

			e.Modify(hiddenColumns, renamedColumns, resizedColumns);
		}

		private void subjectGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var vb = new Program.SubjectView((Subject)subjectGrid.SelectedItem);
			if (vb.ShowDialog() == true)
			{
				subjectGrid.Items.Refresh();
				dialogresult = true;
			}
		}

		private void AddSubject_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.SubjectView();
			if (vb.ShowDialog() == true)
			{
				subjectGrid.Items.Refresh();
				dialogresult = true;
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dialogresult) this.DialogResult = true;
		}
	}
}
