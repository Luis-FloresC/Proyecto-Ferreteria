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

        public  int IdProveedor { get; set; }

        public  string NombreProveedor { get; set; }

        public  int IdProducto { get; set; }

        public  string NombreProducto { get; set; }

        public  int Cantidad { get; set; }


        //Constructores
        public Compras() { }
        public Compras(int idProveedor, string nombreProveedor)
        {
            IdProveedor = idProveedor;
            NombreProveedor = nombreProveedor;
        }

        //Lista
        public List<Compras> Proveedores() 
        {

            List<Compras> ObtenerProveedores = new List<Compras>();
            //Conexion
            var connection = GetConnection();
            try
            {
                //Consulta SQL
                string query = @"Select Codigo_Proveedor, Nombre_Proveedor
                                From Compras.Proveedor";
                //Creacion del comando de consulta
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {
                 
                    while (reader.Read())
                    {
                        ObtenerProveedores.Add(new Compras() {NombreProveedor = reader.GetString(1) });
                        MessageBox.Show(NombreProveedor);
                    }
                }
                return ObtenerProveedores;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                foreach (Compras i in ObtenerProveedores)
                {
                    MessageBox.Show(i.ToString());
                }
                connection.Close();
            }
            return ObtenerProveedores;
        }



        public Compras(int idProducto, string nombreProducto, int cantidad)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
        }
        /// <summary>
        /// Muestra los proveedores en el combo box
        /// </summary>
        //Metodos
        public void MostrarProveedores()
        {
            //Conexion
            var connection = GetConnection();
            try
            {
                //Consulta SQL
                string query = @"Select Codigo_Proveedor, Nombre_Proveedor
                                From Compras.Proveedor";
                //Creacion del comando de consulta
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NombreProveedor = reader["Nombre_Proveedor"].ToString();
                        
                    }
                }                
            }         
            catch (Exception e)
            {
                Console.WriteLine(e);              
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Muestra Productos por proveedor
        /// </summary>
        //Metodos
        public void MostrarProductos()
        {
            //Conexion
            var connection = GetConnection();
            try
            {
                //Consulta SQL
                string query = @"Select Codigo_Proveedor, Nombre_Proveedor
                                From Compras.Proveedor";

                //Creacion del comando de consulta
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IdProveedor = Convert.ToInt32(reader["Codigo_Proveedor"]);
                        NombreProveedor = reader["Nombre_Proveedor"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
