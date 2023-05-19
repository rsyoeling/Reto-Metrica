using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloDAO.Config
{
    public class ConexionDA
    {
        /*
            CREATE USER sede_cliente IDENTIFIED BY sede_cliente;
            GRANT ALL PRIVILEGES TO sede_cliente IDENTIFIED BY
            sede_cliente;
         */
        public OracleConnection GetSqlConnection()
        {
            string connectionString = "Data Source=localhost;User Id=sede_cliente;Password=sede_cliente;";
            var connection = new OracleConnection(connectionString);
            return connection;
        }

        public OracleConnection Open()
        {
            var connection = GetSqlConnection();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }

        public OracleConnection Close()
        {
            var connection = GetSqlConnection();
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

            return connection;
        }
    }
}
