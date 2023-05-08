namespace ContextRecord.Contexts
{
    /// <summary>
    /// The software context.
    /// </summary>
    /// <typeparam name="T">The type of the context data.</typeparam>
    public abstract class Context<T>
    {
        /// <summary>
        /// The context cache;
        /// </summary>
        protected T? ContextCache { get; set; }

        /// <summary>
        /// Gets the context data and caches it.
        /// </summary>
        /// <returns>The context data.</returns>
        public virtual T GetContext()
        {
            this.ContextCache ??= this.GenerateNewContext();
            return this.ContextCache;
        }

        /// <summary>
        /// Loads context from the context data.
        /// </summary>
        /// <param name="contextData">The context data.</param>
        public virtual void LoadContext(T contextData)
        {
            this.ContextCache = contextData;
        }

        /// <summary>
        /// Recovers context based on the context data cached.
        /// </summary>
        public abstract void RecoverContext();

        /// <summary>
        /// Generates a new context.
        /// </summary>
        protected abstract T GenerateNewContext();
    }
}
