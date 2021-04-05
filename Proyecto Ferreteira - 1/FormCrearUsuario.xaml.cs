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
        public FormCrearUsuario()
        {
            InitializeComponent();
           
            MostrarEmpleados();
        }

        Clases.Usuarios usuarios = new Clases.Usuarios();
        private List<Clases.UserData> ListaDeEmpleados;

        private void MostrarEmpleados()
        {
            ListaDeEmpleados = usuarios.ListaEmpleados();
            cmbNombreEmpleado.DisplayMemberPath = "NombreEmpleado";
            cmbNombreEmpleado.SelectedValuePath = "Id";
            cmbNombreEmpleado.ItemsSource = ListaDeEmpleados;
        }


        private bool VerficarDatos()
        {
            bool datosCorrectos = true;

            if(cmbNombreEmpleado.SelectedValue == null)
            {
                MessageBox.Show("Por Favor seleccione un Empleado...","Avisp",MessageBoxButton.OK,MessageBoxImage.Warning);
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

                MessageBox.Show("Todos los Campos son Obligatorios","Aviso",MessageBoxButton.OK,MessageBoxImage.Warning);
                datosCorrectos = false;
            }

            if(txtPassWord.Password != txtConfirmPassWord.Password)
            {
                MessageBox.Show("la contraseña no coinciden", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassWord.Password = "";
                txtConfirmPassWord.Password = "";
                datosCorrectos = false;
            }

            if(txtPassWord.Password.Length <= 7 && txtConfirmPassWord.Password.Length <= 7)
            {
                MessageBox.Show("la constraseña debe contener al menos 8 caracteres", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                datosCorrectos = false;
            }

            return datosCorrectos;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(VerficarDatos())
            {
                var Resultado = usuarios.GuardarDatos(txtNombreUsuario.Text, txtPassWord.Password, Convert.ToInt32(cmbNombreEmpleado.SelectedValue));
                MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
