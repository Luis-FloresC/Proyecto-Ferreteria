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
using System.Windows.Shapes;
using System.Diagnostics;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        MainWindow login = new MainWindow();

        public MenuPrincipal()
        {

            InitializeComponent();
            Pantalla.Text = "WindowMaximize";
            txtCargo.Text = Clases.CacheUsuario.Cargo;
            txtEmail.Text = Clases.CacheUsuario.Email;
            txtUsuario.Text = Cadena(Clases.CacheUsuario.NombreCompleto) + " " + Cadena(Clases.CacheUsuario.ApellidoCompleto);
        }

        /// <summary>
        /// Metodo para Separar Cadena de Texto
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        private string Cadena(string cadena)
        {
            String source = cadena; //Original text
            String[] result = source.Split(new char[] { ' ', ' ' });

            return result[0];
        }

        /// <summary>
        /// Button para poder salir del programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Evento MosueDown para poder mover el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

            }
          
        }

        /// <summary>
        /// Evento para Seleccionar un elemnto de la Lista y mostrar su respectiva pantalla 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new FormInicio());
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Ventas());
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Clientes());
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Productos());
                    break;
                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Compra());
                    break;
                case 5:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new FormEmpleados());
                    break;
                case 6:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new FormProveedores());
                    break;
                case 7:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new FormReportes());
                    break;
                default:
                    if (MessageBox.Show("¿Esta seguro que desea Cerrar Sesión?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        login.Visibility = Visibility.Visible;
                        this.Close();
                    }
                    break;
            }
        }

        /// <summary>
        /// Evento para cambiar la pocision del Cursor al seleccionar un elemnto de la Lista
        /// </summary>
        /// <param name="index"></param>
        private void MoveCursorMenu(int index)
        {
            if(Pantalla.Text != "WindowRestore")
            {
                TrainsitionigContentSlide.OnApplyTemplate();
                GridCursor.Margin = new Thickness(0, (150 + (60 * index)), 0, 0);
            }
            else
            {
                TrainsitionigContentSlide.OnApplyTemplate();
                GridCursor.Margin = new Thickness(0, (90 + ((60 * index) + 60)), 0, 0);
            }
     
        }

        /// <summary>
        /// Funcion para maximizar la pantalla y restaurarla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if(Pantalla.Text != "WindowRestore")
            {
                Pantalla.Text = "WindowRestore";
               
                WindowState = WindowState.Maximized;
            }
            else
            {
                Pantalla.Text = "WindowMaximize";
                WindowState = WindowState.Normal;
            }
        }

       
        /// <summary>
        /// Evento click para minimzar la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Evento click para abrir el formulario de Editar nuestro Perfil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditarPerfil_Click(object sender, RoutedEventArgs e)
        {
            MoveCursorMenu(10);
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new FormUsuarios());
        }

        private void BtnAyuda_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("https://luis-floresc.github.io/SitioWeb77neiras/");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
