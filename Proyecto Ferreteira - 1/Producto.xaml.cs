using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para Producto.xaml
    /// </summary>
    public partial class Producto : UserControl
    {
        Clases.Producto producto = new Clases.Producto();
        Clases.Connection conexion = new Clases.Connection();
        public Producto()
        {
            InitializeComponent();
            MostrarCategorias();
            MostrarDatos();
        }

        //Creacion de la lista privada
        private List<string> Categoria;
        private void MostrarCategorias()
        {
            Categoria = producto.Categoria();
            cbNombreCategoria.ItemsSource = Categoria;
        }

        private void MostrarDatos()
        {
            var connection = conexion.GetConnection();
            try
            {
                string query = @"Select Productos.Producto.Codigo_Producto [Código], Productos.Producto.Nombre_Producto [Nombre del Producto],
                                Productos.Producto.Precio_Estandar [Precio], Productos.Producto.Existencia [Cantidad del Producto],
                                Productos.Categoria.Nombre_Categoria [Nombre de la Categoria] 
                                From Productos.Categoria INNER JOIN Productos.Producto 
                                ON Productos.Categoria.Codigo_Categoria = Productos.Producto.Codigo_Categoria"; //Consulta SQL

                //Abrir la conexión
                connection.Open();
                //Creación del comando SQL
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                DataTable informacionProductos = new DataTable();

                sqlDataAdapter.Fill(informacionProductos);
                dgvInventario.ItemsSource = informacionProductos.DefaultView;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
