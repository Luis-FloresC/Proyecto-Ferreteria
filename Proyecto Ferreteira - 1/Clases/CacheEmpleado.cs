using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Ferreteira___1.Clases
{
  public  class CacheEmpleado
    {
        /// <summary>
        /// Constructor para llamar a la clase
        /// </summary>
        public CacheEmpleado() { }

        public static string NombreEmpleado { get; set; }

        public static string ApellidoEmpleado { get; set; }

        public static DateTime FechaNacimiento { get; set; }

        public static string DNI { get; set; }

        public static string Telefono { get; set; }

        public static int IdEmpleado { get; set; }

        public static string Email { get; set; }

        public static string Cargo { get; set; }

        public static string Direccion { get; set; }


    }
}
