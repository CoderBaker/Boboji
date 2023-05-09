using System.Security.Cryptography.Xml;

namespace ContextRecord.ContextDataStructures
{
    /// <summary>
    /// The context data of edge browser.
    /// </summary>
    public record EdgeBrowserContextData
    {
        //The list that contains the web data.
        public singleWebData[] WebDataList { get; set; } = new singleWebData[0];
    }

    public record singleWebData
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
