using System;
using System.Linq;
using ContextRecord.Contexts;

namespace ContextRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            var edgeBrowserContext = new EdgeBrowserContext();
            var context = edgeBrowserContext.GetContext();
            Console.WriteLine(string.Join(Environment.NewLine, context.Select(x => $"Title: {x.Title} URL: {x.URL} ID: {x.EdgeBrowserId}")));
        }        
    }
}
