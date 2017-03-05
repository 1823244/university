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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using Microsoft.Win32;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;

namespace Program
{
	public partial class MainWindow : System.Windows.Window
	{
		public static MainWindow Reference = null;
		
		public MainWindow()
		{
			Globals.db.Open();
			Globals.Sections = Section.Get();
			Globals.Subjects = Subject.Get();
			Globals.Authors = Author.Get();
			Globals.Books = Book.Get();
			Globals.Readers = Reader.Get();

			InitializeComponent();

			bookGrid.ItemsSource = Globals.Books;
			readerGrid.ItemsSource = Globals.Readers;

			Reference = this;
		}

		#region Book Grid

		private void bookGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var hiddenColumns = new List<string>() { "ID", "Amount", "Pages", "Popularity", "SubjectID", "Authors" };

			var renamedColumns = new List<string[]>() { new string[] { "Title", "Название" },
														new string[] { "Publisher", "Издательство" },
														new string[] { "Year", "Год" },
														new string[] { "Language", "Код языка" },
														new string[] { "Subject", "Жанр" }};

			var resizedColumns = new List<string[]>() { new string[] { "Title", "9" },
														new string[] { "Publisher", "4" },
														new string[] { "ISBN", "4" },
														new string[] { "Year", "2" },
														new string[] { "Language", "3" },
														new string[] { "Subject", "8" } };

			e.Modify(hiddenColumns, renamedColumns, resizedColumns);
		}

