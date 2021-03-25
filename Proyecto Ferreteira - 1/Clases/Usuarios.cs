using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Ferreteira___1.Clases
{
    class Usuarios
    {
        public Usuarios() { }

        /// <summary>
        /// Instancia para llmar ala Clase de UserData
        /// </summary>
        UserData UserData = new UserData();
        /// <summary>
        /// Devuelve un estado logico sobre la existencia del Usuario
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <param name="pass">Contraseña</param>
        /// <returns></returns>
        public bool VerficarInicioSesion(string user, string pass)
        {
            return UserData.Login(user, pass);
        }



    }
}
