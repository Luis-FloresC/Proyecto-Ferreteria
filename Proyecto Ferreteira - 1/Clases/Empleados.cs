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