		private void bookGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var vb = new Program.BookView((Book)bookGrid.SelectedItem);
			if (vb.ShowDialog() == true)
			{
				bookGrid.Items.Refresh();
			}
		}

		private void AddBook_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.BookView();
			if (vb.ShowDialog() == true)
			{
				bookGrid.Items.Refresh();
			}
		}

		#endregion

		#region Reader Grid

		private void readerGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			var hiddenColumns = new List<string>() { "ID", "Birth", "Phone", "Passport", "FormatedPassport", "Sections", "Objects", "Values" };

			var renamedColumns = new List<string[]>() { new string[] { "FirstName", "Имя" },
														new string[] { "MiddleName", "Отчество" },
														new string[] { "LastName", "Фамилия" },
														new string[] { "FormatedPhone", "Телефон" },
														new string[] { "Group", "Группа" }};

			var resizedColumns = new List<string[]>() { new string[] { "FirstName", "3" },
														new string[] { "MiddleName", "3" },
														new string[] { "LastName", "3" },
														new string[] { "FormatedPhone", "2" },
														new string[] { "Group", "1" } };

			e.Modify(hiddenColumns, renamedColumns, resizedColumns);
		}

		private void readerGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var vb = new Program.ReaderView((Reader)readerGrid.SelectedItem);
			if (vb.ShowDialog() == true)
			{
				readerGrid.Items.Refresh();
			}
		}

		private void AddReader_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.ReaderView();
			if (vb.ShowDialog() == true)
			{
				readerGrid.Items.Refresh();
			}
		}

		#endregion

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			var currentTab = ((TabItem)globalTable.SelectedItem).Name;

			if (currentTab == "bookTab")
			{
				foreach (var item in bookGrid.SelectedItems)
				{
					var b = (Book)item;
					if (b.Delete())
					{
						Globals.Books.Remove(b);
					}
				}

				bookGrid.Items.Refresh();
			}
			else if (currentTab == "readerTab")
			{
				foreach (var item in readerGrid.SelectedItems)
				{
					var r = (Reader)item;
					if (r.Delete())
					{
						Globals.Readers.Remove(r);
					}
				}

				readerGrid.Items.Refresh();
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Globals.db.Close();
		}

		private void ExportXML_Click(object sender, RoutedEventArgs e)
		{
			String path = "";

			SaveFileDialog sfd = new SaveFileDialog();
			sfd.InitialDirectory = "C:\\Users\\Dante\\Desktop";
			sfd.Filter = "Файлы XML|*.xml";
			if (sfd.ShowDialog() == true)
			{
				path = sfd.FileName;
			}
			else
			{
				return;
			}

			try
			{
				SerializeClass.Export(path);

				MessageBox.Show("Экспорт завершён успешно!");
			}
			catch (Exception ee)
			{
				MessageBox.Show(ee.Message);
			}
		}

		private void ImportXML_Click(object sender, RoutedEventArgs e)
		{
			String path = "";
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.InitialDirectory = "C:\\Users\\Dante\\Desktop";
			ofd.Filter = "Файлы XML|*.xml";

			if (ofd.ShowDialog() == true)
			{
				path = ofd.FileName;
			}
			else
			{
				return;
			}

			try
			{
				SerializeClass.Import(path);
				bookGrid.ItemsSource = Globals.Books;
				bookGrid.Items.Refresh();
				readerGrid.ItemsSource = Globals.Readers;
				readerGrid.Items.Refresh();

				MessageBox.Show("Импорт завершён успешно!");
			}
			catch (Exception ee)
			{
				MessageBox.Show(ee.Message);
			}
		}

		private void ExportXLS_Click(object sender, RoutedEventArgs e)
		{
			var path = "";
			var sfd = new SaveFileDialog();
			sfd.InitialDirectory = "C:\\Users\\Dante\\Desktop";
			sfd.Filter = "Файлы XSL|*.xsl";

			if (sfd.ShowDialog() == true)
			{
				path = sfd.FileName;
			}
			else
			{
				return;
			}

			var dg = new DataGrid();

			var currentTab = ((TabItem)globalTable.SelectedItem).Name;

			if (currentTab == "bookTab")
			{
				dg = bookGrid;
			}
			else if (currentTab == "readerTab")
			{
				dg = readerGrid;
			}

			dg.SelectAllCells();
			dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
			ApplicationCommands.Copy.Execute(null, dg);
			var res = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
			var result = (string)Clipboard.GetData(DataFormats.UnicodeText);
			dg.UnselectAllCells();

			var file = new StreamWriter(path, false, Encoding.Unicode);
			file.WriteLine(result.Replace(',', ' '));
			file.Close();

			MessageBox.Show("Экспорт завершён успешно!");
		}

		private void ExportDOC_Click(object sender, RoutedEventArgs e)
		{
			var path = "";
			var sfd = new SaveFileDialog();
			sfd.InitialDirectory = "C:\\Users\\Dante\\Desktop";
			sfd.Filter = "Файлы DOC|*.doc";

			if (sfd.ShowDialog() == true)
			{
				path = sfd.FileName;
			}
			else
			{
				return;
			}

			Word.Application wrdApp;
			Word._Document wrdDoc;
			Object oMissing = System.Reflection.Missing.Value;
			Object oFalse = false;
			var dg = new DataGrid();

			var currentTab = ((TabItem)globalTable.SelectedItem).Name;

			if (currentTab == "bookTab")
			{
				dg = bookGrid;
			}
			else if (currentTab == "readerTab")
			{
				dg = readerGrid;
			}

			Word.Selection wrdSelection;
			Word.Table wrdTable;

			wrdApp = new Word.Application();
			wrdApp.Visible = false;

			wrdDoc = wrdApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
			wrdDoc.Select();
			wrdSelection = wrdApp.Selection;

			wrdDoc.PageSetup.BottomMargin = wrdDoc.PageSetup.LeftMargin = wrdDoc.PageSetup.RightMargin =
				wrdDoc.PageSetup.TopMargin = 50;
			wrdDoc.PageSetup.Orientation = WdOrientation.wdOrientLandscape;

			var StrToAdd = "Отчёт научной библиотеки БГТУ им. В. Г. Шухова";
			wrdSelection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
			wrdSelection.Font.Size = 22;
			wrdSelection.Font.Name = "PT Serif Caption";
			wrdSelection.TypeText(StrToAdd);

			for (int iCount = 1; iCount <= 1; iCount++)
			{
				wrdApp.Selection.TypeParagraph();
			}
			wrdSelection.Font.Name = "PT Sans";
			wrdSelection.Font.Size = 12;
			wrdSelection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

			wrdTable = wrdDoc.Tables.Add(wrdSelection.Range, dg.Items.Count + 1, dg.Columns.Count, ref oMissing, ref oMissing);
			wrdTable.Columns.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthAuto;
			wrdTable.set_Style(WdBuiltinStyle.wdStyleTableLightListAccent1);


			for (int i = 0; i < dg.Columns.Count; i++)
			{
				wrdDoc.Tables[1].Cell(1, i + 1).Range.InsertAfter(dg.Columns[i].Header.ToString());
			}

			for (int i = 0; i < dg.Items.Count; i++)
			{
				for (int j = 0; j < dg.Columns.Count; j++)
				{
					var el = dg.Columns[j].GetCellContent(dg.Items[i]);
					var str = "";
					if (el is System.Windows.Controls.CheckBox)
					{
						str = (el as System.Windows.Controls.CheckBox).IsChecked.ToString();
					}
					else if (el is TextBlock)
					{
						str = (el as TextBlock).Text;
					}
					wrdDoc.Tables[1].Cell(i + 2, j + 1).Range.InsertAfter(str);
					if (j == 0) wrdDoc.Tables[1].Cell(i + 2, j + 1).Range.Bold = 0;
				}
			}

			foreach (Microsoft.Office.Interop.Word.Section wordSection in wrdApp.ActiveDocument.Sections)
			{
				Word.Range footerRange = wordSection.Footers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
				footerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
				footerRange.Font.Size = 14;
				footerRange.Text = "БГТУ им. В. Г. Шухова";
			}

			wrdDoc.Saved = true;

			object fileName = path;
			wrdDoc.SaveAs2(ref fileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
							ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
			wrdDoc.Close(ref oFalse, ref oMissing, ref oMissing);
			wrdApp.Quit(ref oFalse, ref oMissing, ref oMissing);

			wrdSelection = null;
			wrdDoc = null;
			wrdApp = null;

			MessageBox.Show("Экспорт завершён успешно!");
		}
	}
}
