namespace ContextRecord.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Automation;
    using System.Windows.Forms;
    using ContextRecord.ContextDataStructures;
    using ContextRecord.ContextSerializers;
    using ContextRecord.Win32ApiUtils;

    internal class EdgeBrowserContext : Context<IEnumerable<EdgeBrowserContextData>>
    {
        /// <summary>
        /// The process name of Edge.
        /// </summary>
        private const string EdgeProcessName = "msedge";

        public EdgeBrowserContext(IContextSerializer<IEnumerable<EdgeBrowserContextData>> serializer)
            : base(serializer)
        {
        }

        /// <inheritdoc/>
        public override void RecoverContext()
        {
            if (this.ContextCache != null)
            {
                foreach (var url in this.ContextCache.Select(x => x.URL))
                {
                    Process.Start(EdgeProcessName, url);
                }
            }
        }

        /// <inheritdoc/>
        protected override IEnumerable<EdgeBrowserContextData> GenerateNewContext()
        {
            /// TBD
            return GetTabsAndURLs().Select(pair => new EdgeBrowserContextData() { Title = pair.Item1, URL = pair.Item2 }).ToList();
        }

        /// <summary>
        /// Gets tab titles and URLs of Edge browser.
        /// </summary>
        /// <returns>A collection of tab titles and URLs.</returns>
        private static IEnumerable<(string, string)> GetTabsAndURLs()
        {
            var procsEdge = Process.GetProcessesByName(EdgeProcessName);

            foreach (var proc in procsEdge)
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
                {
                    continue;
                }

                var root = AutomationElement.FromHandle(proc.MainWindowHandle);
                foreach (var tabItem in root.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem)).Cast<AutomationElement>())
                {
                    var SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));

                    var title = tabItem.Current.Name;
                    var url = (string)SearchBar.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty);
                    yield return (title, url);

                    User32.ApiSetForegroundWindow(proc.MainWindowHandle);
                    SendKeys.SendWait("^{TAB}"); // change focus to next tab
                }
            }
        }
    }
}
