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

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para FormCrearUsuario.xaml
    /// </summary>
    public partial class FormCrearUsuario : UserControl
    {

        /// <summary>
        /// Instancia para llamar a la clase de Uusuarios
        /// </summary>
        Clases.Usuarios usuarios = new Clases.Usuarios();

        /// <summary>
        /// Lista para Obtener los empleados
        /// </summary>
        private List<Clases.UserData> ListaDeEmpleados;

        public FormCrearUsuario()
        {
            InitializeComponent();
           
            MostrarEmpleados();
        }

       
        
        /// <summary>
        /// Metodo para mostrar los empleados en el ComboBox
        /// </summary>
        private void MostrarEmpleados()
        {
            ListaDeEmpleados = usuarios.ListaEmpleados();
            cmbNombreEmpleado.DisplayMemberPath = "Nombre";
            cmbNombreEmpleado.SelectedValuePath = "Id";
            cmbNombreEmpleado.ItemsSource = ListaDeEmpleados;
        }

        /// <summary>
        /// Metodo para validar el ingreso de Datos en el Formulario
        /// </summary>
        /// <returns></returns>
        private bool VerficarDatos()
        {
            bool datosCorrectos = true;

            if(cmbNombreEmpleado.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un Empleado...","Advertencia",MessageBoxButton.OK,MessageBoxImage.Warning);
                cmbNombreEmpleado.Focus();
                datosCorrectos = false;
            }

            if(txtNombreUsuario.Text == string.Empty || txtPassWord.Password == string.Empty || txtConfirmPassWord.Password == string.Empty)
            {
                if(txtNombreUsuario.Text == string.Empty)
                {
                    txtNombreUsuario.Focus();
                }
                else if(txtPassWord.Password == string.Empty)
                {
                    txtPassWord.Focus();
                }
                else
                {
                    txtConfirmPassWord.Focus();
                }

                MessageBox.Show("Todos los Campos son Obligatorios","Advertencia",MessageBoxButton.OK,MessageBoxImage.Warning);
                datosCorrectos = false;
            }

            if(txtPassWord.Password != txtConfirmPassWord.Password)
            {
                MessageBox.Show("La contraseña no coinciden", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassWord.Password = "";
                txtConfirmPassWord.Password = "";
                datosCorrectos = false;
            }

            if(txtPassWord.Password.Length <= 7 && txtConfirmPassWord.Password.Length <= 7)
            {
                MessageBox.Show("La constraseña debe contener al menos 8 caracteres", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                datosCorrectos = false;
            }

            return datosCorrectos;
        }

        /// <summary>
        /// Evento Click para Guardar el Usuaurio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(VerficarDatos())
            {
                var Resultado = usuarios.GuardarDatos(txtNombreUsuario.Text, txtPassWord.Password, Convert.ToInt32(cmbNombreEmpleado.SelectedValue));
                MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// Evento Click para cancelar la operacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Panel2.Children.Clear();
            Panel2.Visibility = Visibility.Collapsed;
        }
    }
}
