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
    public class FileTraceListener : TextWriterTraceListener
    {
        public string FilePath { get; set; }
        public FileStream FileStream { get; set; }
        public StreamWriter StreamWriter { get; set; }

        public FileTraceListener(string path)
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
                    FileShare.ReadWrite);
            }
            else
            {
                FileStream = new FileStream(
                    FilePath,
                    FileMode.Create,
                    FileAccess.ReadWrite,
                    FileShare.ReadWrite);
            }


            StreamWriter = new StreamWriter(FileStream);
            StreamWriter.AutoFlush = true;
            base.Writer = StreamWriter;

            StreamWriter.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            StreamWriter.WriteLine(string.Format("Initializing FileTraceListener on log path : {0} ({1})", FilePath, DateTime.Now));
            StreamWriter.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

        }

        private void GenerateLogFilePath(string path)
        {
            DateTime now = DateTime.Now;
            FilePath = System.IO.Path.Combine(path, string.Format(LogConfiguration.Configuration.PlainTextFileFormat, now));
        }

        public string GetLogContent()
        {
            //FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            string ret = null;

            long currentPosition = FileStream.Position;
            FileStream.Position = 0;

            StreamReader sr = new StreamReader(FileStream);

            ret = sr.ReadToEnd();

            FileStream.Position = currentPosition;

            return ret;
        }

        public override void Write(string message)
        {
            //StreamWriter.Write(message);
        }

        public override void WriteLine(string message)
        {
            StreamWriter.WriteLine(message);
        }
    }
}
