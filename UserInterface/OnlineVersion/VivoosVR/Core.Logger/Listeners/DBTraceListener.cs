using Core.Log.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Core.Log.Listeners
{
    public class DBTraceListener : TextWriterTraceListener
    {
        public SqlConnection Connection { get; set; }

        public DBTraceListener()
        {
            
        }

        public DBTraceListener(string fileName) : base(fileName)
        {
            Connection = new SqlConnection(fileName);
        }

        public override void Write(string message)
        {
            try
            {
                Connection.Open();
                SqlCommand cmd = new SqlCommand("insert into Core.Logs (DateTime, Data) values (getdate(), @data)", Connection);
                var p = cmd.CreateParameter();
                p.DbType = System.Data.DbType.String;
                p.ParameterName = "data";
                p.Value = message;
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();
                Connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void WriteLine(string message)
        {
            Write(message);
        }
    }
}
