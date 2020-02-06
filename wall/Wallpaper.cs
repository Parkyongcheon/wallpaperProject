using System;
using System.Windows.Forms;

using System.Security.Principal;
using Microsoft.Win32;
using System.Threading;

namespace wall
{
    public static class Wallpaper
    {
        public static bool Background(IntPtr ptrFormHandle)
        {
            IntPtr ptrProgman = WinApi.FindWindow("Progman", null);

            if (ptrProgman == IntPtr.Zero)
                return false;

            IntPtr ptrWorkerW = IntPtr.Zero;

            // while (true)
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    IntPtr ptrResult = IntPtr.Zero;

                    WinApi.SendMessageTimeout(ptrProgman,
                        0x052C,
                        new IntPtr(0),
                        IntPtr.Zero,
                        WinApi.SendMessageTimeoutFlags.SMTO_NORMAL,
                        10000,
                        out ptrResult);

                    WinApi.EnumWindows(new WinApi.EnumWindowsProc((topHandle, topParamHandle) =>
                    {
                        IntPtr ptr = WinApi.FindWindowEx(topHandle,
                            IntPtr.Zero,
                            "SHELLDLL_DefView",
                            IntPtr.Zero);

                        if (ptr != IntPtr.Zero)
                        {
                            ptrWorkerW = WinApi.FindWindowEx(IntPtr.Zero,
                                topHandle,
                                "WorkerW",
                                IntPtr.Zero);
                        }

                        return true;
                    }), IntPtr.Zero);

                    if (ptrWorkerW == IntPtr.Zero)
                    {
                        Thread.Sleep(100);
                    }
                    else
                        break;
                }
                
            }

            if (ptrWorkerW == IntPtr.Zero)
                return false;

            // Hide
            WinApi.ShowWindow(ptrWorkerW, 0);
            WinApi.SetParent(ptrFormHandle, ptrProgman);

            return true;
        }
    }
}