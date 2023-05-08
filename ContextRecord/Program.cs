using ContextRecord.Class;
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
        static void Main(string[] args)
        {
            var edgeBrowserContext = new EdgeBrowserContext("");
            edgeBrowserContext.GetContext();
        }

        
    }
    
}
