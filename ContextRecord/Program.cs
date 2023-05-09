using System;
using System.Linq;
using ContextRecord.Contexts;

namespace ContextRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmdController = new CmdController();
            cmdController.StartProcess();
        }        
    }
}
