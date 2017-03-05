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
	public partial class SectionView : Window
	{
		private bool isNew;
		private Section element;
		private Section dump;
		private bool dialogresult = false;
		private bool modified = false;

		public SectionView(Section info = null)
		{
			InitializeComponent();

			element = info;
			isNew = element == null;

			if (!isNew)
			{
				dump = Section.GetCopy(element);

				element.ChangeView(ID);
			}
			else
			{
				element = new Section();

				Globals.HideViewElements(IDLabel, Delete);
			}

			element.BindView(Title, Location);
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (!element.Delete())
			{
				MessageBox.Show("При удалении зала «" + element.ToString() + "» произошла ошибка!");
			}
			else
			{
				Globals.Sections.Remove(element);
				dialogresult = true;
			}

			this.Close();
		}

		private void Complete_Click(object sender, RoutedEventArgs e)
		{
			Globals.UpdateSources(Title, Location);

			if (!(sender as Button).IsEnabled) return;

			modified = true;

			if (!isNew)
			{
				if (!element.Update())
				{
					MessageBox.Show("При обновлении зала «" + element.ToString() + "» произошла ошибка!");
				}
			}
			else
			{
				if (!element.Insert())
				{
					MessageBox.Show("При добавлении нового зала произошла ошибка!");
				}
				else
				{
					Globals.Sections.Add(element);
					dialogresult = true;
				}
			}

			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dialogresult) this.DialogResult = true;

			if (!isNew && !modified)
			{
				dump.ChangeView(Title, Location);
			}
		}
	}
}
