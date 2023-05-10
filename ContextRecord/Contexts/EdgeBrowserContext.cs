﻿namespace ContextRecord.Contexts
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
            this.LoadContext();
            if (this.ContextCache != null)
            {
                //aggregate the urls from the context according to the edge browser id and iterate the urls according to the edge browser id
                var edgeBrowserIds = this.ContextCache.Select(data => data.EdgeBrowserId).Distinct();

                //Iterate the edge browser ids and select the urls according to the edge browser id
                foreach (var edgeBrowserId in edgeBrowserIds)
                {
                    var urls = this.ContextCache.Where(data => data.EdgeBrowserId == edgeBrowserId).Select(data => data.URL);
                    // start a command line and run the command to open the edge browser
                    var command = $"start msedge {string.Join(",", urls)}";
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c {command}",
                            UseShellExecute = false,
                            CreateNoWindow = true,
                        },
                    };
                    process.Start();
                }
            }
        }

        /// <inheritdoc/>
        protected override IEnumerable<EdgeBrowserContextData> GenerateNewContext()
        {
            return GetTabsAndURLs().Select(pair => new EdgeBrowserContextData() { Title = pair.Item1, URL = pair.Item2,EdgeBrowserId = pair.Item3}).ToList();
        }

        /// <summary>
        /// Gets tab titles and URLs of Edge browser.
        /// </summary>
        /// <returns>A collection of tab titles and URLs.</returns>
        private static IEnumerable<(string, string,int)> GetTabsAndURLs()
        {
            var procsEdge = Process.GetProcessesByName(EdgeProcessName);
            int edgeIndex = 0;
            foreach (var proc in procsEdge)
            {
                var windowPlacement = new User32.WindowPlacement();
                //Get the windows handle by the enum function in user32
                //and iterate every handle to check if the windws placement is minimized
                var handles = User32.EnumerateProcessWindowHandles(proc.Id);
                foreach (var handle in handles)
                {
                    User32.ApiGetWindowPlacement(handle, ref windowPlacement);
                    if (windowPlacement.showCmd == 2)
                    {
                        //the window is hidden so we restore it
                        User32.ApiShowWindow(handle.ToInt32(), 3);
                    }
                    //Switch Edge tab to the first one
                    User32.ApiSetForegroundWindow(handle);
                    SendKeys.SendWait("^1");
                    if (handle == IntPtr.Zero)
                    {
                        continue;
                    }
                    
                    var root = AutomationElement.FromHandle(handle);
                    var SearchBar = root.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));

                    if (SearchBar == null)
                    {
                        continue;
                    }

                    edgeIndex++;

                    foreach (var tabItem in root.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem)).Cast<AutomationElement>())
                    {
                        var title = tabItem.Current.Name;
                        var url = (string)SearchBar.GetCurrentPropertyValue(ValuePatternIdentifiers.ValueProperty);
                        yield return (title, url, edgeIndex);

                        User32.ApiSetForegroundWindow(handle);
                        SendKeys.SendWait("^{TAB}"); // change focus to next tab
                    }
                }
            }
        }
    }
}
