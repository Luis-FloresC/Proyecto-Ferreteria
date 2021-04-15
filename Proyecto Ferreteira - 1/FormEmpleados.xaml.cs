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

        private List<Clases.UserData> ListaTotalEmpleados;
        Clases.Empleados Empleados = new Clases.Empleados();
        private List<Clases.UserData> ListaCargos;

        public FormEmpleados()
        {
            InitializeComponent();
            CargarDatosComboBoxCargo();
            CargarDatosDataGrid();
            
        }



        private DataTable DataTable;

     


        private void CargarDatosDataGrid()
        {
            DataTable = Empleados.MostarDataTableEmpleado();
            DataGridEmpleados.ItemsSource = DataTable.DefaultView;
            var TotalRows = DataGridEmpleados.Items.Count;
            txtTotal.Text = "Total: " + TotalRows;
        }
        /// <summary>
        /// Obtiene el los valores de los cargos
        /// </summary>
        private void CargarDatosComboBoxCargo()
        {
            ListaCargos = Empleados.Cargos();
            cmbCargo.SelectedValuePath = "Id";
            cmbCargo.DisplayMemberPath = "Cargo";
            cmbCargo.ItemsSource = ListaCargos;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(FechaNac.SelectedDate.ToString());
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            int codigo = (int)cmbCargo.SelectedValue;
            var Resultado = Empleados.AñadirNuevoEmpleado(
                                                           txtNombreEmpleado.Text,
                                                           txtApellidoEmpleado.Text,
                                                           codigo,
                                                           txtTelefono.Text,
                                                           txtEmail.Text,
                                                           txtDireccion.Text,
                                                           true,
                                                           FechaNac.SelectedDate.ToString(),
                                                           txtDNI.Text);

            MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            CargarDatosDataGrid();


        }

        //public int Codigo;
        private void LlenarCampos()
        {
            txtDNI.Text = Clases.CacheEmpleado.DNI;
            txtNombreEmpleado.Text = Clases.CacheEmpleado.NombreEmpleado;
            txtApellidoEmpleado.Text = Clases.CacheEmpleado.ApellidoEmpleado;
            txtEmail.Text = Clases.CacheEmpleado.Email;
            txtTelefono.Text = Clases.CacheEmpleado.Telefono;
            txtDireccion.Text = Clases.CacheEmpleado.Direccion;
            cmbCargo.Text = Clases.CacheEmpleado.Cargo;
            FechaNac.SelectedDate = Clases.CacheEmpleado.FechaNacimiento;
        }


        private List<FormEmpleados> Lista;
        private void DataGridEmpleados_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
            
        }

        private void DataGridEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridEmpleados.SelectedIndex != -1)
            {


                int id = Convert.ToInt32((DataGridEmpleados.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                var BuscarEmpleado = Empleados.BuscarEmpleado(id);

                if(!BuscarEmpleado)
                {
                    MessageBox.Show("Error");
                }
                else
                {
                    LlenarCampos();
                }


                //Clases.UserData Fila = this.DataGridEmpleados.SelectedItem as Clases.UserData;
                //LlenarCampos(Fila);

            }
            else
            {
                MessageBox.Show("F al chat");
            }
        }
    }
}
