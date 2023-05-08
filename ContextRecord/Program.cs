using ContextRecord.Contexts;

namespace ContextRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            var edgeBrowserContext = new EdgeBrowserContext();
            edgeBrowserContext.GetContext();
        }        
    }
}
