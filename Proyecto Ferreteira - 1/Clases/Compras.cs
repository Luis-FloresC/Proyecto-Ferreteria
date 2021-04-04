using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Namespace Requeridos
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Proyecto_Ferreteira___1.Clases
{
   class Compras : Connection
    {

        //Propiedades

        public int IdProveedor { get; set; }

        public string NombreProveedor { get; set; }

        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public int Cantidad { get; set; }


        //Constructores
        public Compras() { }
        public Compras(int idProveedor, string nombreProveedor)
        {
            IdProveedor = idProveedor;
            NombreProveedor = nombreProveedor;
        }

        public Compras(int idProducto, string nombreProducto, int cantidad)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
        }


        public List<int> Identificador = new List<int>();

        //Lista 
        public List<String> Proveedores()
        {

            List<String> ObtenerProveedores = new List<String>();

            //Conexion
            var connection = GetConnection();
            try
            {
                //Consulta SQL
                string query = @"SELECT [Codigo_Proveedor]
                                ,[Nombre_Proveedor] FROM [Compras].[Proveedor]";

                //Creacion del comando de consulta

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        ObtenerProveedores.Add(NombreProveedor = reader.GetString(1));
                        Identificador.Add(IdProveedor = reader.GetInt32(0));
                    }
                }

                return ObtenerProveedores;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                connection.Close();
            }
        }
        /// <summary>
        /// Obtiene los productos segun que proveedor se seleccione.
        /// </summary>
        /// <param name="codigo">Es el id del proveedor que sirve como parametro para mostrar los productos</param>
        /// <returns></returns>
        public List<String> ObtenerProductos()
        {

            List<String> productos = new List<String>();

            //Conexion
            var connection = GetConnection();
            try
            {
                //Consulta SQL
                string query = @"SELECT   Productos.Producto.Nombre_Producto
                                FROM   Productos.Producto";
                                     

                //Creacion del comando de consulta

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        productos.Add(NombreProducto = reader.GetString(0));

                    }
                }

                return productos;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                connection.Close();
            }
        }

        public void MostrarDatos()
        {


        }
    }
}

    


        
    

