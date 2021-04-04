using System;
using System.Collections.Generic;
//Namespace Requerido
using System.Data.SqlClient;

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
        /// Obtiene la lista de categorias de la base de datos
        /// </summary>
        /// <returns>Lista con los nombres de las categorias</returns>
        public List<String> Categoria()
        {
            List<String> Categoria = new List<string>();
            var connection = GetConnection();

            try
            {
                string query = @"Select Productos.Categoria.Codigo_Categoria, Productos.Producto.Nombre_Producto 
                                From Productos.Categoria INNER JOIN Productos.Producto 
                                ON Productos.Categoria.Codigo_Categoria = Productos.Producto.Codigo_Categoria"; //Consulta SQL

                //Creacion de la consulta
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //Obtencion de datos 
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Categoria.Add(NombreDeLaCategoria = reader.GetString(1));
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

    }
}
