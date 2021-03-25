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

        UserData UserData = new UserData();
        public bool VerficarInicioSesion(string user, string pass)
        {
            return UserData.Login(user, pass);
        }


    }
}
