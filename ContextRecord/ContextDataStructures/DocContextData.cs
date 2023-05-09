namespace ContextRecord.ContextDataStructures
{
    public record DocContextData
    {
        //The list that contains the doc data.
        public SingleDocData[] DocDataList { get; set; } = new SingleDocData[0];
    }

    public record SingleDocData
    {
        /// <summary>
        /// The title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The Type. PPT, Word, Excel, PDF, etc.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The URL.
        /// </summary>
        public string Path { get; set; } = string.Empty;
    }

}
