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
    /// Lógica de interacción para FormUsuarios.xaml
    /// </summary>
    public partial class FormUsuarios : UserControl
    {

        /// <summary>
        /// Instancia para llamar a la clase de Usuario
        /// </summary>
        public Clases.Usuarios usuarios { get; set; }
        public FormUsuarios()
        {
            InitializeComponent();
            LoadEditarPerfil();
            Panel2.Visibility = Visibility.Hidden;
        }

        

       
        public void LoadEditarPerfil()
        {
            usuarios = new Clases.Usuarios
            {
                NombreUsuario = Clases.CacheUsuario.Usuario,
                Nombre = Clases.CacheUsuario.NombreCompleto,
                Apellido = Clases.CacheUsuario.ApellidoCompleto,
                Email = Clases.CacheUsuario.Email,
                Cargo = Clases.CacheUsuario.Cargo
            };
            this.DataContext = usuarios;


            txtPassword.Password = Clases.CacheUsuario.Contraseña;
            txtConfirmarPassword.Password = Clases.CacheUsuario.Contraseña;

            if (!usuarios.Permisos())
            {
                btnNuevo.Visibility = Visibility.Hidden;
                btnEliminar.Visibility = Visibility.Hidden;
            }

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chEditar_Checked(object sender, RoutedEventArgs e)
        {
            if (chEditar.IsChecked != true)
            {

                txtPassword.IsEnabled = false;
                txtConfirmarPassword.IsEnabled = false;
            }
            else
            {
                txtPassword.IsEnabled = true;
                txtConfirmarPassword.IsEnabled = true;
            }
        }

        private void linkEditarPerfil_Click(object sender, RoutedEventArgs e)
        {
            Panel2.Visibility = Visibility.Visible;
            Panel2.Children.Clear();
            Panel2.Children.Add(new FormEditarPerfil());

          
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Panel2.Visibility = Visibility.Visible;
            Panel2.Children.Clear();
            Panel2.Children.Add(new FormEliminarUsuario());
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Panel2.Visibility = Visibility.Visible;
            Panel2.Children.Clear();
            Panel2.Children.Add(new FormCrearUsuario());
            
        }


    }
}
