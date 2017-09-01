using System;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Threading;

namespace Coating
{
    public class Work
    {
        private int ID;
        private string source;
        private string connectionString;
        private string begin;
        private string done;
        private string error;
        public Work(string _source, int _ID, string _connectionString, string _begin, string _done, string _error)
        {
            source = _source;
            ID = _ID;
            connectionString = _connectionString;
            begin = _begin.Split(';')[0];
            done = _done.Split(';')[0];
            error = _error.Split(';')[0];
        }
        public void DoWork()
        {
            DBCon connection = DBCon.GetConnection(connectionString);
            try
            {
                connection.SetStatus(begin, ID);
                IL3DCDLL temp = new IL3DCDLL();
                temp.TryTransform(source);
                connection.SetStatus(done, ID);
            }
            catch
            {
                connection.SetStatus(error, ID);
            }
            Thread.CurrentThread.Abort();
        }
    }
}
