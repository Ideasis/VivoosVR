using Core.Log.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log.Listeners
{
    public class XmlTraceListener : XmlWriterTraceListener
    {
        public string FilePath { get; set; }
        public FileStream FileStream { get; set; }
        public StreamWriter StreamWriter { get; set; }

        public XmlTraceListener(string path)
                    : base(path)
        {
            string pathOnly = Path.GetDirectoryName(path);
            Directory.CreateDirectory(pathOnly);
            GenerateLogFilePath(pathOnly);

            if (File.Exists(FilePath))
            {
                FileStream = new FileStream(
                    FilePath,
                    FileMode.Append,
                    FileAccess.Write,
                    FileShare.Read);
            }
            else
            {
                FileStream = new FileStream(
                    FilePath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.Read);
            }

            StreamWriter = new StreamWriter(FileStream);
            StreamWriter.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            StreamWriter.WriteLine(string.Format("Initializing XmlTraceListener on log path : {0} ({1})", FilePath, DateTime.Now));
            StreamWriter.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

            StreamWriter = new StreamWriter(FileStream);
            StreamWriter.AutoFlush = true;
            base.Writer = StreamWriter;
        }

        private void GenerateLogFilePath(string path)
        {
            DateTime now = DateTime.Now;
            FilePath = System.IO.Path.Combine(path, string.Format(LogConfiguration.Configuration.XmlFileFormat, now));
        }

        public override void Write(string message)
        {
            StreamWriter.Write(message);
        }

        public override void WriteLine(string message)
        {
            StreamWriter.WriteLine(message);
        }
    }
}
