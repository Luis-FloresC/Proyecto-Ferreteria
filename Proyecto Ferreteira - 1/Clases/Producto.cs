using System;
using System.Collections.Generic;
using System.Data;
//Namespace Requerido
using System.Data.SqlClient;
using System.Windows;

namespace Proyecto_Ferreteira___1.Clases
{
    class Producto : Connection
    {
        //Propiedades
        public int CodigoDeLaCategoria { get; set; }
        public string NombreDeLaCategoria { get; set; }
        public string NombreDelProducto { get; set; }
        public int CantidadDeProducto { get; set; }
        public double PrecioDelProducto { get; set; }

        //Constructor
        public Producto() { }

        /// <summary>
        /// Lista encargada de guardar el codigo de producto.
        /// </summary>
        public List<int> IdCategoria = new List<int>();

        /// <summary>
        /// Obtiene la lista de categorias de la base de datos
        /// </summary>
        /// <returns>Lista con los nombres de las categorias</returns>
        public List<String> Categoria()
        {
            List<String> Categoria = new List<string>();
            var connection = GetConnection();

            try
            {
                string query = @"SELECT Codigo_Categoria, Nombre_Categoria
                                FROM Productos.Categoria"; //Consulta SQL

                //Creacion de la consulta
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //Obtencion de datos 
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Categoria.Add(NombreDeLaCategoria = reader.GetString(1));
                        IdCategoria.Add(CodigoDeLaCategoria = reader.GetInt32(0));
                    }
                }
                return Categoria;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Inserta productos a la base de datos.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="existencia"></param>
        /// <param name="precio"></param>
        /// <param name="codigoCategoria"></param>
        public void InsertarProducto(string nombre, int existencia, decimal precio, int codigoCategoria)
        {
            var connection = GetConnection();


            //Validar que el usuario ingreso los datos necesarios
            try
            {
                string query = @"IngresarProducto";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@precio", precio);
                sqlCommand.Parameters.AddWithValue("@codigo", codigoCategoria);
                sqlCommand.Parameters.Add("@msj", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show(sqlCommand.Parameters["@msj"].Value.ToString(), "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {

                MessageBox.Show("Verifique que no este ingresando un producto ya existente","Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                connection.Close();
            }

        }
        /// <summary>
        /// Método que modifica el producto ingresado
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="precio"></param>
        /// <param name="codigoCategoria"></param>
        /// <param name="codigo_Producto"></param>
        public void ModificarProductos(string nombre, double precio, int codigoCategoria, int codigo_Producto, int cantidad)
        {
            var connection = GetConnection();
            try
            {
                string query = @"UPDATE Productos.Producto
                                SET Nombre_Producto = @nombre,
                                Precio_Estandar = @precio,
                                Codigo_Categoria = @codigo,
                                Existencia = @existencia,
                                Estado = 1
                                WHERE Codigo_Producto = @codigoProducto";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@precio", precio);
                sqlCommand.Parameters.AddWithValue("@codigo", codigoCategoria);
                sqlCommand.Parameters.AddWithValue("@existencia", cantidad);
                sqlCommand.Parameters.AddWithValue("@codigoProducto", codigo_Producto);
                sqlCommand.ExecuteNonQuery();
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
        /// <summary>
        /// /Método que elimina un producto del datagrid y lo deshabilita en la base de datos
        /// </summary>
        /// <param name="codigo_Producto"></param>
        public void EliminarProductos(int codigo_Producto)
        {
            var connection = GetConnection();
            try
            {
                string query = @"UPDATE Productos.Producto
                                SET 
                                Estado = 0
                                WHERE Codigo_Producto = @codigoProducto";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@codigoProducto", codigo_Producto);
                sqlCommand.ExecuteNonQuery();
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
