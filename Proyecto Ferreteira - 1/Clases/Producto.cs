﻿using System;
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
                string query = @"Select Productos.Categoria.Codigo_Categoria, Productos.Categoria.Nombre_Categoria [Nombre de la Categoria] 
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

        public void InsertarProducto(string nombre, int existencia, int precio, int codigoCategoria)
        {
            var connection = GetConnection();
            //Validar que el usuario ingreso los datos necesarios
            try
            {
                string query = @"INSERT INTO Productos.Producto(Nombre_Producto, Existencia, Precio_Estandar,
                                Codigo_Categoria, Estado) VALUES (@nombre, @existencia,@precio, @codigo, 1)";
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@existencia", existencia);
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
                Productos nombreProducto = new Productos();
                nombreProducto.MostrarDatos();
            }

        }

    }
}
