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
        public double Precio { get; set; }


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

        public List<Compras> LlenarComboProductos()
        {

            List<Compras> productos = new List<Compras>();

            //Conexion
            var connection = GetConnection();
            try
            {
                //Consulta SQL
                string query = @"SELECT [Codigo_Producto],[Nombre_Producto]
                                FROM [Ferreteria].[Productos].[Producto]";


                //Creacion del comando de consulta

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        productos.Add(new Compras { IdProducto = reader.GetInt32(0), NombreProducto = reader.GetString(1) });

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

        public List<Compras> LlenarComboProveedores()
        {

            List<Compras> proveedores = new List<Compras>();

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
                        proveedores.Add(new Compras { IdProveedor = reader.GetInt32(0), NombreProveedor = reader.GetString(1) });

                    }
                }

                return proveedores;
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


    }
}

    


        
    

