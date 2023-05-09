using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextRecord
{
    internal class CmdController
    {
        /// <summary>
        /// Start the process
        /// </summary>
        public void StartProcess()
        {
            ///Ask user to choose
            Console.WriteLine("Please choose by input the number:");
            Console.WriteLine("1: Record the context");
            Console.WriteLine("2: Read the record");

            //Get the input
            var input = Console.ReadLine();
            switch (input)
            {
                //input = 1, record the context
                case "1":
                    RecordContext();
                    break;
                //input = 2, read the record
                case "2":
                    ReadContext();
                    break;
            }
        }

        /// <summary>
        /// Record the context into a json file
        /// </summary>
        private void RecordContext()
        {
            string fileName = "";
            string filePath = "RecordFile/";
            string description = "";

            while (true)
            {
                //Ask user to input the file name and get the input
                Console.WriteLine("Please input the Record name:");
                fileName = Console.ReadLine();

                //Check the fileName is empty
                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Error: The Record name can not be empty!");
                    continue;
                }

                //Check the file is exist
                if (System.IO.File.Exists(filePath + fileName))
                {
                    Console.WriteLine("Error: Record with this name is exist!");
                    continue;
                }

                filePath = filePath + fileName;

                break;
            }
            
            //Ask user to input the description and get the input
            Console.WriteLine("Please input the description:");
            description = Console.ReadLine();

            //Create a new Json file with name of filePath
            var file = System.IO.File.Create(filePath);
            
        }

        private void ReadContext()
        {
        }
        
    }
}
