using ContextRecord.ContextDataStructures;
using ContextRecord.ContextSerializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace ContextRecord.Contexts
{
    public class DocContext: Context<IEnumerable<DocContextData>>
    {
        public DocContext(IContextSerializer<IEnumerable<DocContextData>> serializer)
            : base(serializer)
        {
        }
        
        public override void RecoverContext()
        {
            this.LoadContext();

            //Open all the files in the context. If the type is word, open the file with winword.exe. If the type is ppt, open the file with powerpnt.exe. If the type is excel, open the file with excel.exe.
            if (this.ContextCache != null)
            {
                foreach (var docContextData in this.ContextCache)
                {
                    if (docContextData.Type == "Word")
                    {
                        //start a command line and run the command to open the word file
                        var command = $"start winword.exe \"\"\"{docContextData.Path}\"\"\"";
                        var process = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = "cmd.exe",
                                Arguments = $"/c {command}",
                                UseShellExecute = false,
                                CreateNoWindow = true,
                            },
                        };
                        process.Start();
                    }
                    else if (docContextData.Type == "PowerPoint")
                    {
                        //start a command line and run the command to open the ppt file
                        var command = $"start powerpnt.exe \"\"\"{docContextData.Path}\"\"\"";
                        var process = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = "cmd.exe",
                                Arguments = $"/c {command}",
                                UseShellExecute = false,
                                CreateNoWindow = true,
                            },
                        };
                        process.Start();
                    }
                    else if (docContextData.Type == "Excel")
                    {
                        //start a command line and run the command to open the excel file
                        var command = $"start excel.exe \"\"\"{docContextData.Path}\"\"\"";
                        var process = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = "cmd.exe",
                                Arguments = $"/c {command}",
                                UseShellExecute = false,
                                CreateNoWindow = true,
                            },
                        };
                        process.Start();
                    }
                }
            }            
        }

        public void writeContextByExe(string filePath)
        {
            //Call DocContext.exe and set the parameter as filePath
            System.Diagnostics.Process.Start("DocContext.exe", filePath);
        }

        public IEnumerable<DocContextData> GetDocContextData()
        {
            this.LoadContext();
            return this.ContextCache;
        }

        /// <summary>
        /// Generates a new context.
        /// </summary>
        protected override IEnumerable<DocContextData> GenerateNewContext()
        {
            return new List<DocContextData>();
        }
    }
}
