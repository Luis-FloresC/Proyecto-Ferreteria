using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Proyecto_Ferreteira___1.Clases
{
  public class Empleados
    {

        /// <summary>
        /// Instancia para mandar a llamar la clase de UserDate
        /// </summary>
        UserData UserData = new UserData();


        /// <summary>
        /// Constructor de la Clase de Empleados
        /// </summary>
        public Empleados() { }

    
        
        

        /// <summary>
        /// Metodo para Buscar un Empleado
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public bool BuscarEmpleado(int codigo)
        {
            return UserData.BuscarEmpleado(codigo);
        }

        /// <summary>
        /// Metodo para Modificar Datos Personales del Empleado
        /// </summary>
        /// <param name="nombreEmpleado"></param>
        /// <param name="apellidoEmpleado"></param>
        /// <param name="dni"></param>
        /// <param name="email"></param>
        /// <param name="direccion"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="codigoPuesto"></param>
        /// <param name="telefono"></param>
        /// <returns></returns>
        public string ModificarDatosPersonales(string nombreEmpleado, string apellidoEmpleado, string dni, string email, string direccion, DateTime fechaNacimiento, int codigoPuesto, string telefono)
        {
            try
            {
                return UserData.EditarDatosEmpleados(nombreEmpleado,apellidoEmpleado,dni,email,direccion,fechaNacimiento,codigoPuesto,telefono);
            }
            catch (Exception EX)
            {

                return EX.Message.ToString();
            }
        }

        /// <summary>
        /// Metodo para Eliminar un Usuario de la Base de datos
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string EliminarUsuario(int codigo)
        {
            try
            {
                return UserData.DesactivarUsuario(false, codigo);
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Metodo para llenar el Datatable con los datos del Empleado
        /// </summary>
        /// <returns></returns>
        public DataTable MostarDataTableEmpleado()
        {
            try
            {
                return UserData.DataTableEmpleado();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        /// <summary>
        /// Metodo para Buscar un Empleado
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public DataTable BuscarDataTableEmpleado(string nombre)
        {
            try
            {
                return UserData.BuscarEmpleado(nombre);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// Lista para guardar los Cargos Existentes
        /// </summary>
        /// <returns></returns>
        public List<UserData> Cargos()
        {
            try
            {
                return UserData.ComboBoxCargo();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Metodo para Registrar un Nuevo Empleado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="codigoCargo"></param>
        /// <param name="telefono"></param>
        /// <param name="correo"></param>
        /// <param name="direccion"></param>
        /// <param name="estado"></param>
        /// <param name="fechaNac"></param>
        /// <param name="Dni"></param>
        /// <returns></returns>
        public string AñadirNuevoEmpleado(string nombre, string apellido, int codigoCargo, string telefono, string correo, string direccion, bool estado, string fechaNac,string Dni)
        {
            try
            {
                return UserData.RegistrarEmpleados(nombre,apellido,codigoCargo,telefono,correo,direccion,estado,fechaNac,Dni);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
