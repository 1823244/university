using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace OS_lab_3_2_1
{
    class win32API
    {
        [Flags]
        public enum SyncObjectAccess : uint
        {
            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000,
            SYNCHRONIZE = 0x00100000,
            EVENT_ALL_ACCESS = 0x001F0003,
            EVENT_MODIFY_STATE = 0x00000002,
            MUTEX_ALL_ACCESS = 0x001F0001,
            MUTEX_MODIFY_STATE = 0x00000001,
            SEMAPHORE_ALL_ACCESS = 0x001F0003,
            SEMAPHORE_MODIFY_STATE = 0x00000002,
            TIMER_ALL_ACCESS = 0x001F0003,
            TIMER_MODIFY_STATE = 0x00000002,
            TIMER_QUERY_STATE = 0x00000001
        }

        [DllImport("kernel32.dll", SetLastError=true)]
        private static extern IntPtr CreateSemaphore(
                IntPtr lpSemaphoreAttributes,
                int lInitialCount,
                int lMaximumCount,
                string lpName);
        
        [DllImport("kernel32.dll")]
        private static extern bool ReleaseSemaphore(
                IntPtr hSemaphore,
                int lReleaseCount,
                IntPtr lpPreviousCount);
        
        [DllImport("kernel32.dll", SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError=true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenSemaphore(
                uint dwDesiredAccess,
                [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
                string lpName);



        public static IntPtr createSemaphore(decimal maxValue, String nameSemaphore)
        {
            return CreateSemaphore(IntPtr.Zero, (int)maxValue, (int)maxValue, nameSemaphore);
        }
        public static IntPtr openSemaphore(String nameSemaphore)
        {
            return OpenSemaphore((uint)SyncObjectAccess.SEMAPHORE_ALL_ACCESS, false, nameSemaphore);
        }
        public static bool releaseSemaphore(IntPtr semaphore)
        {
            return ReleaseSemaphore(semaphore, 1, IntPtr.Zero);
        }
        public static bool closeSemaphore(IntPtr semaphore)
        {
            releaseSemaphore(semaphore);
            return CloseHandle(semaphore);
        }
        public static bool pause(IntPtr semaphore)
        {
            const UInt32 WAIT_TIMEOUT = 0x00000102;
            const UInt32 WAIT_ABANDONED = 0x00000080;
            const UInt32 WAIT_OBJECT_0 = 0x00000000;

            UInt32 result = WaitForSingleObject(semaphore, 0xFFFFFFFF);
            if (result == WAIT_TIMEOUT)
            {
                throw new Exception("Время вышло, но объект до сих пор занят");
            } else if (result == WAIT_ABANDONED) {
                throw new Exception("Ошибка мьютекса (объект не был освобожден потоком, который владел им до своего завершения)");
            } else if (result != WAIT_OBJECT_0) {
                throw new Exception("Ошибка выполнения функции WaitForSingleObject");
            }
            return true;
        }
    }
}
