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
    /// Lógica de interacción para BuscarCliente.xaml
    /// </summary>
    public partial class BuscarCliente : Window
    {
        public delegate void pasarCliente(string codigoCliente, string nombreCliente);
        public event pasarCliente pasar;
        private SqlConnection sqlConnection;

        public BuscarCliente()
        {
            InitializeComponent();
            // Realizar la conexión con el servidor de base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["Proyecto_Ferreteira___1.Properties.Settings.FerreteriaDb"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            buscarClientes();
        }

        /// <summary>
        /// Metodo para buscar los clientes segun el nombre
        /// </summary>
        public void buscarClientes()
        {
            try
            {
                //Establece la conexion con la base de datos
                sqlConnection.Open();

                SqlCommand cmd = sqlConnection.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"Select codigo_cliente, nombres, apellidos, identidad from [Ventas].[Cliente] " +
                                    "WHERE nombres like '%"+txtBuscar.Text+"%' and estado = 1";

                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);

                dgClientes.ItemsSource = dt.DefaultView;
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

        /// <summary>
        /// Envia los datos del cliente seleccionado a la venta de Ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            if(dgClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Error! seleccione el cliente en la tabla");
            }
            else
            {
                int fila = dgClientes.SelectedIndex;

                string codigo = (dgClientes.Items[fila] as System.Data.DataRowView).Row.ItemArray[0].ToString();
                string nombre = (dgClientes.Items[fila] as System.Data.DataRowView).Row.ItemArray[1].ToString();

                pasar(codigo, nombre);

                this.Visibility = Visibility.Hidden;
            }
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            buscarClientes();
        }
    }
}
