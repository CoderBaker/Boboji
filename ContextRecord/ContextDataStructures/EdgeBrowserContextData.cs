namespace ContextRecord.ContextDataStructures
{
    /// <summary>
    /// The context data of edge browser.
    /// </summary>
    public record EdgeBrowserContextData
    {
        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The URL.
        /// </summary>
        public string URL { get; set; } = string.Empty;
    }
}
