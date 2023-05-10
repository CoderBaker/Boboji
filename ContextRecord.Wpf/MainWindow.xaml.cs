using System;
using System.Collections.Generic;
using System.Windows;
using ContextRecord.ContextDataStructures;
using ContextRecord.Contexts;
using ContextRecord.ContextSerializers;

namespace ContextRecord.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string edgeExtName = "_web";
        private const string overallExtName = "_overall";
        private const string recordFolder = "Record/";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSaveContext_Click(object sender, RoutedEventArgs e)
        {
            RecordContext();
        }

        private void ButtonLoadRecord_Click(object sender, RoutedEventArgs e)
        {
            ReadContext();
        }

        private void ButtonDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            DeleteContext();
        }

        /// <summary>
        /// Record the context into a json file
        /// </summary>
        private static void RecordContext()
        {
            EdgeBrowserContext edgeBrowserContext;
            OverallContext overallContext;
            string filePath = GetFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            IContextSerializer<IEnumerable<EdgeBrowserContextData>> webContextSerializer = new JsonContextSerializer<IEnumerable<EdgeBrowserContextData>>(filePath + edgeExtName);
            IContextSerializer<OverallContextData> overallContextSerializer = new JsonContextSerializer<OverallContextData>(filePath + overallExtName);
            overallContext = new OverallContext(overallContextSerializer);
            edgeBrowserContext = new EdgeBrowserContext(webContextSerializer);

            overallContext.GetContext();
            overallContext.SaveContext();
            edgeBrowserContext.GetContext();
            edgeBrowserContext.SaveContext();

        }

        private static string GetFilePath()
        {
            while (true)
            {
                //Ask user to input the file name and get the input
                Console.WriteLine("Please input the Record name:");
                var recordNameInputWindow = new RecordNameInputWindow();
                if (!(recordNameInputWindow.ShowDialog() ?? false))
                {
                    return string.Empty;
                }

                var recordName = recordNameInputWindow.RecordName;

                //Check the fileName is empty
                if (string.IsNullOrEmpty(recordName))
                {
                    MessageBox.Show("Error: The Record name can not be empty!", "Context Record", MessageBoxButton.OK, MessageBoxImage.Error);
                    continue;
                }

                var filePath = "Record/" + recordName;
                //Check the file is exist
                if (System.IO.File.Exists(filePath + overallExtName))
                {
                    MessageBox.Show("Error: Record with this name exists! Please choose another name.", "Context Record", MessageBoxButton.OK, MessageBoxImage.Error);
                    continue;
                }

                return filePath;
            }
        }

        /// <summary>
        /// Read the context from a json file
        /// </summary>
        private static void ReadContext()
        {
            EdgeBrowserContext edgeBrowserContext;

            string recordName = DisplayAndChooseRecord();

            IContextSerializer<IEnumerable<EdgeBrowserContextData>> webContextSerializer = new JsonContextSerializer<IEnumerable<EdgeBrowserContextData>>(recordFolder + recordName + edgeExtName);
            edgeBrowserContext = new EdgeBrowserContext(webContextSerializer);
            edgeBrowserContext.RecoverContext();
        }

        /// <summary>
        /// Get the user input number
        /// </summary>
        /// <param name="maxLength">File length</param>
        /// <returns>The input number</returns>
        private static int GetUserInputNum(int maxLength)
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

        private static void DeleteContext()
        {
            string recordName = DisplayAndChooseRecord();
            System.IO.File.Delete(recordFolder + recordName + edgeExtName);
            System.IO.File.Delete(recordFolder + recordName + overallExtName);
        }

        private static string DisplayAndChooseRecord()
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
