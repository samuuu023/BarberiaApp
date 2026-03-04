using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Barberia.AccesoDatos
{

        public class ConexionBD
        {
            private readonly string cadena =
            "Data Source=DESKTOP-IB4C7AU\\SQLEXPRESS;Initial Catalog=BarberiaDB;Integrated Security=True;TrustServerCertificate=True;";

            public SqlConnection ObtenerConexion()
            {
                return new SqlConnection(cadena);
            }
        }
    }

