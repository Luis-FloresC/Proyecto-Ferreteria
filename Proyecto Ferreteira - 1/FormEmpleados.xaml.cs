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

     
        Clases.Empleados Empleados = new Clases.Empleados();
        private List<Clases.UserData> ListaCargos;
        private DataTable DataTable;

        public FormEmpleados()
        {
            InitializeComponent();
            CargarDatosComboBoxCargo();
            CargarDatosDataGrid();
            
        }

        /// <summary>
        /// Metodo para Visualizar los datos en el DatagridView
        /// </summary>
        private void CargarDatosDataGrid()
        {
            DataTable = Empleados.MostarDataTableEmpleado();
            DataGridEmpleados.ItemsSource = DataTable.DefaultView;
            var TotalRows = DataGridEmpleados.Items.Count;
            txtTotal.Text = "Total: " + TotalRows;
        }
        /// <summary>
        /// Obtiene el los valores de los cargos Dsiponibles en nuestra base de datos
        /// </summary>
        private void CargarDatosComboBoxCargo()
        {
            ListaCargos = Empleados.Cargos();
            cmbCargo.SelectedValuePath = "Id";
            cmbCargo.DisplayMemberPath = "Cargo";
            cmbCargo.ItemsSource = ListaCargos;
        }

        /// <summary>
        /// Metodo para cancelar la operacion a realizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("¿Esta Seguro que desea cancelar la operacion?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))
            {
                LimpiarCampos();
            }
          
        }

        /// <summary>
        /// Metodo para hacer una insercion en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(Validaciones())
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
                LimpiarCampos();
            }
           


        }

        /// <summary>
        /// Metodo para llenar los Campos cuando seleccionamos una fila del DataGridView
        /// </summary>
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

        /// <summary>
        /// Metodo para seleccionar una fila del DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            }
          
        }

       
        /// <summary>
        /// Metodo para Modificar Datos del Empleado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridEmpleados.SelectedIndex != -1 && Validaciones())
            {

                int codigo = (int)cmbCargo.SelectedValue;
                var Resultado = Empleados.ModificarDatosPersonales(
                                                               txtNombreEmpleado.Text,
                                                               txtApellidoEmpleado.Text,
                                                               txtDNI.Text,
                                                               txtEmail.Text,
                                                               txtDireccion.Text,
                                                               FechaNac.SelectedDate.Value,
                                                               codigo,
                                                               txtTelefono.Text
                                                               );

                MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                CargarDatosDataGrid();
                LimpiarCampos();
            }
            

        }

        /// <summary>
        /// Metodo para Desabilitar un Empleado de la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridEmpleados.SelectedIndex != -1 )
            {

                if((MessageBox.Show("¿Esta Seguro que desea Eliminar el Empleado?","Advertencia",MessageBoxButton.YesNo,MessageBoxImage.Warning) == MessageBoxResult.Yes))
                {
                    var Resultado = Empleados.EliminarUsuario(Clases.CacheEmpleado.IdEmpleado);
                    MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarDatosDataGrid();
                    LimpiarCampos();
                }

               
            }
          
        }


        /// <summary>
        /// Metodo para vaidar los campos del formulario
        /// </summary>
        /// <returns></returns>
        private bool Validaciones()
        {
            bool datosCorrectos = true;
           
            if( (txtDNI.Text == string.Empty) || (txtNombreEmpleado.Text == string.Empty) || 
                (txtApellidoEmpleado.Text == string.Empty) || (txtEmail.Text == string.Empty) ||
                (txtDireccion.Text == string.Empty) || (txtTelefono.Text == string.Empty) ||
                (cmbCargo.SelectedValue == null) || (FechaNac.SelectedDate == null) )
            {
                datosCorrectos = false;
                MessageBox.Show("Todos los Campos son Obligatorio","Advertencia",MessageBoxButton.OK,MessageBoxImage.Warning);
                
            }

            if(txtDNI.Text.Length <= 13 || txtDNI.Text.Length > 20)
            {
                datosCorrectos = false;
                MessageBox.Show("La Identidad tiene que tener entre 13-20 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            
            return datosCorrectos;

        }

        /// <summary>
        /// Boton para limpiar los valores de los campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }


        private void LimpiarCampos()
        {
            txtDNI.Text = string.Empty;
            txtNombreEmpleado.Text = string.Empty;
            txtApellidoEmpleado.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            cmbCargo.Text = null;
            FechaNac.SelectedDate = null;
        }
    }
}
