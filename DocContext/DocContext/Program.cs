﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocContext
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProcessController processController = new ProcessController();
            processController.StartProcess(args[0]);
        }
    }
}
