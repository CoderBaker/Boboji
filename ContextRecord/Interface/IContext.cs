using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextRecord.Interface
{
    internal interface IContext
    {
        // Return a list of context
        // For browser, return list of urls
        // For doc, return list of doc path
        void GetContext();
    }
}
