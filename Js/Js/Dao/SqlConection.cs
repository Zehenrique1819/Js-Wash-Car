
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Js.Dao
{
    class SqlConection
    {
        private readonly String connectionString;

        public SqlConection()
        {
            connectionString = "server=localhost;database=js;userid=root;password=2303;";
        }

        public MySqlConnection Criar()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
