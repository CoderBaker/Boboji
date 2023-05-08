namespace ContextRecord.Contexts
{
    using System;
    using System.Diagnostics;
    using System.Windows.Automation;
    using System.Windows.Forms;
    using ContextRecord.ContextDataStructures;
    using ContextRecord.Win32ApiUtils;

    internal class EdgeBrowserContext : Context<EdgeBrowserContextData>
    {
        /// <summary>
        /// The process name of Edge.
        /// </summary>
        private const string EdgeProcessName = "msedge";

        /// <inheritdoc/>
        public override EdgeBrowserContextData GetContext()
        {
            /// TBD
            this.SwitchTab();
            return new EdgeBrowserContextData();
        }

        /// <inheritdoc/>
        public override void LoadContext(EdgeBrowserContextData contextData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Switch the tab in edge and record the url
        /// </summary>
        private void SwitchTab()
        {
            Process[] procsEdge = Process.GetProcessesByName(EdgeProcessName);

            foreach (Process proc in procsEdge)
            {
                var windowPlacement = new User32.WindowPlacement();
                User32.ApiGetWindowPlacement(proc.MainWindowHandle, ref windowPlacement);

                // Check if window is minimized
                if (windowPlacement.showCmd == 2)
                {
                    //the window is hidden so we restore it
                    User32.ApiShowWindow(proc.MainWindowHandle.ToInt32(), 9);
                }
                //Switch Edge tab to the first one
                User32.ApiSetForegroundWindow(proc.MainWindowHandle);
                SendKeys.SendWait("^1");
                if (proc.MainWindowHandle == IntPtr.Zero)
                    continue;

                int numTabs = procsEdge.Length;
                int index = 1;
                //loop all tabs in Edge
                while (index <= numTabs)
                {
                    //get the url of tab
                    var root = AutomationElement.FromHandle(proc.MainWindowHandle);
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
    }
}
