using System;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.ComponentModel;
using System.Windows.Threading;

namespace Program
{
	public partial class MainWindow : Window
	{
		private DispatcherTimer timer = null;

		public MainWindow()
		{
			InitializeComponent();

			System.Diagnostics.Process.EnterDebugMode();

			#region StepByStep
			uint pid = Kernel32.GetCurrentProcessId();
			LabelCurrentPID.Content = pid;

			IntPtr pHandle = Kernel32.GetCurrentProcess();
			LabelPHandle.Content = String.Format("0x{0:X8}", (uint)pHandle);

			IntPtr pHandleDuplicate;
			Kernel32.DuplicateHandle(pHandle, pHandle, pHandle, out pHandleDuplicate, 0, false,
									 0x00000001 | 0x00000002);
			LabelPHandleDuplicate.Content = String.Format("0x{0:X8}", (uint)pHandleDuplicate);

			IntPtr pHandleOpenProcess = Kernel32.OpenProcess(0x00000040, false, Convert.ToInt32(pid));
			LabelPHandleOpenProcess.Content = String.Format("0x{0:X8}", (uint)pHandleOpenProcess);

			LabelPHandleDuplicateClose.Content = (Kernel32.CloseHandle(pHandleDuplicate))
												 ? "Закрылся " + LabelPHandleDuplicateClose.Content
												 : "Не закрылся " + LabelPHandleDuplicateClose.Content;
			LabelPHandleOpenProcessClose.Content = (Kernel32.CloseHandle(pHandleOpenProcess))
												   ? "Закрылся " + LabelPHandleOpenProcessClose.Content
												   : "Не закрылся " + LabelPHandleOpenProcessClose.Content;
			#endregion

			#region Timer
			timer = new DispatcherTimer();
			timer.Tick += new EventHandler(timerTick);
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Start();
			#endregion

			UpdateModulesGrid();
			UpdateDriversGrid();
			UpdateThreadsGrid9x();
			UpdateModulesGrid9x();
		}

		private void timerTick(object sender, EventArgs e)
		{
			UpdateProcessesGrid();
			UpdateProcessesGrid9x();
		}

		/// <summary>
		/// Get handle, name and fullname
		/// </summary>
		/// <param name="handle">Handle</param>
		/// <param name="name">Name of file</param>
		/// <param name="fullname">Name with path to file</param>
		public static void GetHandle(ref string handle, ref string name, ref string fullname)
		{
			if (handle != null && handle.Length > 0)
			{
				fullname = GetFullName(new IntPtr(Int32.Parse(handle)));
				name = GetName(fullname);
				
				return;
			}

			if (fullname != null && fullname.Length > 0)
			{
				name = GetName(fullname);
				handle = Kernel32.GetModuleHandle(name).ToInt32().ToString();

				return;
			}

			if (name != null && name.Length > 0)
			{
				IntPtr h = Kernel32.GetModuleHandle(name);
				handle = h.ToInt32().ToString();
				fullname = GetFullName(h);

				return;
			}

			fullname = GetFullName(IntPtr.Zero);
			name = GetName(fullname);
			handle = Kernel32.GetModuleHandle(name).ToInt32().ToString();
		}

		/// <summary>
		/// Get name and path to file by handle
		/// </summary>
		/// <param name="handle">Handle</param>
		/// <returns>Full path to file</returns>
		public static string GetFullName(IntPtr handle)
		{
			StringBuilder fullname = new StringBuilder(255);
			Kernel32.GetModuleFileName(handle, fullname, 255);

			return fullname.ToString();
		}

		/// <summary>
		/// Get name of file by fullname
		/// </summary>
		/// <param name="fullname">Full path to file</param>
		/// <returns></returns>
		public static string GetName(string fullname)
		{
			string[] splited = fullname.Split('\\');

			return splited[splited.Length - 1];
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string name = ProcessName.Text;
			string path = ProcessPath.Text;
			string handle = ProcessHandle.Text.Split('x').Last();
			if (handle.Length != 0)
			{
				handle = System.Int32.Parse(handle, NumberStyles.AllowHexSpecifier).ToString();
			}

			GetHandle(ref handle, ref name, ref path);

			ProcessName.Text = name;
			ProcessPath.Text = path;
			ProcessHandle.Text = String.Format("0x{0:X8}", Convert.ToUInt32(handle));
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			ProcessName.Text = "";
			ProcessPath.Text = "";
			ProcessHandle.Text = "";
		}

