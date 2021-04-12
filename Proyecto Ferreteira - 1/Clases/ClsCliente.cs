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
    class ClsCliente
    {


        //Atributos
        public int codigo { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string identidad { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string telefono { get; set; }
        public string rtn { get; set; }
        public bool estado { get; set; }

        public void AgregarCliente()
        {

        }
    }
}
