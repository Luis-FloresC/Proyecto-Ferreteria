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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            Clases.Usuarios usuarios = new Clases.Usuarios();
            var ResultadoValidacion = usuarios.VerficarInicioSesion(txtuser.Text, txtpass.Password);
            if ((ResultadoValidacion == true) && (txtuser.Text == Clases.CacheUsuario.Usuario) && (txtpass.Password == Clases.CacheUsuario.Contraseña) && (Clases.CacheUsuario.Estado == true))
            {
                MessageBox.Show(string.Format("Bienvenido al sistema! {0},{1}", Clases.CacheUsuario.NombreCompleto,Clases.CacheUsuario.ApellidoCompleto));
                //Menu_Principal menu_Principal = new Menu_Principal();
                //menu_Principal.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario Incorrecto o Contraseña\nIntente de nuevo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea Salir?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
