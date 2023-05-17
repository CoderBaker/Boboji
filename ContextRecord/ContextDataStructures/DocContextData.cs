using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextRecord.ContextDataStructures
{
    public record DocContextData
    {
        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The Type, Word, PPT, Excel.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The edge browser id
        /// </summary>
        public string Path { get; set; } = string.Empty;
    }
}
