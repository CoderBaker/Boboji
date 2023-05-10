using ContextRecord.Contexts;
using ContextRecord.ContextSerializers;
using ContextRecord.ContextDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextRecord
{
    internal class CmdController
    {
        private EdgeBrowserContext edgeBrowserContext;
        private OverallContext overallContext;

        private const string edgeExtName = "_web";
        private const string overallExtName = "_overall";

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
            string filePath = this.getFilePath();

            IContextSerializer<IEnumerable<EdgeBrowserContextData>> webContextSerializer = new JsonContextSerializer<IEnumerable<EdgeBrowserContextData>>(filePath + edgeExtName);
            IContextSerializer<OverallContextData> overallContextSerializer = new JsonContextSerializer<OverallContextData>(filePath + overallExtName);
            this.overallContext = new OverallContext(overallContextSerializer);
            this.edgeBrowserContext = new EdgeBrowserContext(webContextSerializer);

            this.overallContext.GetContext();
            this.overallContext.SaveContext();
            this.edgeBrowserContext.GetContext();
            this.edgeBrowserContext.SaveContext();
            
        }

        private string getFilePath()
        {
            while (true)
            {
                //Ask user to input the file name and get the input
                Console.WriteLine("Please input the Record name:");
                var recordName = Console.ReadLine();

                //Check the fileName is empty
                if (string.IsNullOrEmpty(recordName))
                {
                    Console.WriteLine("Error: The Record name can not be empty!");
                    continue;
                }

                var filePath = "Record/" + recordName;
                //Check the file is exist
                if (System.IO.File.Exists(filePath + overallContext))
                {
                    Console.WriteLine("Error: Record with this name is exist! Please choose another name:");
                    continue;
                }

                return filePath;
            }
        }

        private void ReadContext()
        {
        }

    }
}
