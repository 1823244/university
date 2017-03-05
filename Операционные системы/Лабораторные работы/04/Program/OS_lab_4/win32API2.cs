using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace OS_lab_2
{
    class win32API2
    {
        /// <summary>
        /// Определяет идентификатор текущего процесса
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentProcessId();

        /// <summary>
        /// Определяет псевдодескриптор текущего процесса
        /// 
        /// A pseudo handle is a special constant, currently (HANDLE)-1, that is interpreted as the current process handle.
        /// http://msdn.microsoft.com/ru-ru/library/windows/desktop/ms683179(v=vs.85).aspx
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        /// <summary>
        /// Получение настоящего дескриптора по псевдодескриптору
        /// </summary>
        /// <param name="hSourceProcessHandle">Дескриптор процесса источника</param>
        /// <param name="hSourceHandle">Копируемый дескриптор</param>
        /// <param name="hTargetProcessHandle">Дескриптор процесса-приемника</param>
        /// <param name="lpTargetHandle">Указатель на копию дескриптора</param>
        /// <param name="dwDesiredAccess">Доступ к копии дескриптора</param>
        /// <param name="bInheritHandle">Флаг наследования дескриптора</param>
        /// <param name="dwOptions">Необязательные опции</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DuplicateHandle(
            IntPtr hSourceProcessHandle,
            IntPtr hSourceHandle,
            IntPtr hTargetProcessHandle,
            out IntPtr lpTargetHandle,
            uint dwDesiredAccess,
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            uint dwOptions
            );
        [Flags]
        public enum DuplicateOptions : uint
        {
            DUPLICATE_CLOSE_SOURCE = (0x00000001),// Closes the source handle. This occurs regardless of any error status returned.
            DUPLICATE_SAME_ACCESS = (0x00000002), //Ignores the dwDesiredAccess parameter. The duplicate handle has the same access as the source handle.
        }
        public static IntPtr getHandleDuplicate() {
            IntPtr result = GetCurrentProcess();
            IntPtr result2;
            bool flag = DuplicateHandle(
                GetCurrentProcess(),
                result,
                GetCurrentProcess(),
                out result2,
                0,
                false,
                (uint)DuplicateOptions.DUPLICATE_SAME_ACCESS | (uint)DuplicateOptions.DUPLICATE_CLOSE_SOURCE);

            return result2;
        }

        /// <summary>
        /// Определить дескриптор процесса
        /// пример использования http://www.pinvoke.net/default.aspx/kernel32/OpenProcess.html
        /// </summary>
        /// <param name="dwDesiredAccess">Флаг доступа</param>
        /// <param name="bInheritHandle">Флаг наследования дескриптора</param>
        /// <param name="dwProcessId">Идентификатор процесса</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }


        /// <summary>
        /// Закрывает дескриптор
        /// </summary>
        /// <param name="hObject">Дескриптор</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);
    }
}
