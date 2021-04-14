using System;
using System.Collections.Generic;
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
        public int PrecioDelProducto { get; set; }

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
        public void InsertarProducto(string nombre, int existencia, int precio, int codigoCategoria)
        {
            var connection = GetConnection();
            //Validar que el usuario ingreso los datos necesarios
            try
            {
                string query = @"INSERT INTO Productos.Producto(Nombre_Producto, Existencia, Precio_Estandar,
                                Codigo_Categoria, Estado) VALUES (@nombre, 0 ,@precio, @codigo, 1)";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@precio", precio);
                sqlCommand.Parameters.AddWithValue("@codigo", codigoCategoria);
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
        public void ModificarProductos(string nombre, double precio, int codigoCategoria, int codigo_Producto)
        {
            var connection = GetConnection();
            try
            {
                string query = @"UPDATE Productos.Producto
                                SET Nombre_Producto = @nombre,
                                Precio_Estandar = @precio,
                                Codigo_Categoria = @codigo,
                                Estado = 1
                                WHERE Codigo_Producto = @codigoProducto";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@precio", precio);
                sqlCommand.Parameters.AddWithValue("@codigo", codigoCategoria);
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
