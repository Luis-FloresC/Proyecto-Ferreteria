using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Proyecto_Ferreteira___1.Clases
{
   public class Proveedores
    {
        /// <summary>
        /// Instancia para llamar a la clase de UserData
        /// </summary>
        UserData UserData = new UserData();

        /// <summary>
        /// Constructor sin Parametros
        /// </summary>
        public Proveedores() { }

        /// <summary>
        /// Metodo para llenar el DatgridView de Proveedores
        /// </summary>
        /// <returns></returns>
        public DataTable MostrarProveedores()
        {
            try
            {
                return UserData.ObtenerProveedores();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Buscar un Proveedor
        /// </summary>
        /// <returns></returns>
        public DataTable BuscarProveedores(string nombre)
        {
            try
            {
                return UserData.BuscarProveedores(nombre);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Metodo para guardar los datos en la base de datos
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="telefono"></param>
        /// <param name="direccion"></param>
        /// <param name="correo"></param>
        /// <returns></returns>
        public string GuardarDatos(string nombre, string telefono, string direccion, string correo)
        {
            try
            {
                return UserData.RegistrarProveedores(nombre, telefono,direccion,correo);
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Modifcar datos personales del Proveedor
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="telefono"></param>
        /// <param name="direccion"></param>
        /// <param name="correo"></param>
        /// <returns></returns>
        public string ModificarDatos(int codigo,string nombre, string telefono, string direccion, string correo,bool estado)
        {
            try
            {
                return UserData.ModificarProveedores(codigo,nombre, telefono, direccion, correo,estado);
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Metodo para eliminar un Proveedor
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="telefono"></param>
        /// <param name="direccion"></param>
        /// <param name="correo"></param>
        /// <returns></returns>
        public string EliminarDatos(int codigo)
        {
            try
            {
                return UserData.EliminarProveedor(codigo,false);
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

    }
}
