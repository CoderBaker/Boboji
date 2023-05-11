namespace DocContext.ContextSerializers
{
    /// <summary>
    /// The context serializer.
    /// </summary>
    /// <typeparam name="T">The type of the context data.</typeparam>
    public interface IContextSerializer<T>
    {
        /// <summary>
        /// Loads context data from the storage.
        /// </summary>
        /// <returns>The context data.</returns>
        T? LoadContextData();

        /// <summary>
        /// Saves context data to the storage.
        /// </summary>
        /// <param name="contextData">The context data.</param>
        void SaveContextData(T? contextData);
    }
}
