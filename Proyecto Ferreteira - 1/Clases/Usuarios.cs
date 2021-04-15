using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;

namespace Proyecto_Ferreteira___1.Clases
{
   public class Usuarios : INotifyPropertyChanged
    {
        public Usuarios() { }



       
      

        public string EditarDatos(string nombreEmpleado, string apellidoEmpleado, string nombreUsuario, string contraseña, string correo,string Dni)
        {
            string DatosActualizados = "";
            try
            {
                
                DatosActualizados = UserData.EditarDatosPerfil(nombreEmpleado, apellidoEmpleado, nombreUsuario, contraseña, correo,Dni);
                VerficarInicioSesion(nombreUsuario, contraseña);
                return DatosActualizados;
            }
            catch(Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public string GuardarDatos(string nombreUsuario, string contraseña, int codigo)
        {
            string DatosGuardados = "";
            try
            {

                DatosGuardados = UserData.RegistrarUsuario(nombreUsuario, contraseña, codigo);
                return DatosGuardados;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public bool ObtenerEstadoUsuario(int codigo)
        {
            try
            {
                return UserData.EstadoEmpleado(codigo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public string EliminarUsuario(int codigo ,bool estado)
        {
            try
            {
                return UserData.DesactivarUsuario(estado, codigo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
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

       
        public List<UserData> ListaEmpleados()
        {
            return UserData.MostrarEmpleados();
        }



        public string NombreUsuario
        {
            get { return CacheUsuario.Usuario; }
            set
            {

                CacheUsuario.Usuario = value;

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("NombreUsuario");

            }
        }

        public string DNI
        {
            get { return CacheUsuario.DNI; }
            set
            {

                CacheUsuario.DNI = value;

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("DNI");

            }
        }



        public string Nombre
        {
            get { return CacheUsuario.NombreCompleto; }
            set
            {

                CacheUsuario.NombreCompleto = value;

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Nombre");

            }
        }


        public string Apellido
        {
            get { return CacheUsuario.ApellidoCompleto; }
            set
            {

                CacheUsuario.ApellidoCompleto = value;

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Apellido");

            }
        }


        public string Email
        {
            get { return CacheUsuario.Email; }
            set
            {

                CacheUsuario.Email = value;

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Email");

            }
        }

        public string Cargo
        {
            get { return CacheUsuario.Cargo; }
            set
            {

                CacheUsuario.Cargo = value;

                // La propiedad cambia, avisar a la interfaz
                OnPropertyChanged("Cargo");

            }
        }



        public bool Permisos()
        {
            if (CacheUsuario.Cargo != "Administrador")
            {
                return false;
            }
            else
                return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        // Método que "avisa" a la interfaz que existe un cambio en una propiedad
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }


      
    }
}
