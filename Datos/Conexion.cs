using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public class Conexion
    {
        private readonly string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Neptuno;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(connectionString);
        }
    }
}
