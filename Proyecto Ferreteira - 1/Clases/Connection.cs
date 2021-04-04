using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Proyecto_Ferreteira___1.Clases
{
    public class Connection
    {

        private readonly String Connection_st;

        public Connection()
        {
            Connection_st = ConfigurationManager.ConnectionStrings["Proyecto_Ferreteira___1.Properties.Settings.FerreteriaDb"].ConnectionString;

        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(Connection_st);
        }

    }
}
