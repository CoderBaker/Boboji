using DocContext.ContextDataStructures;
using DocContext.ContextSerializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using System.Diagnostics;

namespace DocContext.Contexts
{
    internal class DocumentContext : Context<IEnumerable<DocContextData>>
    {
        private const string wordDocProcessName = "WINWORD";
        private const string pptDocProcessName = "POWERPNT";
        private const string excelDocProcessName = "EXCEL";
        private const string excelDocAPPName = "Excel.Application";
        private const string pptDocAPPName = "PowerPoint.Application";
        private const string wordDocAPPName = "Word.Application";

        public DocumentContext(IContextSerializer<IEnumerable<DocContextData>> serializer)
            : base(serializer)
        {
        }

        /// <inheritdoc/>
        public override void RecoverContext()
        {
        }

        /// <summary>
        /// Generates a new context.
        /// </summary>
        protected override IEnumerable<DocContextData> GenerateNewContext()
        {
            return GetDocPaths().Select(pair => new DocContextData() { Title = pair.Item1, Type = pair.Item2, Path = pair.Item3 }).ToList();
        }

        /// <summary>
        /// Gets tab titles and URLs of Edge browser.
        /// </summary>
        /// <returns>A collection of tab titles and URLs.</returns>
        private static IEnumerable<(string, string, string)> GetDocPaths()
        {
            IEnumerable<(string, string, string)> docPaths = new List<(string, string, string)>();
            docPaths = docPaths.Concat(GetDocPathsByProcess(wordDocProcessName, wordDocAPPName));
            docPaths = docPaths.Concat(GetDocPathsByProcess(pptDocProcessName, pptDocAPPName));
            docPaths = docPaths.Concat(GetDocPathsByProcess(excelDocProcessName, excelDocAPPName));

            return docPaths;
        }

        private static IEnumerable<(string, string, string)> GetDocPathsByProcess(string processName, string appName)
        {
            dynamic app;
            string appType;
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length <= 0)
            {
                yield break;
            }

            object appAsObject = System.Runtime.InteropServices.Marshal.GetActiveObject(appName);
            app = appAsObject;

            if (appName == "Word.Application")
            {
                appType = "Word";
                foreach (Word.Document doc in app.Documents)
                {
                    yield return (doc.Name, appType, doc.FullName);
                }
            }
            else if (appName == "Excel.Application")
            {
                appType = "Excel";
                foreach (Excel.Workbook book in app.Workbooks)
                {
                    yield return (book.Name, appType, book.FullName);
                }
            }
            else if (appName == "PowerPoint.Application")
            {
                appType = "PowerPoint";
                foreach (PowerPoint.Presentation pres in app.Presentations)
                {
                    yield return (pres.Name, appType, pres.FullName);
                }
            }
        }

    }   
}
