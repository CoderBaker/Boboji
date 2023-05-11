using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextRecord.Wpf.DataStructures
{
    public record Record
    {
        public string Name { get; set; } = string.Empty;
        
        public DateTime Time { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
