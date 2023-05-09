using System.Collections.Generic;
using ContextRecord.ContextDataStructures;
using ContextRecord.Contexts;
using ContextRecord.ContextSerializers;

namespace ContextRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            var edgeBrowserContext = new EdgeBrowserContext(new JsonContextSerializer<IEnumerable<EdgeBrowserContextData>>("edge_context.json"));
            edgeBrowserContext.GetContext();
            edgeBrowserContext.SaveContext();
        }        
    }
}
