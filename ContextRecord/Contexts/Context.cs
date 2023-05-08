namespace ContextRecord.Contexts
{
    using System.Collections.Generic;

    /// <summary>
    /// The software context.
    /// </summary>
    /// <typeparam name="T">The type of the context data.</typeparam>
    public abstract class Context<T>
    {
        /// <summary>
        /// Get and write the context into the file
        /// </summary>
        /// <returns>The context data.</returns>
        public abstract T GetContext();

        /// <summary>
        /// Loads context from the context data.
        /// </summary>
        /// <param name="contextData">The context data.</param>
        public abstract void LoadContext(T contextData);
    }
}
