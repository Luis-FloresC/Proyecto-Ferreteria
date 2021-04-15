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

       


        public Empleados() { }

    
        
        UserData UserData = new UserData();


        public bool BuscarEmpleado(int codigo)
        {
            return UserData.BuscarEmpleado(codigo);
        }


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
