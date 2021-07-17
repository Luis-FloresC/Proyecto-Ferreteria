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
            if(VerificarConexion())
            {
                Connection_st = ConfigurationManager.ConnectionStrings["Proyecto_Ferreteira___1.Properties.Settings.FerreteriaDb"].ConnectionString;
            }
            else
            {
                Connection_st = ConfigurationManager.ConnectionStrings["Proyecto_Ferreteira___1.Properties.Settings.FerreteriaDb2"].ConnectionString;
            }
        }


        /// <summary>
        /// Metodo para verificar la conexion
        /// </summary>
        /// <returns></returns>
        public bool VerificarConexion()
        {

            string connetionString = null;
            SqlConnection cnn;
            connetionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=Ferreteria;Integrated Security=True";

            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }

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
