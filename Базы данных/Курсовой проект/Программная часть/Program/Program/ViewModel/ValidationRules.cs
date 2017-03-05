using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Program
{
	public class LengthRangeRule : ValidationRule
	{
		public int Min { get; set; }
		public int? Max { get; set; }

		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			int length = value.ToString().Length;

			if (length < Min)
			{
				return new ValidationResult(false, String.Format("Количество символов во введённой строке " +
																 "не может быть меньше {0}", Min));										
			}

			if (Max != null && length > Max)
			{
				return new ValidationResult(false, String.Format("Количество символов во введённой строке " +
																 "не может быть больше {0}", Max));	
			}

			return ValidationResult.ValidResult;
		}
	}

	public class NotNullRule : ValidationRule
	{
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			if (value == null)
			{
				return new ValidationResult(false, "Значение обязательно должно быть задано.");
			}

			return ValidationResult.ValidResult;
		}
	}
}
