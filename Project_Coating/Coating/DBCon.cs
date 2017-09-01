using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;

namespace Coating
{
    public class DBCon
    {
        private static string connectionString;
        private static SqlConnection db;
        private static DBCon con;
        private DBCon(string con)
        {
            db = new SqlConnection(connectionString);
            db.Open();
        }
        public static DBCon GetConnection(string _connectionString)
        {
            if(con == null || connectionString != _connectionString)
            {
                con = new DBCon(_connectionString);
            }
            return con;
        }
        public SqlCommand TableFillingCommand()
        {
            SqlCommand select = new SqlCommand("SELECT ID, FileName, Status FROM MainTable ORDER BY DataAdd, Status DESC", db);
            return select;
        }
        public void SetStatus(string status, int ID)
        {
            SqlCommand start = new SqlCommand(string.Format("UPDATE MainTable SET Status = {1} WHERE ID = {0}", ID, status), db);
            start.ExecuteNonQuery();
        }
        public int Insert(string name, string number)
        {
                string insCommand = string.Format("INSERT INTO MainTable ([FileFolder],[FileName])VALUES ('{0}','{1}') SELECT @@identity", number, name);
                SqlCommand ins = new SqlCommand(insCommand, db);
            return Convert.ToInt32(ins.ExecuteScalar()); 
        }
        ~DBCon()
        {
            db.Close();
        }
    }
}
