using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;


namespace Proyecto_Ferreteira___1.Clases
{
    class clsEnvio: Connection 
    {
        //Atributos 
        ClsVenta venta = new ClsVenta();
        

        private string telefono;
        private string direccion;

        //Propiedades
 
        public string Telefono { get => telefono; set => telefono = value; }
        public string Direccion { get => direccion; set => direccion = value; }

        //Metodos
        /// <summary>
        /// Es el metodo encargado de ingresar el envio a la base de datos
        /// </summary>
        public void envio(string tel, string dic)
        {
            var conexion = GetConnection();

            try
            {
                //Query a ejecutar en la base de datos
                string query = "EXEC [dbo].[InsertarEnvio] @codigo_venta, @estado_envio, @direccion, @telefono, @codigo_empleado";

                //Establece la conexión
                conexion.Open();

                //Crea el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                //Remplazar los valores de los parametros
                
                sqlCommand.Parameters.AddWithValue("@codigo_venta", venta.CodigoVenta());
                sqlCommand.Parameters.AddWithValue("@estado_envio", 1);
                sqlCommand.Parameters.AddWithValue("@direccion",dic );
                sqlCommand.Parameters.AddWithValue("@telefono", tel);
                sqlCommand.Parameters.AddWithValue("@codigo_empleado", CacheUsuario.IdUsuario);
                
                //Ejecutar el comando de insercion
                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                //Mensaje de error
                MessageBox.Show(e.ToString());
            }
            finally
            {
                //Cerrar la conexión
                conexion.Close();
            }


        }
    }
}
