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
    class Connection
    {

        private readonly String Connection_st;

        public Connection()
        {
            Connection_st = ConfigurationManager.ConnectionStrings["Proyecto_Ferreteira___1.Properties.Settings.FerreteriaDb"].ConnectionString;

        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(Connection_st);
        }

    }
}
