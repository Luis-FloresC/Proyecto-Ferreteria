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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para FormEmpleados.xaml
    /// </summary>
    public partial class FormEmpleados : UserControl
    {
        public FormEmpleados()
        {
            InitializeComponent();
            MOSTRAR_DATOS();
        }

        Clases.Connection conexion = new Clases.Connection();

        public void MOSTRAR_DATOS()
        {
            var connection = conexion.GetConnection();
            DataTable DT = new DataTable();
            try
            {
                string query = @"select
                               E.Codigo_Empleado [Id],
                               concat(E.Nombre_Empleado,' ',E.Apellido_empleado)[Nombre Completo],
                               P.Descripcion [Cargo],
                               E.Telefono,
                               E.Correo,
                               Datediff(YEAR,E.Fecha_Nacimiento,Getdate()) [Edad]
                               from [Recursos_humanos].[Empleado] E
                               join [Recursos_humanos].[Puesto] P  on  E.Codigo_Puesto = P.Codigo_Puesto";
                connection.Open();
                SqlDataAdapter DTA = new SqlDataAdapter(query, connection);
                DT.Clear();
                DTA.Fill(DT);
                DataGridEmpleados.ItemsSource = DT.DefaultView;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
