using ContextRecord.ContextDataStructures;
using ContextRecord.ContextSerializers;
using System;

namespace ContextRecord.Contexts
{
    public class OverallContext : Context<OverallContextData>
    {
        public OverallContext(IContextSerializer<OverallContextData> serializer)
            : base(serializer)
        {
        }

        /// <inheritdoc/>
        public override void RecoverContext()
        {
        }

        public OverallContextData GetOverallContextData()
        {
            this.LoadContext();
            return this.ContextCache;
        }

        /// <summary>
        /// Generates a new context.
        /// </summary>
        protected override OverallContextData GenerateNewContext()
        {
            return new OverallContextData
            {
                Time = DateTime.Now,
                Description = this.getDescription(),
            };
        }

        /// <summary>
        /// Get the description of the context
        /// </summary>
        /// <returns>description</returns>
        private string getDescription()
        {
            //Ask user to input the description and get the input
            Console.WriteLine("Please input the description:");
            string description = Console.ReadLine();
            return description;
        }


    }
}
