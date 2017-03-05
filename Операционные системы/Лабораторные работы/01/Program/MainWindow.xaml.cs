using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Program
{
    public partial class MainWindow : Window
    {
		private FilterKeyStruct tmp_fk;
		private FilterKeyStruct fk;
		private MinimizedMetricsStruct tmp_mm;
		private MinimizedMetricsStruct mm;
		private uint FKF_FILTERKEYSON = 0x00000001;
		private Dictionary<string, uint> valueFilterKeys = new Dictionary<string, uint>();
		private int[] arColors = { 21, 18, 2 };
		private Color[] arColorsValues = new Color[3];
		private Color[] tmp_arColorsValues = new Color[3];
		private DispatcherTimer timer = null;
		private bool allRight = true;

        public MainWindow()
        {
            InitializeComponent();

			#region Main
			var main = new ObservableCollection<DataObject>();
			
			main.Add(new DataObject() { Name = "Имя компьютера", Value = GetMain.ComputerName() });
			main.Add(new DataObject() { Name = "Имя пользователя", Value = GetMain.UserName() });
			main.Add(new DataObject() { Name = "Путь к каталогу Windows", Value = GetMain.WindowsDirectory() });
			main.Add(new DataObject() { Name = "Путь к системному каталогу", Value = GetMain.SystemDirectory() });
			main.Add(new DataObject() { Name = "Путь к каталогу временных файлов", Value = GetMain.TempPath() });
			main.Add(new DataObject() { Name = "Версия операционной системы", Value = GetMain.WindowsVersion() });

			this.mainGrid.ItemsSource = main;
			#endregion

			#region System Metrics
			string px = " пикс.";
			var metrics = new ObservableCollection<DataObject>();

			metrics.Add(new DataObject() { Name = "Ширина экрана",								Value = GetMetrics.Metric(0) + px });
			metrics.Add(new DataObject() { Name = "Высота экрана",								Value = GetMetrics.Metric(1) + px });
			metrics.Add(new DataObject() { Name = "Ширина вертикальной прокрутки",				Value = GetMetrics.Metric(2) + px });
			metrics.Add(new DataObject() { Name = "Высота горизонтальной прокрутки",			Value = GetMetrics.Metric(3) + px });
			metrics.Add(new DataObject() { Name = "Высота заголовка окна",						Value = GetMetrics.Metric(4) + px });
			metrics.Add(new DataObject() { Name = "Ширина границы окна",						Value = GetMetrics.Metric(5) + px });
			metrics.Add(new DataObject() { Name = "Высота границы окна",						Value = GetMetrics.Metric(6) + px });
			metrics.Add(new DataObject() { Name = "Ширина рамки диалогового окна",				Value = GetMetrics.Metric(7) + px });
			metrics.Add(new DataObject() { Name = "Высота рамки диалогового окна",				Value = GetMetrics.Metric(8) + px });
			metrics.Add(new DataObject() { Name = "Высота вертикального бегунка прокрутки",		Value = GetMetrics.Metric(9) + px });
			metrics.Add(new DataObject() { Name = "Ширина горизонтального бегунка прокрутки",	Value = GetMetrics.Metric(10) + px });
			metrics.Add(new DataObject() { Name = "Ширина значка",								Value = GetMetrics.Metric(11) + px });
			metrics.Add(new DataObject() { Name = "Высота значка",								Value = GetMetrics.Metric(12) + px });
			metrics.Add(new DataObject() { Name = "Ширина курсора",								Value = GetMetrics.Metric(13) + px });
			metrics.Add(new DataObject() { Name = "Высота курсора",								Value = GetMetrics.Metric(14) + px });
			metrics.Add(new DataObject() { Name = "Высота отдельной строки меню",				Value = GetMetrics.Metric(15) + px });
			metrics.Add(new DataObject() { Name = "Ширина полноэкранного окна клиента",			Value = GetMetrics.Metric(16) + px });
			metrics.Add(new DataObject() { Name = "Высота полноэкранного окна клиента",			Value = GetMetrics.Metric(17) + px });
			metrics.Add(new DataObject() { Name = "Статус мыши",								Value = (GetMetrics.Metric(19) == "0" ? "не " : "") + "подключена" });
			metrics.Add(new DataObject() { Name = "Высота стрелки на вертикальной прокрутке",	Value = GetMetrics.Metric(20) + px });
			metrics.Add(new DataObject() { Name = "Ширина стрелки на горизонтальной прокрутке", Value = GetMetrics.Metric(21) + px });
			metrics.Add(new DataObject() { Name = "Статус отладочной версии USER.EXE",			Value = (GetMetrics.Metric(22) == "0" ? "не " : "") + "установлена" });
			metrics.Add(new DataObject() { Name = "Статус левой и правой кнопок мыши",			Value = (GetMetrics.Metric(23) == "0" ? "не " : "") + "поменялись" });
			metrics.Add(new DataObject() { Name = "Минимальная ширина окна",					Value = GetMetrics.Metric(28) + px });
			metrics.Add(new DataObject() { Name = "Минимальная высота окна",					Value = GetMetrics.Metric(29) + px });
			metrics.Add(new DataObject() { Name = "Ширина кнопки заголовка",					Value = GetMetrics.Metric(30) + px });
			metrics.Add(new DataObject() { Name = "Высота кнопки заголовка",					Value = GetMetrics.Metric(31) + px });
			metrics.Add(new DataObject() { Name = "Ширина границы окна изменяемого размера",	Value = GetMetrics.Metric(32) + px });
			metrics.Add(new DataObject() { Name = "Высота границы окна изменяемого размера",	Value = GetMetrics.Metric(33) + px });
			metrics.Add(new DataObject() { Name = "Ширина прямоугольника для двойного клика",	Value = GetMetrics.Metric(36) + px });
			metrics.Add(new DataObject() { Name = "Высота прямоугольника для двойного клика",	Value = GetMetrics.Metric(37) + px });
			metrics.Add(new DataObject() { Name = "Ширина ячейки для позиционирования значков", Value = GetMetrics.Metric(38) + px });
			metrics.Add(new DataObject() { Name = "Высота ячейки для позиционирования значков", Value = GetMetrics.Metric(39) + px });
			metrics.Add(new DataObject() { Name = "Положение всплывающего меню",				Value = (GetMetrics.Metric(40) == "0" ? "слева" : "справа") });
			metrics.Add(new DataObject() { Name = "Набор символов с двойным байтом USER.EXE",	Value = (GetMetrics.Metric(42) == "0" ? "не " : "") + "установлен" });
			metrics.Add(new DataObject() { Name = "Защита",										Value = (GetMetrics.Metric(44) == "0" ? "отсутствует или неактивна" : "присутствует и активна") });
			metrics.Add(new DataObject() { Name = "Подключение к сети",							Value = ((Convert.ToInt32(GetMetrics.Metric(63)) & 0x1) == 0 ? "отсутствует" : "присутствует") });
			metrics.Add(new DataObject() { Name = "Визуальная команда вызова программы",		Value = (GetMetrics.Metric(70) == "0" ? "не " : "") + "показывается" });
			metrics.Add(new DataObject() { Name = "Процессор",									Value = (GetMetrics.Metric(73) == "0" ? "быстрый" : "медленный") });
			metrics.Add(new DataObject() { Name = "Еврейские или арабские языки",				Value = (GetMetrics.Metric(74) == "0" ? "не " : "") + "используются" });
			metrics.Add(new DataObject() { Name = "Мышь с колесом для вертикальной прокрутки",	Value = (GetMetrics.Metric(75) == "0" ? "не " : "") + "используется" });
			metrics.Add(new DataObject() { Name = "Мышь с колесом для горизонтальной прокрутки",Value = (GetMetrics.Metric(91) == "0" ? "не " : "") + "используется" });
			metrics.Add(new DataObject() { Name = "Ширина виртуального экрана",					Value = GetMetrics.Metric(78) + px });
			metrics.Add(new DataObject() { Name = "Высота виртуального экрана",					Value = GetMetrics.Metric(79) + px });
			metrics.Add(new DataObject() { Name = "Количество используемых мониторов",			Value = GetMetrics.Metric(80) });
			metrics.Add(new DataObject() { Name = "Цветовая модель мониторов",					Value = (GetMetrics.Metric(81) == "0" ? "различная" : "одинаковая у всех") });
			metrics.Add(new DataObject() { Name = "Статус сервиса IMM/IME",						Value = (GetMetrics.Metric(82) == "0" ? "не " : "") + "запущен" });
			metrics.Add(new DataObject() { Name = "Статус сеанса",								Value = (GetMetrics.Metric(0x2000) == "0" ? "активен" : "завершается") });

			this.systemGrid.ItemsSource = metrics;
			#endregion

			#region System parameters
			var parameters = new ObservableCollection<DataObject>();

			parameters.Add(new DataObject() { Name = "Время ожидания",			Value = GetParameters.Parameter(60) + " мс" });
			parameters.Add(new DataObject() { Name = "Клавиатура вместо мыши",	Value = (GetParameters.Parameter(68) == "0" ? "не " : "") + "используется" });
			parameters.Add(new DataObject() { Name = "Экранный диктор",		Value = (GetParameters.Parameter(70) == "0" ? "не " : "") + "используется" });

			this.parametersGrid.ItemsSource = parameters;

			valueFilterKeys.Add("0,0 с", 0);
			valueFilterKeys.Add("0,3 с", 300);
			valueFilterKeys.Add("0,5 с", 500);
			valueFilterKeys.Add("0,7 с", 700);
			valueFilterKeys.Add("1,0 с", 1000);
			valueFilterKeys.Add("1,4 с", 1400);
			valueFilterKeys.Add("1,5 с", 1500);
			valueFilterKeys.Add("2,0 с", 2000);
			valueFilterKeys.Add("5,0 с", 5000);
			valueFilterKeys.Add("10,0 с", 10000);
			valueFilterKeys.Add("20,0 с", 20000);

			tmp_fk = fk = GetParameters.FilterKey();

			InitFilter();		

			tmp_mm = mm = GetParameters.MinimizedMetrics();
			InitMinimizedMetrics();
			#endregion

			#region Colors
			arColorsValues[0] = GetColors.Element(arColors[0]);
			arColorsValues[1] = GetColors.Element(arColors[1]);
			arColorsValues[2] = GetColors.Element(arColors[2]);
			arColorsValues.CopyTo(tmp_arColorsValues, 0);

			InitColors();
			#endregion

			#region Time info
			timer = new DispatcherTimer();
			timer.Tick += (sender, e) =>
				{
					currentTime.Content = GetTime.SystemTime();
				};
			timer.Interval = new TimeSpan(0, 0, 0, 1);
			timer.Start();
			biasTime.Content = GetTime.Bias();
			summerTime.Content = GetTime.ShiftingTime(false);
			winterTime.Content = GetTime.ShiftingTime(true);
			#endregion

			#region Additional
			resultNumber.Content = GetAdditional.CurrencyFormat(Convert.ToDouble(number.Text), Convert.ToUInt32(digits.Text), Convert.ToUInt32(grouping.Text), decimalsep.Text, thousandsep.Text);

			errorCode.Content = GetAdditional.LastError().ToString();
			#endregion
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MinimizedMetricsStruct
		{
			public int cbSize;
			public int iWidth;
			public int iHorzGap;
			public int iVertGap;
			public MinimizedMetricsArrangement iArrange;
		}

		[Flags]
		public enum MinimizedMetricsArrangement
		{
			BottomLeft = 0,
			BottomRight = 1,
			TopLeft = 2,
			TopRight = 3,
			Left = 0,
			Right = 0,
			Up = 4,
			Down = 4,
			Hide = 8
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct FilterKeyStruct
		{
			public int cbSize;
			public uint dwFlags;
			public uint iWaitMSec;
			public uint iDelayMSec;
			public uint iRepeatMSec;
			public uint iBounceMSec;
		}

		public class DataObject
		{
			public string Name { get; set; }
			public string Value { get; set; }
		}

		private void InitColors()
		{
			arColorsValues[0].A = 255;
			arColorsValues[1].A = 255;
			arColorsValues[2].A = 255;
			shadowColor.SelectedColor = arColorsValues[0];
			commandKeysColor.SelectedColor = arColorsValues[1];
			captionColor.SelectedColor = arColorsValues[2];
		}

		private void InitMinimizedMetrics()
		{
			enableMinimized.IsChecked = mm.iArrange.ToString().Contains("Hide");
		}

		private void enableFilter_Checked(object sender, RoutedEventArgs e)
		{
			fk.dwFlags |= FKF_FILTERKEYSON;
			SetParameters.FilterKey(fk);
			EnableGroup(true);
		}

		private void enableFilter_Unchecked(object sender, RoutedEventArgs e)
		{
			fk.dwFlags &= ~FKF_FILTERKEYSON;
			SetParameters.FilterKey(fk);
			EnableGroup(false);
		}

		private void UpdateColors()
		{
			SetColors.Elements(3, arColors, arColorsValues);
		}

		private void EnableGroup(bool isEnable)
		{
			SolidColorBrush black = new SolidColorBrush();
			black.Color = Color.FromRgb(0, 0, 0);
			SolidColorBrush gray = new SolidColorBrush();
			gray.Color = Color.FromRgb(109, 109, 109);

			GroupFilter.IsEnabled = isEnable;

			GroupFilter.Foreground = 
			iWaitMSecFilter.Foreground = 
			iDelayMSecFilter.Foreground = 
			iRepeatMSecFilter.Foreground = isEnable ? black : gray;
		}

		private void iWaitMSecFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!this.IsLoaded) return;
			fk.iWaitMSec = valueFilterKeys[iWaitMSecFilter.SelectedValue.ToString()];
			SetParameters.FilterKey(fk);
		}

		private void iDelayMSecFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!this.IsLoaded) return;
			fk.iDelayMSec = valueFilterKeys[iDelayMSecFilter.SelectedValue.ToString()];
			SetParameters.FilterKey(fk);
		}

		private void iRepeatMSecFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!this.IsLoaded) return;
			fk.iRepeatMSec = valueFilterKeys[iRepeatMSecFilter.SelectedValue.ToString()];
			SetParameters.FilterKey(fk);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			SetParameters.FilterKey(tmp_fk);
			fk = tmp_fk;
			InitFilter();
			SetParameters.MinimizedMetrics(tmp_mm);
		}

		private void InitFilter()
		{
			if ((fk.dwFlags & FKF_FILTERKEYSON) == 0)
			{
				enableFilter.IsChecked = false;
				EnableGroup(false);
			}
			else
			{
				enableFilter.IsChecked = true;
				EnableGroup(true);
			}

			foreach (KeyValuePair<string, uint> pair in valueFilterKeys)
			{
				if (fk.iWaitMSec.Equals(pair.Value))
				{
					iWaitMSecFilter.SelectedValue = pair.Key;
				}

				if (fk.iDelayMSec.Equals(pair.Value))
				{
					iDelayMSecFilter.SelectedValue = pair.Key;
				}

				if (fk.iRepeatMSec.Equals(pair.Value))
				{
					iRepeatMSecFilter.SelectedValue = pair.Key;
				}
			}
		}

		private void enableMinimized_Checked(object sender, RoutedEventArgs e)
		{
			mm.iArrange = MinimizedMetricsArrangement.Hide;
			SetParameters.MinimizedMetrics(mm);
		}

		private void enableMinimized_Unchecked(object sender, RoutedEventArgs e)
		{
			mm.iArrange = MinimizedMetricsArrangement.Left;
			SetParameters.MinimizedMetrics(mm);
		}

		private void HideReset_Click(object sender, RoutedEventArgs e)
		{
			SetParameters.MinimizedMetrics(tmp_mm);
			mm = tmp_mm;
			InitMinimizedMetrics();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			tmp_arColorsValues.CopyTo(arColorsValues, 0);
			UpdateColors();
			InitColors();
		}

		private void shadowColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
		{
			arColorsValues[0] = shadowColor.SelectedColor;
			UpdateColors();
		}

		private void commandKeysColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
		{
			arColorsValues[1] = commandKeysColor.SelectedColor;
			UpdateColors();
		}

		private void captionColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
		{
			arColorsValues[2] = captionColor.SelectedColor;
			UpdateColors();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			User32.ActivateKeyboardLayout(0, 0);
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			User32.ActivateKeyboardLayout(1, 0);
		}

		private void number_KeyUp(object sender, KeyEventArgs e)
		{
			TextBox t = (TextBox)sender;
			allRight = true;

			if (! (t.Text.Length == 0))
			{
				try
				{
					if (t.Name == "digits" || t.Name == "grouping")
					{
						uint result = Convert.ToUInt32(t.Text);
					}
					else
					{
						double result = Convert.ToDouble(t.Text);
					}
				}
				catch (Exception ex)
				{
					e.Handled = true;
					t.Text = t.Text.Substring(0, t.Text.Length - 1);
					t.Focus();
					t.CaretIndex = t.Text.Length;
					allRight = false;
				}

				if (allRight)
				{
					resultNumber.Content = GetAdditional.CurrencyFormat(Convert.ToDouble(number.Text), Convert.ToUInt32(digits.Text), Convert.ToUInt32(grouping.Text), decimalsep.Text, thousandsep.Text);
				}
				allRight = true;
			}
		}

		private void decimalsep_KeyUp(object sender, KeyEventArgs e)
		{
			if (allRight)
			{
				resultNumber.Content = GetAdditional.CurrencyFormat(Convert.ToDouble(number.Text), Convert.ToUInt32(digits.Text), Convert.ToUInt32(grouping.Text), decimalsep.Text, thousandsep.Text);
			}
		}

		private void toOEM_KeyUp(object sender, KeyEventArgs e)
		{
			inOEM.Content = GetAdditional.CharToOem(toOEM.Text);
			fromOEM.Content = GetAdditional.OemToChar(inOEM.Content.ToString());
		}
    }
}
