using System;

namespace ContextRecord.ContextDataStructures
{
    internal class ContextData
    {
        /// <summary>
        /// Create time.
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        string Description { get; set; } = string.Empty;

        /// <summary>
        /// The doc context data.
        /// </summary>
        DocContextData DocContextData { get; set; } = new DocContextData();

        /// <summary>
        /// The edge browser context data.
        /// </summary>
        EdgeBrowserContextData EdgeBrowserContextData { get; set; } = new EdgeBrowserContextData();
    }
}
