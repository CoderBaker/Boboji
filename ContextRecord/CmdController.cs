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
        private const string edgeExtName = "_web";
        private const string overallExtName = "_overall";
        private const string recordFolder = "Record/";

        /// <summary>
        /// Start the process
        /// </summary>
        public void StartProcess()
        {
            while (true)
            {
                ///Ask user to choose
                Console.WriteLine("*********************************************");
                Console.WriteLine("Please choose by input the number:");
                Console.WriteLine("1: Record the context");
                Console.WriteLine("2: Read the record");
                Console.WriteLine("3: Delete a record");
                Console.WriteLine("*********************************************");
                //Get the input
                var input = Console.ReadLine();
                switch (input)
                {
                    //input = 1, record the context
                    case "1":
                        this.RecordContext();
                        break;
                    //input = 2, read the record
                    case "2":
                        this.ReadContext();
                        break;
                    //input = 3, delete a record
                    case "3":
                        this.DeleteContext();
                        break;
                    //input = 0, exit the process
                    case "0":
                        return;
                }

                //refresh the window
                Console.Clear();
            }
        }

        /// <summary>
        /// Record the context into a json file
        /// </summary>
        private void RecordContext()
        {
            EdgeBrowserContext edgeBrowserContext;
            OverallContext overallContext;
            string filePath = this.getFilePath();

            IContextSerializer<IEnumerable<EdgeBrowserContextData>> webContextSerializer = new JsonContextSerializer<IEnumerable<EdgeBrowserContextData>>(filePath + edgeExtName);
            IContextSerializer<OverallContextData> overallContextSerializer = new JsonContextSerializer<OverallContextData>(filePath + overallExtName);
            overallContext = new OverallContext(overallContextSerializer);
            edgeBrowserContext = new EdgeBrowserContext(webContextSerializer);

            overallContext.GetContext();
            overallContext.SaveContext();
            edgeBrowserContext.GetContext();
            edgeBrowserContext.SaveContext();
            
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

                var filePath = recordFolder + recordName;
                //Check the file is exist
                if (System.IO.File.Exists(filePath + overallExtName))
                {
                    Console.WriteLine("Error: Record with this name is exist! Please choose another name:");
                    continue;
                }

                return filePath;
            }
        }

        /// <summary>
        /// Read the context from a json file
        /// </summary>
        private void ReadContext()
        {
            EdgeBrowserContext edgeBrowserContext;

            string recordName = this.displayAndChooseRecord();

            IContextSerializer<IEnumerable<EdgeBrowserContextData>> webContextSerializer = new JsonContextSerializer<IEnumerable<EdgeBrowserContextData>>(recordFolder + recordName + edgeExtName);
            edgeBrowserContext = new EdgeBrowserContext(webContextSerializer);
            edgeBrowserContext.RecoverContext();
        }

        /// <summary>
        /// Get the user input number
        /// </summary>
        /// <param name="maxLength">File length</param>
        /// <returns>The input number</returns>
        private int GetUserInputNum(int maxLength)
        {
            while (true)
            {
                //Ask user to choose the record
                Console.WriteLine("Please choose the record by input the number:");
                var input = int.Parse(Console.ReadLine());
                //check the input is not larger than the list
                if (input >= maxLength || input < 0)
                {
                    Console.WriteLine("Error: The input is not valid!");
                    continue;
                }

                return input;
            }
        }

        private void DeleteContext()
        {
            string recordName = this.displayAndChooseRecord();
            System.IO.File.Delete(recordFolder + recordName + edgeExtName);
            System.IO.File.Delete(recordFolder + recordName + overallExtName);
        }

        private string displayAndChooseRecord()
        {
            OverallContext overallContext;
            //Scan the record folder and store the file name end with _overview into a list
            string[] files = System.IO.Directory.GetFiles("Record/", "*_overall");

            //Display the file name in the list and cut the _overall, and display the time and description in record
            for (int i = 0; i < files.Length; i++)
            {
                //Get the file name
                string curFileName = System.IO.Path.GetFileName(files[i]);
                //Cut the _overall
                string curRecordName = curFileName.Substring(0, curFileName.Length - 8);
                //Get the time and description
                IContextSerializer<OverallContextData> overallContextSerializer = new JsonContextSerializer<OverallContextData>(recordFolder + curFileName);
                overallContext = new OverallContext(overallContextSerializer);
                OverallContextData overallContextData = overallContext.GetOverallContextData();
                Console.WriteLine(i + ":   " + curRecordName + " --- " + overallContextData.Time + " --- " + overallContextData.Description);
            }

            int input = GetUserInputNum(files.Length);
            string fileName = System.IO.Path.GetFileName(files[input]);
            string recordName = fileName.Substring(0, fileName.Length - 8);

            return recordName;
        }


    }
}
