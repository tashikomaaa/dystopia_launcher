using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace The_Dystopia
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "gspdb-chi.hosthavoc.com";
            int port = 3306;
            string database = "db90843";
            string username = "db90843";
            string password = "HqjuIFA6u3";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
