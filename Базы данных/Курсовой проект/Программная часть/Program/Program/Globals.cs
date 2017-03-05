using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Program
{
	class Globals
	{
		public static DB db = new DB();

		public static List<Subject> Subjects { get; set; }
		public static List<Section> Sections { get; set; }
		public static List<Author> Authors { get; set; }
		public static List<Book> Books { get; set; }
		public static List<Reader> Readers { get; set; }

		public static Int16? ToNullableInt16(object n)
		{
			try
			{
				return Convert.ToInt16(n);
			}
			catch
			{
				return null;
			}
		}

		public static Int32? ToNullableInt32(object n)
		{
			try
			{
				return Convert.ToInt32(n);
			}
			catch
			{
				return null;
			}
		}

		public static Int64? ToNullableInt64(object n)
		{
			try
			{
				return Convert.ToInt64(n);
			}
			catch
			{
				return null;
			}
		}

		public static String ToString(Int16? n)
		{
			string result = n.ToString();
			return (result == null || result == "") ? "NULL" : result;
		}

		public static String ToString(int? n)
		{
			string result = n.ToString();
			return (result == null || result == "") ? "NULL" : result;
		}

		public static void HideViewElements(params object[] os)
		{
			var l = new Label();
			var b = new Button();

			foreach (var o in os)
			{
				if (o.GetType() == l.GetType())
				{
					((Label)o).Visibility = Visibility.Hidden;
				}

				if (o.GetType() == b.GetType())
				{
					((Button)o).Visibility = Visibility.Hidden;
				}
			}
		}

		public static void UpdateSources(params object[] os)
		{
			var tb = new TextBox();
			var cb = new CheckBox();
			var l = new Label();
			var dp = new DatePicker();
			var cmb = new ComboBox();

			foreach (var o in os)
			{
				if (o.GetType() == tb.GetType())
				{
					((TextBox)o).GetBindingExpression(TextBox.TextProperty).UpdateSource();
				}

				if (o.GetType() == cb.GetType())
				{
					((CheckBox)o).GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
				}

				if (o.GetType() == l.GetType())
				{
					((Label)o).GetBindingExpression(Label.ContentProperty).UpdateSource();
				}

				if (o.GetType() == dp.GetType())
				{
					((DatePicker)o).GetBindingExpression(DatePicker.TextProperty).UpdateSource();
				}

				if (o.GetType() == cmb.GetType())
				{
					((ComboBox)o).GetBindingExpression(ComboBox.SelectedItemProperty).UpdateSource();
				}
			}
		}
	}

	public static class ExtensionClass
	{
		public static void Bind(this TextBox tb,
								object source,
								string path = null,
								ValidationRule vrule = null,
								List<ValidationRule> rules = null,
								BindingMode mode = BindingMode.TwoWay,
								UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged,
								bool validatesonexceptions = true)
		{
			var b = new Binding();
			b.Source = source;

			if (path != null)
			{
				b.Path = new PropertyPath(path);
			}
			else
			{
				b.Path = new PropertyPath(tb.Name);
			}

			b.Mode = mode;
			b.UpdateSourceTrigger = trigger;
			b.ValidatesOnExceptions = validatesonexceptions;
			
			if (vrule != null)
			{
				b.ValidationRules.Add(vrule);
			}

			if (rules != null)
			{
				foreach (ValidationRule rule in rules)
				{
					b.ValidationRules.Add(rule);
				}
			}
			
			tb.SetBinding(TextBox.TextProperty, b);
		}

		public static void Bind(this CheckBox cb,
								object source,
								string path = null,
								ValidationRule vrule = null,
								List<ValidationRule> rules = null,
								BindingMode mode = BindingMode.TwoWay,
								UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged,
								bool validatesonexceptions = true)
		{
			var b = new Binding();
			b.Source = source;

			if (path != null)
			{
				b.Path = new PropertyPath(path);
			}
			else
			{
				b.Path = new PropertyPath(cb.Name);
			}

			b.Mode = mode;
			b.UpdateSourceTrigger = trigger;
			b.ValidatesOnExceptions = validatesonexceptions;

			if (vrule != null)
			{
				b.ValidationRules.Add(vrule);
			}

			if (rules != null)
			{
				foreach (ValidationRule rule in rules)
				{
					b.ValidationRules.Add(rule);
				}
			}

			cb.SetBinding(CheckBox.IsCheckedProperty, b);
		}

		public static void Bind(this Label l,
								object source,
								string path = null,
								BindingMode mode = BindingMode.OneWay,
								UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged)
		{
			var b = new Binding();
			b.Source = source;

			if (path != null)
			{
				b.Path = new PropertyPath(path);
			}
			else
			{
				b.Path = new PropertyPath(l.Name);
			}

			b.Mode = mode;
			b.UpdateSourceTrigger = trigger;

			l.SetBinding(Label.ContentProperty, b);
		}

		public static void Bind(this DatePicker dp,
								object source,
								string path = null,
								ValidationRule vrule = null,
								List<ValidationRule> rules = null,
								BindingMode mode = BindingMode.TwoWay,
								UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged,
								bool validatesonexceptions = true)
		{
			var b = new Binding();
			b.Source = source;

			if (path != null)
			{
				b.Path = new PropertyPath(path);
			}
			else
			{
				b.Path = new PropertyPath(dp.Name);
			}

			b.Mode = mode;
			b.UpdateSourceTrigger = trigger;
			b.ValidatesOnExceptions = validatesonexceptions;

			if (vrule != null)
			{
				b.ValidationRules.Add(vrule);
			}

			if (rules != null)
			{
				foreach (ValidationRule rule in rules)
				{
					b.ValidationRules.Add(rule);
				}
			}

			dp.SetBinding(DatePicker.TextProperty, b);
		}

		public static void BindSelectedItem(this ComboBox cb,
								object source,
								string path = null,
								ValidationRule vrule = null,
								List<ValidationRule> rules = null,
								BindingMode mode = BindingMode.TwoWay,
								UpdateSourceTrigger trigger = UpdateSourceTrigger.PropertyChanged)
		{
			var b = new Binding();
			b.Source = source;

			if (path != null)
			{
				b.Path = new PropertyPath(path);
			}
			else
			{
				b.Path = new PropertyPath(cb.Name);
			}

			if (vrule != null)
			{
				b.ValidationRules.Add(vrule);
			}

			if (rules != null)
			{
				foreach (ValidationRule rule in rules)
				{
					b.ValidationRules.Add(rule);
				}
			}

			b.Mode = mode;
			b.UpdateSourceTrigger = trigger;

			cb.SetBinding(ComboBox.SelectedItemProperty, b);
		}

		public static void Modify(this DataGridAutoGeneratingColumnEventArgs e, List<string> hiddenColumns,
								  List<string[]> renamedColumns, List<string[]> resizedColumns)
		{
			foreach (var item in hiddenColumns)
			{
				if (e.Hide(item)) return;
			}

			foreach (var item in resizedColumns)
			{
				if (e.Resize(item[0], item[1])) break;
			}

			foreach (var item in renamedColumns)
			{
				if (e.Rename(item[0], item[1])) break;
			}
		}

		public static bool Rename(this DataGridAutoGeneratingColumnEventArgs e, string oldname, string newname)
		{
			if (e.Column.Header.ToString() == oldname)
			{
				e.Column.Header = newname;
				return true;
			}
			return false;
		}

		public static bool Hide(this DataGridAutoGeneratingColumnEventArgs e, string name)
		{
			if (e.Column.Header.ToString() == name)
			{
				e.Cancel = true;
				return true;
			}
			return false;
		}

		public static bool Resize(this DataGridAutoGeneratingColumnEventArgs e, string name, string size)
		{
			if (e.Column.Header.ToString() == name)
			{
				e.Column.Width = new DataGridLength(Convert.ToInt32(size), DataGridLengthUnitType.Star);
				return true;
			}
			return false;
		}
	}

	public class Binder
	{
		public void BindView(params object[] os)
		{
			var tb = new TextBox();
			var cb = new CheckBox();
			var l = new Label();
			var dp = new DatePicker();

			foreach (var o in os)
			{
				if (o.GetType() == tb.GetType())
				{
					((TextBox)o).Bind(this);
				}

				if (o.GetType() == cb.GetType())
				{
					((CheckBox)o).Bind(this);
				}

				if (o.GetType() == l.GetType())
				{
					((Label)o).Bind(this);
				}

				if (o.GetType() == dp.GetType())
				{
					((DatePicker)o).Bind(this);
				}
			}
		}

		public void ChangeView(params object[] os)
		{
			var tb = new TextBox();
			var l = new Label();
			var dp = new DatePicker();
			var cmb = new ComboBox();

			foreach (var o in os)
			{
				if (o.GetType() == tb.GetType())
				{
					((TextBox)o).Text = this.GetType().GetProperty(((TextBox)o).Name).GetValue(this).ToString();
				}

				if (o.GetType() == cmb.GetType())
				{
					((ComboBox)o).SelectedValue = this.GetType().GetProperty(((ComboBox)o).Name).GetValue(this);
				}

				if (o.GetType() == l.GetType())
				{
					((Label)o).Content = this.GetType().GetProperty(((Label)o).Name).GetValue(this).ToString();
				}

				if (o.GetType() == dp.GetType())
				{
					((DatePicker)o).Text = this.GetType().GetProperty(((DatePicker)o).Name).GetValue(this).ToString();
				}
			}
		}
	}
}
