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
	public partial class InstanceView : Window
	{
		private bool isNew;
		private Instance element;
		private List<Instance> elements;
		private Instance dump;
		private Book book;
		private bool dialogresult = false;
		private bool modified = false;

		public InstanceView(Instance info = null, List<Instance> infos = null, Book parentinfo = null)
		{
			InitializeComponent();

			element = info;
			elements = infos;
			isNew = element == null;

			if (!isNew)
			{
				dump = Instance.GetCopy(element);

				element.ChangeView(ID);
			}
			else
			{
				book = parentinfo;
				element = new Instance();

				Globals.HideViewElements(IDLabel, Delete, Reader, ReaderView);
			}

			element.BindView(Issue, Return, Available, Reader);
		}

		private void Complete_Click(object sender, RoutedEventArgs e)
		{
			Globals.UpdateSources(Issue, Return, Available, Reader);

			if (!(sender as Button).IsEnabled) return;

			modified = true;

			if (!isNew)
			{
				if (!element.Update())
				{
					MessageBox.Show("При обновлении экземпляра #" + element.ToString() + " произошла ошибка!");
				}
			}
			else
			{
				element.Book = book;
				if (!element.Insert())
				{
					MessageBox.Show("При добавлении нового экземпляра произошла ошибка!");
				}
				else
				{
					elements.Add(element);
					dialogresult = true;
				}
			}

			this.Close();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (!element.Delete())
			{
				MessageBox.Show("При удалении экземпляра #" + element.ToString() + " произошла ошибка!");
			}
			else
			{
				elements.Remove(element);
				dialogresult = true;
			}

			this.Close();
		}

		private void Reader_Click(object sender, RoutedEventArgs e)
		{
			var vb = new Program.ReaderView((Reader)element.Reader);
			if (vb.ShowDialog() == true)
			{
				Globals.UpdateSources(Reader);
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dialogresult) this.DialogResult = true;

			if (!isNew && !modified)
			{
				dump.ChangeView(Issue, Return, Available);
			}
		}
	}
}
