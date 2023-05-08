using System;
using System.Runtime.InteropServices;

namespace ContextRecord.Win32ApiUtils
{
    /// <summary>
    /// The user32.dll APIs.
    /// </summary>
    public static class User32
    {
        public static int ApiSetForegroundWindow(IntPtr point)
        {
            return SetForegroundWindow(point);
        }

        public static int ApiShowWindow(int hwnd, int nCmdShow)
        {
            return ShowWindow(hwnd, nCmdShow);
        }

        public static bool ApiGetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl)
        {
            return GetWindowPlacement(hWnd, ref lpwndpl);
        }

        [DllImport("User32.dll")]
        private static extern int SetForegroundWindow(IntPtr point);

        [DllImport("User32")]
        private static extern int ShowWindow(int hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

        public struct WindowPlacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }
    }
}
