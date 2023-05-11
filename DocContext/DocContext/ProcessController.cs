using DocContext.ContextDataStructures;
using DocContext.Contexts;
using DocContext.ContextSerializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocContext
{
    internal class ProcessController
    {
        private const string docExtName = "_doc";

        /// <summary>
        /// Start the process
        /// </summary>
        public void StartProcess(string recordName)
        {
            this.RecordContext(recordName);
        }

        private void RecordContext(string recordName)
        {
            IContextSerializer<IEnumerable<DocContextData>> webContextSerializer = new JsonContextSerializer<IEnumerable<DocContextData>>(recordName + docExtName);
            var webContext = new DocumentContext(webContextSerializer);
            webContext.GetContext();
            webContext.SaveContext();
        }
    }
}
