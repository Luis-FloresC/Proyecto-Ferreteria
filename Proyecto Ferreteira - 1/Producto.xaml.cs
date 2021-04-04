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
    public partial class Productos : UserControl
    {
        Clases.Producto producto = new Clases.Producto();
        Clases.Connection conexion = new Clases.Connection();
        public Productos()
        {
            InitializeComponent();
            MostrarCategorias();
            MostrarDatos();
        }

        //Creacion de la lista privada
        private List<string> Categoria;
        private List<int> IdCategoria;
        private void MostrarCategorias()
        {
            Categoria = producto.Categoria();
            IdCategoria = producto.IdCategoria;
            cbNombreCategoria.ItemsSource = Categoria;
        }
        /// <summary>
        /// Llena el Datagrid con los datos obtenidos de SQL server
        /// </summary>
        public void MostrarDatos()
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
        /// <summary>
        /// Limpia los textbox y el combobox.
        /// </summary>
        private void LimpiarPantalla()
        {
            txtCantidadProducto.Text = String.Empty;
            txtNombreProducto.Text = String.Empty;
            txtPrecioProducto.Text = String.Empty;
            cbNombreCategoria.SelectedValue = null;
            txtNombreProducto.Focus();
        }

        /// <summary>
        /// Inserta los datos solicitados en el datagridview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            int c = 0;
            foreach (int i in IdCategoria)
            {
                if (cbNombreCategoria.SelectedIndex == c)
                {
                    string nombre = txtNombreProducto.Text;
                    int existencia = Convert.ToInt32(txtCantidadProducto.Text);
                    int precio = Convert.ToInt32(txtPrecioProducto.Text);
                    producto.InsertarProducto(nombre, existencia, precio, i);
                }
                c++;
            }
            MessageBox.Show("Se ha insertado correctamente el producto");
            MostrarDatos();
            LimpiarPantalla();
        }
    }
}
