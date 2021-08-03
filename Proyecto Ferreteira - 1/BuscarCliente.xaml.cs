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
        public delegate void pasarCliente(string codigoCliente, string nombreCliente, int edad);
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
                //Crea un comando SQL
                SqlCommand cmd = sqlConnection.CreateCommand();

                //Crea un comando tipo texto con el que se guardara el query
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT codigo_cliente [#], nombres [Nombres], apellidos[Apellidos], identidad [Identidad], fecha_nacimiento [Fecha Nacimiento] FROM [Ventas].[Cliente] " +
                                    "WHERE nombres like '%"+txtBuscar.Text+"%' and cod_estado = 1";

                //Ejecuta el query
                cmd.ExecuteNonQuery();

                //Crea un objeto tipo tabla
                DataTable dt = new DataTable();
                //Crea un objeto donde se guardara la consulta 
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //Llena el objeto tipo tabla con la consulta
                da.Fill(dt);
                //Llena el DataGrid con los datos guardados en el objeto tipo tabla
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
                MessageBox.Show("Error! seleccione el cliente en la tabla","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                int fila = dgClientes.SelectedIndex;

                string codigo = (dgClientes.Items[fila] as System.Data.DataRowView).Row.ItemArray[0].ToString();
                string nombre = (dgClientes.Items[fila] as System.Data.DataRowView).Row.ItemArray[1].ToString();
                DateTime fechaNacimiento = Convert.ToDateTime((dgClientes.Items[fila] as System.Data.DataRowView).Row.ItemArray[4].ToString());
                int edad = DateTime.Today.AddTicks(-fechaNacimiento.Ticks).Year - 1; //Calcula la edad del cliente con la fecha de nacimiento

                pasar(codigo, nombre, edad);

                this.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Ejecuta el metodo de la busqueda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            buscarClientes();
        }

    }
}
