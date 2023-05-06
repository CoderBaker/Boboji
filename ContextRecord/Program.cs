using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.Windows.Forms;

namespace ContextRecord
{
    class Program
    {
        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);

        static void Main(string[] args)
        {
            /* var rootElement = AutomationElement.RootElement;

             // Create a condition to find all instances of Microsoft Edge
             var edgeCondition = new AndCondition(
                 new PropertyCondition(AutomationElement.ProcessIdProperty, 30408),
                 new PropertyCondition(AutomationElement.FrameworkIdProperty, "Win32"));

             // Find all instances of Microsoft Edge
             var edgeElements = rootElement.FindAll(TreeScope.Children, edgeCondition);

             // Iterate over each instance of Microsoft Edge
             foreach (AutomationElement edgeElement in edgeElements)
             {
                 // Create a condition to find all address bars in this instance of Microsoft Edge
                 var addressBarCondition = new AndCondition(
                     new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit),
                     new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));

                 // Find all address bars in this instance of Microsoft Edge
                 var addressBarElements = edgeElement.FindAll(TreeScope.Descendants, addressBarCondition);

                 // Iterate over each address bar element
                 foreach (AutomationElement addressBarElement in addressBarElements)
                 {
                     // Get the URL displayed in this address bar
                     string url = (string)addressBarElement.GetCurrentPropertyValue(ValuePattern.ValueProperty);

                     // Print out the URL
                     Console.WriteLine(url);
                 }
             }*/
            switchTab();
            Console.ReadKey();
        }

        static void switchTab()
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

                string matchUrl = "https://www.bing.com";
                string originalUrl = "http://www.google.com/";
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
