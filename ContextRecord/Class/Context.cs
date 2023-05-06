using ContextRecord.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextRecord.Class
{
    public class Context: IContext
    {
        /// <summary>
        /// Path of the file that used to store the context
        /// </summary>
        public string recordFilePath = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="recordFilePath">record file path</param>
        public Context(string recordFilePath)
        {
            this.recordFilePath = recordFilePath;
        }

        /// <summary>
        /// Get and write the context into the file
        /// </summary>
        public virtual void GetContext()
        {
            return ;
        }

        /// <summary>
        /// Write the record into file
        /// </summary>
        protected virtual void WriteContext()
        {
            return;
        }
    }
}
