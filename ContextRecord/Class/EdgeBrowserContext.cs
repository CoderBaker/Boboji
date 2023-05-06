using ContextRecord.Interface;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ContextRecord.Class
{
    internal class EdgeBrowserContext: Context
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeBrowserContext"/> class.
        /// </summary>
        /// <param name="recordFilePath">record file path</param>
        public EdgeBrowserContext(string recordFilePath) : base(recordFilePath)
        {
        }

        /// <summary>
        /// Get and write the context into the file
        /// </summary>
        /// <returns></returns>
        public override void GetContext()
        {
            /// TBD
            this.switchTab();
            return;
        }

        /// <summary>
        /// Write the record into file
        /// </summary>
        protected override void WriteContext()
        {
            /// TBD
            return;
        }

        /// <summary>
        /// Switch the tab in edge and record the url
        /// </summary>
        private void switchTab()
        {
            Process[] procsEdge = System.Diagnostics.Process.GetProcessesByName("msedge");

            foreach (Process proc in procsEdge)
            {
                Windowplacement placement = new Windowplacement();
                GetWindowPlacement(proc.MainWindowHandle, ref placement);

                // Check if window is minimized
                if (placement.showCmd == 2)
                {
                    //the window is hidden so we restore it
                    ShowWindow(proc.MainWindowHandle.ToInt32(), 9);
                }
                //Switch Edge tab to the first one
                SetForegroundWindow(proc.MainWindowHandle);
                SendKeys.SendWait("^1");
                if (proc.MainWindowHandle == IntPtr.Zero)
                    continue;

                int numTabs = procsEdge.Length;
                int index = 1;
                //loop all tabs in Edge
                while (index <= numTabs)
                {
                    //get the url of tab
                    AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                    var SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));
                    if (SearchBar != null)
                    {
                        string str = (string)SearchBar.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty);
                        Console.WriteLine(str);
                    }
                    index++;
                    SendKeys.SendWait("^{TAB}"); // change focus to next tab
                }
            }
        }

        /// <summary>
        /// Not sure why we need this
        /// </summary>
        private struct Windowplacement
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
