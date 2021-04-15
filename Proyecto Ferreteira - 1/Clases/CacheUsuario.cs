using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Ferreteira___1.Clases
{
  public  class CacheUsuario
    {

        public static int IdUsuario { get; set; }
        public static string NombreCompleto { get; set; }
        
        public static string DNI { get; set; }

        public static string ApellidoCompleto { get; set; }
        public static string Cargo { get; set; }
        public static string Email { get; set; }
        public static bool Estado { get; set; }

        public static string Usuario { get; set; }

        public static string Contraseña { get; set; }

        public CacheUsuario() { }

    }
}
