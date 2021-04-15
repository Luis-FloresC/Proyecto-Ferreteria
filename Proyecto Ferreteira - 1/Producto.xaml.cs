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
                                Productos.Producto.Precio_Estandar [Precio],
                                Productos.Categoria.Nombre_Categoria [Nombre de la Categoria] 
                                From Productos.Categoria INNER JOIN Productos.Producto 
                                ON Productos.Categoria.Codigo_Categoria = Productos.Producto.Codigo_Categoria
                                WHERE Productos.Producto.Estado = 1;"; //Consulta SQL

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
            if (txtNombreProducto.Text == String.Empty || txtPrecioProducto.Text == String.Empty || cbNombreCategoria.SelectedValue == null)
            {
                MessageBox.Show("¡Por favor, llenar todos los campos solicitados!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (int i in IdCategoria)
                {
                    if (cbNombreCategoria.SelectedIndex == c)
                    {
                        string nombre = txtNombreProducto.Text;
                        int existencia = 0;
                        double precio = Convert.ToDouble(txtPrecioProducto.Text);
                        producto.InsertarProducto(nombre, existencia, precio, i);
                    }
                    c++;
                }
                MessageBox.Show("Se ha insertado correctamente el producto", "Felicidades", MessageBoxButton.OK, MessageBoxImage.Information);
                MostrarDatos();
                LimpiarPantalla();
            }
           
        }

        /// <summary>
        /// Modifica los datos del datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            int c = 0;
            if (txtNombreProducto.Text == String.Empty || txtPrecioProducto.Text == String.Empty || cbNombreCategoria.SelectedValue == null)
            {
                MessageBox.Show("¡Por favor, seleccionar el producto que desea modificar!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (int i in IdCategoria)
                {
                    if (cbNombreCategoria.SelectedIndex == c)
                    {
                        string nombre = txtNombreProducto.Text;
                        double precio = Convert.ToDouble(txtPrecioProducto.Text);
                        int productos = IdProducto;
                        producto.ModificarProductos(nombre, precio, i, productos);
                    }
                    c++;
                }
                MessageBox.Show("Se ha actualizado correctamente el producto", "Felicidades", MessageBoxButton.OK, MessageBoxImage.Information);
                MostrarDatos();
                LimpiarPantalla();
            }
           
        }
        /// <summary>
        /// Evento que se encarga de enviar los elementos del datagrid a los textbox y combobox
        /// </summary>

        public static int IdProducto;
        private void dgvInventario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvInventario.SelectedIndex != -1)
            {
                IdProducto = Convert.ToInt32((dgvInventario.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                txtNombreProducto.Text = (dgvInventario.CurrentItem as DataRowView).Row.ItemArray[1].ToString();
                txtPrecioProducto.Text = (dgvInventario.CurrentItem as DataRowView).Row.ItemArray[2].ToString();
                cbNombreCategoria.Text = (dgvInventario.CurrentItem as DataRowView).Row.ItemArray[3].ToString();
            }
        }
        /// <summary>
        /// Elimina el producto del datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int c = 0;
            if (txtNombreProducto.Text == String.Empty || txtPrecioProducto.Text == String.Empty || cbNombreCategoria.SelectedValue == null)
            {
                MessageBox.Show("¡Por favor, seleccionar el producto que desea eliminar!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                foreach (int i in IdCategoria)
                {
                    if (cbNombreCategoria.SelectedIndex == c)
                    {

                        int productos = IdProducto;
                        producto.EliminarProductos(productos);
                    }
                    c++;
                }
                if (MessageBox.Show("¿Esta seguro que desea eliminar el producto?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    MostrarDatos();
                    LimpiarPantalla();
                }
            }
        }
            
    }
}
