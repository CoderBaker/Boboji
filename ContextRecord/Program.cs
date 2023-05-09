using System;
using System.Linq;
using ContextRecord.Contexts;

namespace ContextRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                string type = args[0];
                string description = args[1];
                // Your code here

                switch (type)
                {
                    //If the type is 'Record', print 'Recording the context...'
                    case "Record":
                        Console.WriteLine("Recording the context...");
                        break;
                    //If the type is 'Read', print 'Read the context...'
                    case "Read":
                        Console.WriteLine("Read the context...");
                        break;
                }

                //Print the description
                Console.WriteLine(description);
            }
            else
            {
                Console.WriteLine("Please provide two arguments: type and description.");
            }
        }        
    }
}
