using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ContextRecord.ContextDataStructures;
using ContextRecord.Contexts;
using ContextRecord.ContextSerializers;
using ContextRecord.Wpf.DataStructures;
using ContextRecord.Wpf.ViewModels;

namespace ContextRecord.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string edgeExtName = "_web";
        private const string overallExtName = "_overall";
        private const string docExtName = "_doc";
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
            DocContext docContext;
            string filePath = GetFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            IContextSerializer<IEnumerable<EdgeBrowserContextData>> webContextSerializer = new JsonContextSerializer<IEnumerable<EdgeBrowserContextData>>(filePath + edgeExtName);
            IContextSerializer<OverallContextData> overallContextSerializer = new JsonContextSerializer<OverallContextData>(filePath + overallExtName);
            IContextSerializer<IEnumerable<DocContextData>> docContextSerializer = new JsonContextSerializer<IEnumerable<DocContextData>>(filePath + docExtName);
            overallContext = new OverallContext(overallContextSerializer);
            edgeBrowserContext = new EdgeBrowserContext(webContextSerializer);
            docContext = new DocContext(docContextSerializer);

            overallContext.GetContext();
            overallContext.SaveContext();
            edgeBrowserContext.GetContext();
            edgeBrowserContext.SaveContext();

            docContext.writeContextByExe(filePath);

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

                var recordName = ((RecordNameInputViewModel)recordNameInputWindow.DataContext).RecordName;

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
        /// Read the context from a JSON file.
        /// </summary>
        private static void ReadContext()
        {
            EdgeBrowserContext edgeBrowserContext;
            DocContext docContext;

            var recordName = DisplayAndChooseRecord();
            if (string.IsNullOrEmpty(recordName))
            {
                return;
            }

            IContextSerializer<IEnumerable<EdgeBrowserContextData>> webContextSerializer = new JsonContextSerializer<IEnumerable<EdgeBrowserContextData>>(recordFolder + recordName + edgeExtName);
            IContextSerializer<IEnumerable<DocContextData>> docContextSerializer = new JsonContextSerializer<IEnumerable<DocContextData>>(recordFolder + recordName + docExtName);
            edgeBrowserContext = new EdgeBrowserContext(webContextSerializer);
            docContext = new DocContext(docContextSerializer);
            edgeBrowserContext.RecoverContext();
            docContext.RecoverContext();
        }

        /// <summary>
        /// Get the user input for records.
        /// </summary>
        /// <param name="records">Records.</param>
        /// <returns>The record chosen.</returns>
        private static Record? GetUserInput(IEnumerable<Record> records)
        {
            while (true)
            {
                //Ask user to choose the record
                var dialog = new RecordSelectorWindow();
                ((RecordSelectorViewModel)dialog.DataContext).Records.Clear();
                foreach (var record in records)
                {
                    ((RecordSelectorViewModel)dialog.DataContext).Records.Add(record);
                }

                var result = dialog.ShowDialog() ?? false;
                if (!result)
                {
                    return null;
                }

                return ((RecordSelectorViewModel)dialog.DataContext).SelectedItem;
            }
        }

        private static void DeleteContext()
        {
            var recordName = DisplayAndChooseRecord();
            if(string.IsNullOrEmpty(recordName))
            {
                return;
            }

            System.IO.File.Delete(recordFolder + recordName + edgeExtName);
            System.IO.File.Delete(recordFolder + recordName + overallExtName);
        }

        private static string? DisplayAndChooseRecord()
        {
            OverallContext overallContext;
            //Scan the record folder and store the file name end with _overview into a list
            string[] files = System.IO.Directory.GetFiles("Record/", "*_overall");

            //Display the file name in the list and cut the _overall, and display the time and description in record
            var records = files.Select(x =>
            {
                //Get the file name
                string curFileName = System.IO.Path.GetFileName(x);
                //Cut the _overall
                string curRecordName = curFileName[..^8];
                //Get the time and description
                IContextSerializer<OverallContextData> overallContextSerializer = new JsonContextSerializer<OverallContextData>(recordFolder + curFileName);
                overallContext = new OverallContext(overallContextSerializer);
                OverallContextData overallContextData = overallContext.GetOverallContextData();
                return new Record()
                {
                    Name = curRecordName,
                    Path = x,
                    Time = overallContextData.Time,
                    Description = overallContextData.Description,
                };
            }).ToList();

            var input = GetUserInput(records);
            if (input == null)
            {
                return null;
            }

            string fileName = System.IO.Path.GetFileName(input.Path);
            string recordName = fileName[..^8];

            return recordName;
        }
    }
}
