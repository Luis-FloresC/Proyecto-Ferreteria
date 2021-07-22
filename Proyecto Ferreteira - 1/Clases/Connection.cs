using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Proyecto_Ferreteira___1.Clases
{
    public class Connection
    {

        private readonly String Connection_st;

        public Connection()
        {
                Connection_st = ConfigurationManager.ConnectionStrings["Proyecto_Ferreteira___1.Properties.Settings.FerreteriaDb"].ConnectionString;
           
        }


  

        /// <summary>
        /// Metodo para obtener la conexion
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            return new SqlConnection(Connection_st);
        }

    }
}
