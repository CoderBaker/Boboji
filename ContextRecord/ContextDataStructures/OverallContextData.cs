namespace ContextRecord.ContextDataStructures
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The context data of edge browser.
    /// </summary>
    public record OverallContextData
    {
        /// <summary>
        /// The title.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// The URL.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
