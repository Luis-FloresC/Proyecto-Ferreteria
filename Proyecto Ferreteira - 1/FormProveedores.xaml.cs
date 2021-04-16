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


namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para FormProveedores.xaml
    /// </summary>
    public partial class FormProveedores : UserControl
    {
        /// <summary>
        /// Creacion de Instancia a la clase de Proveedres
        /// </summary>
        public Clases.Proveedores Proveedores = new Clases.Proveedores();
        private static int IdProveedor; 

        /// <summary>
        /// instancia para el DataTable de proveedores
        /// </summary>
        private DataTable DataTableProveedores = new DataTable();

        public FormProveedores()
        {
            InitializeComponent();
            MostrarDatosDataGridProveedores();
        }

        /// <summary>
        /// Metodo para Mostrar los Proveedores existentes
        /// </summary>
        private void MostrarDatosDataGridProveedores()
        {
            DataTableProveedores.Clear();
            DataTableProveedores = Proveedores.MostrarProveedores();
            DataGridProveedores.ItemsSource = DataTableProveedores.DefaultView;
            var TotalRows = DataGridProveedores.Items.Count;
            txtTotal.Text = "Total: " + TotalRows;
        }

        /// <summary>
        /// Metodo para Limpiar todos los campos del formulario
        /// </summary>
        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;  
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
        }

        /// <summary>
        /// Metodo para vaidar los campos del formulario
        /// </summary>
        /// <returns></returns>
        private bool Validaciones()
        {
            bool datosCorrectos = true;

            if ((txtNombre.Text == string.Empty) || (txtEmail.Text == string.Empty) ||
                (txtDireccion.Text == string.Empty) || (txtTelefono.Text == string.Empty))            
            {
                datosCorrectos = false;
                MessageBox.Show("Todos los Campos son Obligatorio", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (txtTelefono.Text.Length <= 7 || txtTelefono.Text.Length > 20)
            {
                datosCorrectos = false;
                MessageBox.Show("El telefono debe tener al menos 8 numeros", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return datosCorrectos;
        }

        /// <summary>
        /// Configuracion del evento click para guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(Validaciones())
            {
                var Resultado = Proveedores.GuardarDatos(txtNombre.Text,txtTelefono.Text,txtDireccion.Text,txtEmail.Text);
                MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
                MostrarDatosDataGridProveedores();
            }
        }

        /// <summary>
        /// Evento para seleccionar una fila del DataGridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridProveedores.SelectedIndex != -1)
            {
                IdProveedor = Convert.ToInt32((DataGridProveedores.CurrentItem as DataRowView).Row.ItemArray[0].ToString());
                txtNombre.Text = (DataGridProveedores.CurrentItem as DataRowView).Row.ItemArray[1].ToString();
                txtTelefono.Text = (DataGridProveedores.CurrentItem as DataRowView).Row.ItemArray[2].ToString();
                txtDireccion.Text = (DataGridProveedores.CurrentItem as DataRowView).Row.ItemArray[3].ToString();
                txtEmail.Text = (DataGridProveedores.CurrentItem as DataRowView).Row.ItemArray[4].ToString();
            }
        }

        /// <summary>
        /// Evento Click para Limpiar los campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        /// <summary>
        /// Evento Click para Modificar un Proveedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridProveedores.SelectedIndex != -1 && Validaciones())
            {
                var Resultado = Proveedores.ModificarDatos(IdProveedor,txtNombre.Text, txtTelefono.Text, txtDireccion.Text, txtEmail.Text);
                MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
                MostrarDatosDataGridProveedores();
            }
        }

        /// <summary>
        /// Evento Click para Eliminar un proveedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridProveedores.SelectedIndex != -1 && Validaciones())
            {

                if ((MessageBox.Show("¿Esta Seguro que desea Eliminar el Empleado?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))
                {
                    var Resultado = Proveedores.EliminarDatos(IdProveedor);
                    MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCampos();
                    MostrarDatosDataGridProveedores();
                }

            }
        }

        private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTableProveedores.Clear();
            DataTableProveedores = Proveedores.BuscarProveedores(txtBuscar.Text);
            DataGridProveedores.ItemsSource = DataTableProveedores.DefaultView;
            var TotalRows = DataGridProveedores.Items.Count;
            txtTotal.Text = "Total: " + TotalRows;
        }


        /// <summary>
        /// Metodo para permitir solo Letras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidacionLetras(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = true;
                MessageBox.Show("Solo se Permiten Letras", "Aviso");
            }
            else
            {
                e.Handled = false;
            }
        }

        /// <summary>
        /// Metodo para permitir solo Numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidacionNumeros(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo se Permiten Numeros", "Aviso");
            }
        }

        /// <summary>
        /// Evento Click para cancelar la operacion a realizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("¿Esta Seguro que desea cancelar la operacion?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))
            {
                LimpiarCampos();
            }
        }
    }
}