		public static uint[] GetPIDs()
		{
			UInt32 size = 120, 
				   bytes = size * sizeof(UInt32),
				   copy;
			var PIDs = new UInt32[size];

			bool success = Psapi.EnumProcesses(PIDs, bytes, out copy);

			if (success && copy > 0)
			{
				copy >>= 2;
				var result = new UInt32[copy];

				for (int i = 0; i < copy; i++)
				{
					result[i] = PIDs[i];
				}

				return result;
			}
			else
			{
				return new UInt32[0];
			}
		}

		public static IntPtr[] GetProcessHandles(uint[] PIDs)
		{
			var result = new IntPtr[PIDs.Length];

			for (int i = 0; i < PIDs.Length; i++)
			{
				result[i] = Kernel32.OpenProcess((int)Kernel32.ProcessAccessFlags.QueryInformation
												 | (int)Kernel32.ProcessAccessFlags.VMRead, false, (int)PIDs[i]);
			}

			return result;
		}

		public static string[] GetProcessNames(IntPtr[] handles)
		{
			var result = new string[handles.Length];
			var tmp = GetProcessModules(handles);

			for (int i = 0; i < handles.Length; i++)
			{
				if (tmp[i].Length == 0)
				{
					result[i] = "";
					continue;
				}

				var sb = new StringBuilder(255);
				uint size = Psapi.GetModuleBaseName(handles[i], new IntPtr(tmp[i][0]), sb, 255);
				result[i] = sb.ToString().Substring(0, (int)size);
			}

			return result;
		}

		public static uint[][] GetProcessModules(IntPtr[] handles)
		{
			uint size = 1000,
				 bytes = size * sizeof(uint),
				 copy;

			var result = new uint[size];
			var full = new uint[handles.Length][];

			for (int i = 0; i < handles.Length; i++)
			{
				Psapi.EnumProcessModules(handles[i], result, bytes, out copy);
				copy >>= 2;
				var tmp = new uint[copy];
				for (int j = 0; j < copy; j++)
				{
					tmp[j] = result[j];
				}

				full[i] = tmp;
			}

			return full;
		}

		private static Psapi.MODULEINFO[] GetModuleInfo(IntPtr handle, uint[] modules)
		{
			var infos = new Psapi.MODULEINFO[modules.Length];
			Psapi.MODULEINFO info;

			for (int i = 0; i < modules.Length; i++)
			{
				if (Psapi.GetModuleInformation(handle,
											   new IntPtr(modules[i]),
											   out info,
											   (uint)Marshal.SizeOf(new Psapi.MODULEINFO())))
				{
					infos[i] = info;
				}
				else
				{
					infos[i] = new Psapi.MODULEINFO();
					infos[i].EntryPoint = IntPtr.Zero;
					infos[i].lpBaseOfDll = IntPtr.Zero;
					infos[i].SizeOfImage = 0;
				}
			}

			return infos;
		}

		public static string[] GetModulesInfo(IntPtr[] handles)
		{
			var result = new string[handles.Length];
			var tmp = GetProcessModules(handles);

			for (int i = 0; i < handles.Length; i++)
			{
				if (tmp[i].Length == 0)
				{
					result[i] = "";
					continue;
				}

				Psapi.MODULEINFO info;

				if (Psapi.GetModuleInformation(handles[i], new IntPtr(tmp[i][0]), out info,
											   (uint)Marshal.SizeOf(new Psapi.MODULEINFO())))
				{
					result[i] = info.SizeOfImage + " б";
					while (result[i].Length < 30)
					{
						result[i] += " ";
					}
				}
			}

			return result;
		}

