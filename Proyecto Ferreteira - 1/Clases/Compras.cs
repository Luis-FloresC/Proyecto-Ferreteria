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

        public string IdProveedor { get; set; }

        public string NombreProveedor { get; set; }

        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public int Cantidad { get; set; }


        //Constructores
        public Compras() { }
        public Compras(string idProveedor, string nombreProveedor)
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

        public void ObtenerProductos()
        {
            Compra compra = new Compra();

            var connection = GetConnection();

            try
            {            
                string query = @"SELECT   Productos.Producto.Nombre_Producto, Compras.Proveedor.Codigo_Proveedor
                                FROM   Compras.Compra INNER JOIN
                                       Compras.Detalle_Compra ON Compras.Compra.Codigo_Compra = Compras.Detalle_Compra.Codigo_Compra INNER JOIN
                                       Productos.Producto ON Compras.Detalle_Compra.Codigo_Producto = Productos.Producto.Codigo_Producto INNER JOIN
                                       Compras.Proveedor ON Compras.Compra.Codigo_Proveedor = Compras.Proveedor.Codigo_Proveedor AND Compras.Compra.Codigo_Proveedor = Compras.Proveedor.Codigo_Proveedor
                                WHERE (Compras.Proveedor.Codigo_Proveedor = @Proveedor)";

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);


                //Obtencion de datos y almacenamiento en las propiedades
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                using (dataAdapter)
                {
                    command.Parameters.AddWithValue("@Proveedor", compra.cmbProveedor.SelectedValue);

                    DataTable Productos = new DataTable();

                    dataAdapter.Fill(Productos);
                    compra.cmbProveedor.DisplayMemberPath = "Nombre_Producto";
                    compra.cmbProveedor.SelectedValuePath = "Codigo_Proveedor";
                    compra.cmbProveedor.ItemsSource = Productos.DefaultView;
                }
        
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            finally
            { 
                connection.Close();               
            }
        }
    }
}

    


        
    

