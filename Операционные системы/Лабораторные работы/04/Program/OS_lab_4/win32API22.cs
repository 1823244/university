using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using OS_lab_2;

namespace OS_lab3
{
    class win32API22
    {
        /// <summary>
        /// Перечисляет идентификаторы процессов: для каждого процесса в системе
        /// Пример использования http://www.pinvoke.net/default.aspx/psapi.EnumProcesses
        /// </summary>
        /// <param name="processIds">Массив: который будет заполнен значениями ID процессов</param>
        /// <param name="arraySizeBytes">Количество элементов передаваемых в processIds</param>
        /// <param name="bytesCopied">Количество байтов, скопированных в массив processIds. ... div Sizeof(DWORD) будет определять число элементов</param>
        /// <returns>true: если функция отработала без ошибок, иначе false</returns>
        [DllImport("Psapi.dll", SetLastError = true)]
        static extern bool EnumProcesses(
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)]
            [In][Out] UInt32[] processIds,

            UInt32 arraySizeBytes,

            [MarshalAs(UnmanagedType.U4)]
            out UInt32 bytesCopied
            );
        /// <summary>
        /// Перечесляет дескрипторы каждого модуля данного процесса
        /// Пример использования http://www.pinvoke.net/default.aspx/psapi.EnumProcessModules
        /// </summary>
        /// <param name="hProcess">Дескриптор процесса</param>
        /// <param name="lphModule">массив дескрипторов модулей</param>
        /// <param name="cb">Число элементов в массиве</param>
        /// <param name="lpcbNeeded">Количество байтов, скопированных в массив lphModule. ... div Sizeof(DWORD) будет определять число элементов</param>
        /// <returns></returns>
        [DllImport("psapi.dll", SetLastError = true)]
        static extern bool EnumProcessModules(
            IntPtr hProcess,

            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4)] 
            [In][Out] uint[] lphModule,

            uint cb,

            [MarshalAs(UnmanagedType.U4)] 
            out uint lpcbNeeded
            );
        /// <summary>
        /// Получает имя модуля по его дускриптору
        /// </summary>
        /// <param name="hProcess">Дескриптор процесса</param>
        /// <param name="hModule">Дескриптор модуля</param>
        /// <param name="lpBaseName">Строка с результатом</param>
        /// <param name="nSize">Размер строки</param>
        /// <returns>Возвращает длину строки</returns>
        [DllImport("psapi.dll")]
        static extern uint GetModuleBaseName(
            IntPtr hProcess,
            IntPtr hModule,
            StringBuilder lpBaseName,
            uint nSize
            );
        [DllImport("kernel32.dll", SetLastError=true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);


        public static void pauseProcess(IntPtr handle, UInt32 maxPauseMilliseconds, bool isPaused)
        {
            if (!isPaused)
            {
                const UInt32 WAIT_TIMEOUT = 0x00000102;
                const UInt32 WAIT_ABANDONED = 0x00000080;
                const UInt32 WAIT_OBJECT_0 = 0x00000000;

                UInt32 result = WaitForSingleObject(handle, maxPauseMilliseconds);
                if (result == WAIT_TIMEOUT)
                {
                    throw new Exception("Время вышло, но объект до сих пор занят");
                } else if (result == WAIT_ABANDONED) {
                    throw new Exception("Объект не был освобожден потоком, который владел им до своего завершения");
                } else if (result != WAIT_OBJECT_0) {
                    throw new Exception("Ошибка выполнения функции WaitForSingleObject");
                }
                win32API2.CloseHandle(handle);
            }
        }
        public static IntPtr getProcessHandle(String nameSearchProcess)
        {
            UInt32[] processIds = getProcessIds();
            UInt32 currentProcessId = win32API2.GetCurrentProcessId();

            IntPtr tmp;
            String tmpStr;
            for (int i = 0; i < processIds.Length; i++)
            {
                //Если идентификатор процесса равен текущему процессу, то пропускаем шаг
                //if (processIds[i] == currentProcessId) continue;
                //Получаем дескриптор процесса
                tmp = win32API2.OpenProcess((int)win32API2.ProcessAccessFlags.All,
                    false,
                    (int)processIds[i]);
                //Получаем имя процесса
                tmpStr = getProcessName(tmp);
                if (tmpStr.ToLower().Equals(nameSearchProcess.ToLower()))
                {
                    //Если имя процесса совпадает с тем, который мы ищем, то возвращаем дескриптор
                    return tmp;
                }
                else
                {
                    //Закрываем дескриптор
                    win32API2.CloseHandle(tmp);
                }
            }

            throw new Exception("Не найден соответствующий процесс");
        }
        private static UInt32[] getProcessIds()
        {
            UInt32 arraySize = 120;
            UInt32 arrayBytesSize = arraySize * sizeof(UInt32);
            UInt32[] processIds = new UInt32[arraySize];
            UInt32 bytesCopied;

            bool success = EnumProcesses(processIds, arrayBytesSize, out bytesCopied);

            if (success && bytesCopied > 0)
            {
                bytesCopied >>= 2;
                UInt32[] result = new UInt32[bytesCopied];

                for (int i = 0; i < bytesCopied; i++)
                {
                    result[i] = processIds[i];
                }

                return result;
            }
            else
            {
                return new UInt32[0];
            }
        }
        private static String getProcessName(IntPtr handle)
        {
            String result = "";
            UInt32 t = getProcessModule(handle);

            if (t != 0)
            {
                StringBuilder bl = new StringBuilder(255);
                UInt32 size = GetModuleBaseName(handle, new IntPtr(t), bl, 255);
                result = bl.ToString().Substring(0, (int)size);
            }

            return result;
        }
        private static UInt32 getProcessModule(IntPtr handle)
        {
            UInt32 arraySize = 1000;
            UInt32 arrayBytesSize = arraySize * sizeof(UInt32);
            UInt32[] result = new UInt32[arraySize];
            UInt32 bytesCopied;

            EnumProcessModules(handle, result, arrayBytesSize, out bytesCopied);
            bytesCopied >>= 2;

            if (bytesCopied > 0)
            {
                return result[0];
            }
            else
            {
                return 0;
            }
        }
    }
}