		public static string[][] GetModulesFilenames(uint[] PIDs, string[] names, IntPtr[] handles)
		{
			var result = new List<string[]>();
			var tmp = GetProcessModules(handles);

			for (int i = 0; i < tmp.GetLength(0); i++)
			{
				if (tmp[i].Length == 0)
				{
					continue;
				}

				Psapi.MODULEINFO[] info = GetModuleInfo(handles[i], tmp[i]);

				for (int j = tmp[i].Length - 1; j >= 0; j--)
				{
					var sb = new StringBuilder(255);
					var size = Psapi.GetModuleFileNameEx(handles[i], new IntPtr(tmp[i][j]), sb, 255);
					result.Add(new string[] { PIDs[i].ToString(),
											  names[i],
											  info[j].lpBaseOfDll.ToString(),
											  info[j].EntryPoint.ToString(),
											  info[j].SizeOfImage.ToString() + " б",
											  sb.ToString() });
				}
			}

			return result.ToArray();
		}

		public static List<string[]> GetDriversInfo()
		{
			var result = new List<string[]>();
			uint size, bytes, needed;
			uint[] addresses;

			bool success = Psapi.EnumDeviceDrivers(null, 0, out needed);

			if (!success || needed == 0)
			{
				return null;
			}

			size = needed >> 2;
			bytes = needed;
			addresses = new uint[size];

			success = Psapi.EnumDeviceDrivers(addresses, bytes, out needed);

			if (!success)
			{
				return null;
			}

			for (int i = 0; i < size; i++)
			{
				var sb = new StringBuilder(1000);

				int res = Psapi.GetDeviceDriverBaseName(addresses[i], sb, sb.Capacity);

				result.Add(new string[] { addresses[i].ToString(), sb.ToString() });
			}

			return result;
		}

