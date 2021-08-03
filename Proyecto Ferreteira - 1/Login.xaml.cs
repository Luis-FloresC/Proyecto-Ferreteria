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


        /// <summary>
        /// Evento MouseDown para poder mover el Formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();

            }
            catch (Exception )
            {

            }
        }

        /// <summary>
        /// Boton de Aceptar para Verificar los datos del usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Clases.Usuarios usuarios = new Clases.Usuarios();
                var ResultadoValidacion = usuarios.VerficarInicioSesion(txtuser.Text, txtpass.Password);
                if ((ResultadoValidacion == true) && (txtuser.Text == Clases.CacheUsuario.Usuario) && (txtpass.Password == Clases.CacheUsuario.Contraseña) && (Clases.CacheUsuario.Estado == true))
                {
                    MessageBox.Show(string.Format("¡Bienvenido(a) al sistema, {0} {1}!", Clases.CacheUsuario.NombreCompleto, Clases.CacheUsuario.ApellidoCompleto),"Aviso",MessageBoxButton.OK,MessageBoxImage.Information);
                    MenuPrincipal menuPrincipal = new MenuPrincipal();
                    menuPrincipal.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o Contraseña Incorrecto\nIntente de nuevo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtpass.Password = string.Empty;
                    txtpass.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }

            
        }

        /// <summary>
        /// Evento Click para salir del Programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea Salir?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
