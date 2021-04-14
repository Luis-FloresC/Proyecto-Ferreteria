using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para BuscarProducto.xaml
    /// </summary>
    public partial class BuscarProducto : Window
    {
        public delegate void pasarProducto(string codigoProducto, string nombreProducto, string existenciaProducto,string precioProducto);
        public event pasarProducto pasar;
        private SqlConnection sqlConnection;

        public BuscarProducto()
        {
            InitializeComponent();
            // Realizar la conexión con el servidor de base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Ferreteira___1.Properties.Settings.FerreteriaDb"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            buscarProductos();
        }

        private void buscarProductos()
        {
            try
            {
                //Establece la conexion con la base de datos
                sqlConnection.Open();

                SqlCommand cmd = sqlConnection.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Select Codigo_Producto, Nombre_Producto, Existencia, Precio_Estandar From [Productos].[Producto] 
                                    WHERE [Nombre_Producto] like '%"+txtBuscar.Text+"%' and [Estado] = 1";

                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

                dgProductos.ItemsSource = dt.DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            buscarProductos();
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Error! seleccione el producto en la tabla");
            }
            else
            {
                int fila = dgProductos.SelectedIndex;

                string codigo = (dgProductos.Items[fila] as System.Data.DataRowView).Row.ItemArray[0].ToString();
                string nombre = (dgProductos.Items[fila] as System.Data.DataRowView).Row.ItemArray[1].ToString();
                string existencia = (dgProductos.Items[fila] as System.Data.DataRowView).Row.ItemArray[2].ToString();
                string precio = (dgProductos.Items[fila] as System.Data.DataRowView).Row.ItemArray[3].ToString();

                pasar(codigo, nombre,existencia, precio);

                this.Visibility = Visibility.Hidden;
            }
        }
    }
}