		public static List<string[]> GetProcessesInfo()
		{
			var result = new List<string[]>();
			var handleToSnapshot = IntPtr.Zero;

			try
			{
				var procEntry = new Kernel32.PROCESSENTRY32();
				procEntry.dwSize = (uint)Marshal.SizeOf(typeof(Kernel32.PROCESSENTRY32));
				handleToSnapshot = Kernel32.CreateToolhelp32Snapshot((uint)Kernel32.SnapshotFlags.Process, 0);

				if (Kernel32.Process32First(handleToSnapshot, ref procEntry))
				{
					do
					{
						result.Add(new string[] { procEntry.cntUsage.ToString(), procEntry.th32ProcessID.ToString(),
												  procEntry.th32DefaultHeapID.ToString(),
												  procEntry.th32ModuleID.ToString(),
												  procEntry.pcPriClassBase.ToString(),
												  procEntry.szExeFile.ToString()
						});
					} while (Kernel32.Process32Next(handleToSnapshot, ref procEntry));
				}
				else
				{
					throw new ApplicationException(string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
				}
			}
			finally
			{
				Kernel32.CloseHandle(handleToSnapshot);
			}

			return result;
		}

		public static List<string[]> GetThreadsInfo()
		{
			var result = new List<string[]>();
			var handleToSnapshot = IntPtr.Zero;

			try
			{
				var threadEntry = new Kernel32.THREADENTRY32();
				threadEntry.dwSize = (uint)Marshal.SizeOf(typeof(Kernel32.THREADENTRY32));
				handleToSnapshot = Kernel32.CreateToolhelp32Snapshot((uint)Kernel32.SnapshotFlags.Thread, 0);

				if (Kernel32.Thread32First(handleToSnapshot, ref threadEntry))
				{
					do
					{
						result.Add(new string[] { threadEntry.cntUsage.ToString(), threadEntry.th32ThreadID.ToString(),
												  threadEntry.th32OwnerProcessID.ToString(), 
												  threadEntry.tpBasePri.ToString(), threadEntry.tpDeltaPri.ToString() 
						});
					} while (Kernel32.Thread32Next(handleToSnapshot, ref threadEntry));
				}
				else
				{
					throw new ApplicationException(string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
				}
			}
			catch { }
			finally
			{
				Kernel32.CloseHandle(handleToSnapshot);
			}

			return result;
		}

		public static List<string[]> GetModulesInfo()
		{
			var result = new List<string[]>();
			var handleToSnapshot = IntPtr.Zero;

			try
			{
				var modEntry = new Kernel32.MODULEENTRY32();
				modEntry.dwSize = (uint)Marshal.SizeOf(typeof(Kernel32.MODULEENTRY32));
				handleToSnapshot = Kernel32.CreateToolhelp32Snapshot((uint)Kernel32.SnapshotFlags.All, 0);

				if (Kernel32.Module32First(handleToSnapshot, ref modEntry))
				{
					do
					{
						result.Add(new string[] { modEntry.th32ModuleID.ToString(), modEntry.th32ProcessID.ToString(),
												  modEntry.GlblcntUsage.ToString(), modEntry.ProccntUsage.ToString(),
												  modEntry.modBaseAddr.ToString(), modEntry.modBaseSize.ToString(),
												  String.Format("0x{0:X8}", (uint)modEntry.hModule),
												  modEntry.szModule.ToString(), modEntry.szExePath.ToString() 
						});
					} while (Kernel32.Module32Next(handleToSnapshot, ref modEntry));
				}
				else
				{
					throw new ApplicationException(string.Format("Failed with win32 error code {0}", Marshal.GetLastWin32Error()));
				}
			}
			catch { }
			finally
			{
				Kernel32.CloseHandle(handleToSnapshot);
			}

			return result;
		}	

		private void UpdateProcessesGrid()
		{
			var PIDs = GetPIDs();
			var tmp = ProcessesCounter.Content;
			ProcessesCounter.Content = "Обновляю..";

			if (ProcessesGrid.Items.Count == 0 || ProcessesGrid.Items.Count != PIDs.Length)
			{
				var handles = GetProcessHandles(PIDs);
				var names = GetProcessNames(handles);
				var modules = GetModulesInfo(handles);
				var ps = Processes.Fill(PIDs, handles, names, modules);

				ProcessesCounter.Content = "Всего процессов: " + ps.Count.ToString();
				ProcessesGrid.ItemsSource = ps.OrderByDescending(o => o.Name).ToList();

				ProcessesGrid.Items.Refresh();

				for (int i = 0; i < handles.Length; i++)
				{
					Kernel32.CloseHandle(handles[i]);
				}
			}
			else
			{
				ProcessesCounter.Content = tmp;
			}
		}

		private void UpdateModulesGrid()
		{
			var PIDs = GetPIDs();
			var handles = GetProcessHandles(PIDs);
			var names = GetProcessNames(handles);
			var filenames = GetModulesFilenames(PIDs, names, handles);
			var ms = Modules.Fill(filenames);

			ModulesCounter.Content = "Модулей: " + ms.Count.ToString();
			ModulesGrid.ItemsSource = ms;

			for (int i = 0; i < handles.Length; i++)
			{
				Kernel32.CloseHandle(handles[i]);
			}
		}

		private void UpdateDriversGrid()
		{
			var drivers = GetDriversInfo();
			var ds = Drivers.Fill(drivers);

			DriversCounter.Content = "Драйверов: " + ds.Count.ToString();
			DriversGrid.ItemsSource = ds;
		}

		private void UpdateProcessesGrid9x()
		{
			var processes = GetProcessesInfo();
			var tmp = ProcessesCounter9x.Content;
			ProcessesCounter9x.Content = "Обновляю..";

			if (ProcessesGrid9x.Items.Count == 0 || ProcessesGrid9x.Items.Count != processes.Count)
			{
				var ps = Processes9x.Fill(processes);

				ProcessesCounter9x.Content = "Процессов: " + ps.Count.ToString();
				ProcessesGrid9x.ItemsSource = ps;
			}
			else
			{
				ProcessesCounter9x.Content = tmp;
			}
		}

		private void UpdateThreadsGrid9x()
		{
			var threads = GetThreadsInfo();
			var tmp = ThreadsCounter9x.Content;
			ThreadsCounter9x.Content = "Обновляю...";

			if (ThreadsGrid9x.Items.Count == 0 || ThreadsGrid9x.Items.Count != threads.Count)
			{
				var ts = Threads9x.Fill(threads);

				ThreadsCounter9x.Content = "Потоков: " + ts.Count.ToString();
				ThreadsGrid9x.ItemsSource = ts;
			}
			else
			{
				ThreadsCounter9x.Content = tmp;
			}
		}

		private void UpdateModulesGrid9x()
		{
			var modules = GetModulesInfo();
			var tmp = ModulesCounter9x.Content;
			ModulesCounter9x.Content = "Обновляю...";

			if (ModulesGrid9x.Items.Count == 0 || ModulesGrid9x.Items.Count != modules.Count)
			{
				var ms = Modules9x.Fill(modules);

				ModulesCounter9x.Content = "Модулей: " + ms.Count.ToString();
				ModulesGrid9x.ItemsSource = ms;
			}
			else
			{
				ModulesCounter9x.Content = tmp;
			}
		} 

		public static string PriorityToString(string pri)
		{
			switch (pri)
			{
				case "4": return "Ожидающий";

				case "8": return "Нормальный";

				case "13": return "Высокий";

				case "24": return "Реального времени";

				default: return "Неизвестен";
			}
		}

		private void Processes_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			if (e.Column.Header.ToString() == "ID")
			{
				e.Column.Header = "PID";
				e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "Handle")
			{
				e.Column.Header = "Дескриптор";
				e.Column.Width = new DataGridLength(3, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "Info")
			{
				e.Column.Header = "Размер модуля";
				e.Column.Width = new DataGridLength(4, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "Name")
			{
				e.Column.Header = "Имя модуля";
				e.Column.Width = new DataGridLength(5, DataGridLengthUnitType.Star);
				e.Column.SortMemberPath = "Name";
			}
		}

		private void Modules_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			if (e.Column.Header.ToString() == "PName")
			{
				e.Column.Header = "Образ";
			}

			if (e.Column.Header.ToString() == "Address")
			{
				e.Column.Header = "Адрес загрузки";
			}

			if (e.Column.Header.ToString() == "Enter")
			{
				e.Column.Header = "Точка входа";
			}

			if (e.Column.Header.ToString() == "Size")
			{
				e.Column.Header = "Размер";
			}

			if (e.Column.Header.ToString() == "Path")
			{
				e.Column.Header = "Полный путь";
			}
		}

		private void DriversGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			if (e.Column.Header.ToString() == "Address")
			{
				e.Column.Header = "Адрес загрузки";
				e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "Name")
			{
				e.Column.Header = "Имя";
				e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}
		}

		private void ProcessesGrid9x_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			if (e.Column.Header.ToString() == "Links")
			{
				e.Column.Header = "Кол-во ссылок";
				e.Column.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "Priority")
			{
				e.Column.Header = "Приоритет";
				e.Column.Width = new DataGridLength(3, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "Name")
			{
				e.Column.Header = "Имя";
				e.Column.Width = new DataGridLength(3, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "ID")
			{
				e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "HID")
			{
				e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "MID")
			{
				e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}
		}

		private void ThreadsGrid9x_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			if (e.Column.Header.ToString() == "Links")
			{
				e.Column.Header = "Кол-во ссылок";
				e.Column.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "Priority")
			{
				e.Column.Header = "Приоритет";
				e.Column.Width = new DataGridLength(3, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "DPriority")
			{
				e.Column.Header = "Дельта приоритет";
				e.Column.Width = new DataGridLength(3, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "ID")
			{
				e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}

			if (e.Column.Header.ToString() == "PID")
			{
				e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
			}
		}

		private void ModulesGrid9x_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			if (e.Column.Header.ToString() == "Links")
			{
				e.Column.Header = "Кол-во ссылок";
			}

			if (e.Column.Header.ToString() == "PLinks")
			{
				e.Column.Header = "Ссылок в контексте владельца";
			}

			if (e.Column.Header.ToString() == "Address")
			{
				e.Column.Header = "Базовый адрес";
			}

			if (e.Column.Header.ToString() == "Size")
			{
				e.Column.Header = "Базовый размер";
			}

			if (e.Column.Header.ToString() == "Handle")
			{
				e.Column.Header = "Дескриптор";
			}

			if (e.Column.Header.ToString() == "Name")
			{
				e.Column.Header = "Имя";
			}

			if (e.Column.Header.ToString() == "Path")
			{
				e.Column.Header = "Путь";
			}
		}
	}

	class Processes
	{
		public string ID { get; set; }
		public string Handle { get; set; }
		public string Info { get; set; }
		public string Name { get; set; }

		public Processes(string id, string handle, string info, string name)
		{
			ID = id;
			Handle = handle;
			Info = info;
			Name = name;
		}

		public static List<Processes> Fill(uint[] pids, IntPtr[] handles, string[] names, string[] modules)
		{
			var result = new List<Processes>();

			for (int i = 0; i < pids.Length; i++)
			{
				result.Add(new Processes(pids[i].ToString(), String.Format("0x{0:X8}", (uint)handles[i]),
										   modules[i], names[i]));
			}

			return result;
		}
	}

	class Modules
	{
		public string PID { get; set; }
		public string PName { get; set; }
		public string Address { get; set; }
		public string Enter { get; set; }
		public string Size { get; set; }
		public string Path { get; set; }

		public Modules(string pid, string pname, string address, string enter, string size, string path)
		{
			PID = pid;
			PName = pname;
			Address = address;
			Enter = enter;
			Size = size;
			Path = path;
		}

		public static List<Modules> Fill(string[][] filenames)
		{
			var result = new List<Modules>();

			for (int i = 0; i < filenames.GetLength(0); i++)
			{
				result.Add(new Modules(filenames[i][0], filenames[i][1], String.Format("0x{0:X8}", Convert.ToUInt32(filenames[i][2])), String.Format("0x{0:X8}", Convert.ToUInt32(filenames[i][3])),
									   filenames[i][4], filenames[i][5]));
			}

			return result;
		}
	}

	class Drivers
	{
		public string Address { get; set; }
		public string Name { get; set; }

		public Drivers(string address, string name)
		{
			Address = address;
			Name = name;
		}

		public static List<Drivers> Fill(List<string[]> drivers)
		{
			var result = new List<Drivers>();

			for (int i = 0; i < drivers.Count; i++)
			{
				result.Add(new Drivers(String.Format("0x{0:X8}", Convert.ToUInt32(drivers[i][0])), drivers[i][1]));
			}

			return result;
		}
	}

	class Processes9x
	{
		public string ID { get; set; }
		public string Name { get; set; }
		public string Priority { get; set; }
		public string Links { get; set; }
		public string HID { get; set; }
		public string MID { get; set; }

		public Processes9x(string id, string name, string priority, string links, string hid, string mid)
		{
			ID = id;
			Name = name;
			Priority = priority;
			Links = links;
			HID = hid;
			MID = mid;
		}

		public static List<Processes9x> Fill(List<string[]> info)
		{
			var result = new List<Processes9x>();

			for (int i = 0; i < info.Count; i++)
			{
				result.Add(new Processes9x(info[i][1], info[i][5], MainWindow.PriorityToString(info[i][3]),
										   info[i][0], info[i][2], info[i][3]));
			}

			return result;
		}
	}

	class Threads9x
	{
		public string ID { get; set; }
		public string PID { get; set; }
		public string Links { get; set; }
		public string Priority { get; set; }
		public string DPriority { get; set; }

		public Threads9x(string id, string pid, string links, string priority, string dpriority)
		{
			ID = id;
			PID = pid;
			Links = links;
			Priority = priority;
			DPriority = dpriority;
		}

		public static List<Threads9x> Fill(List<string[]> info)
		{
			var result = new List<Threads9x>();

			for (int i = 0; i < info.Count; i++)
			{
				result.Add(new Threads9x(info[i][1], info[i][2], info[i][0], MainWindow.PriorityToString(info[i][3]),
										 info[i][4]));
			}

			return result;
		}
	}

	class Modules9x
	{
		public string ID { get; set; }
		public string PID { get; set; }
		public string Links { get; set; }
		public string PLinks { get; set; }
		public string Address { get; set; }
		public string Size { get; set; }
		public string Handle { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }

		public Modules9x(string id, string pid, string links, string plinks, string address, string size, string handle, string name,
						 string path)
		{
			ID = id;
			PID = pid;
			Links = links;
			PLinks = plinks;
			Address = address;
			Size = size;
			Handle = handle;
			Name = name;
			Path = path;
		}

		public static List<Modules9x> Fill(List<string[]> info)
		{
			var result = new List<Modules9x>();

			for (int i = 0; i < info.Count; i++)
			{
				result.Add(new Modules9x(info[i][0], info[i][1], info[i][2], info[i][3], String.Format("0x{0:X8}", Convert.ToUInt32(info[i][4])), info[i][5],
										 info[i][6], info[i][7], info[i][8]));
			}

			return result;
		}
	}
}
