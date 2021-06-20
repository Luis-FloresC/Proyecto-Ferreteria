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
    /// Lógica de interacción para FormEditarPerfil.xaml
    /// </summary>
    public partial class FormEditarPerfil : UserControl
    {

       


        public FormEditarPerfil()
        {
            InitializeComponent();
            LoadEditarPerfil();
        }

        /// <summary>
        /// Metodo para validar los campos
        /// </summary>
        /// <returns></returns>
        private bool VerificarDatos()
        {
            bool VerificacionCorrecta = true;
            if (txtPassword.Password.Length < 8)
            {
                MessageBox.Show("La contraseña debe tener minimo 8 caracteres,Vuelva a intentar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                VerificacionCorrecta = false;
                
            }

            if(txtPassword.Password != txtConfirmarPassword.Password)
            {
                MessageBox.Show("La contraseña no coincide,Vuelva a intentar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                VerificacionCorrecta = false;
            }

            if(txtPassWordActual.Password != Clases.CacheUsuario.Contraseña)
            {
                MessageBox.Show("La contraseña es incorrecta,Vuelva a intentar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
               
                VerificacionCorrecta = false;
            }

            return VerificacionCorrecta;
        }

      /// <summary>
      /// Metodo para Editar los datos personales
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(VerificarDatos())
            {
                var Resultado = usuarios.EditarDatos(txtNombre.Text, txtApellido.Text, txtUsuario.Text, txtPassword.Password, txtEmail.Text,txtDNI.Text);
                MessageBox.Show(Resultado,"Aviso",MessageBoxButton.OK,MessageBoxImage.Information);
                LoadEditarPerfil();
            }
        }


        /// <summary>
        /// Evento Click para cancelar la operacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Color.Text = "#242222";
            Panel2.Children.Clear();
            Panel2.Visibility = Visibility.Hidden;
            
        }

        /// <summary>
        /// Funcion para habilitar los campos de Contraseña y Confrimar Constraseña
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chEditar_Checked(object sender, RoutedEventArgs e)
        {
            if (chEditar.IsChecked != true)
            {

                txtPassword.IsEnabled = false;
                txtConfirmarPassword.IsEnabled = false;
                txtPassword.Password = Clases.CacheUsuario.Contraseña;
                txtConfirmarPassword.Password = Clases.CacheUsuario.Contraseña;
            }
            else
            {
                txtPassword.Password = string.Empty;
                txtConfirmarPassword.Password = string.Empty;
                txtPassword.IsEnabled = true; 
                txtConfirmarPassword.IsEnabled = true;
            }
        }


       /// <summary>
       /// Instancia para llamar al Formulario de Usuarios
       /// </summary>
        FormUsuarios formUsuario = new FormUsuarios();

        /// <summary>
        /// Instancia para llamar a la clase de Usuarios
        /// </summary>
        public Clases.Usuarios usuarios { get; set; }
        private void LoadEditarPerfil()
        {
            usuarios = new Clases.Usuarios
            {
                NombreUsuario = Clases.CacheUsuario.Usuario,
                Nombre = Clases.CacheUsuario.NombreCompleto,
                Apellido = Clases.CacheUsuario.ApellidoCompleto,
                Email = Clases.CacheUsuario.Email,
                Cargo = Clases.CacheUsuario.Cargo,
                DNI = Clases.CacheUsuario.DNI
            };
            this.DataContext = usuarios;


            txtPassword.Password = Clases.CacheUsuario.Contraseña;
            txtConfirmarPassword.Password = Clases.CacheUsuario.Contraseña;
           
         

        }


    }
}
