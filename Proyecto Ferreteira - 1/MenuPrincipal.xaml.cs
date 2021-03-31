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

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {

            InitializeComponent();
            Pantalla.Text = "WindowMaximize";
            txtCargo.Text = Clases.CacheUsuario.Cargo;
            txtEmail.Text = Clases.CacheUsuario.Email;
            txtUsuario.Text = Clases.CacheUsuario.NombreCompleto + " " + Clases.CacheUsuario.ApellidoCompleto;
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

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
                    //GridPrincipal.Children.Clear();
                    //GridPrincipal.Children.Add(new FormUsuarios());
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(new Clientes());
                    break;
                case 2:
                    //GridPrincipal.Children.Clear();
                    //GridPrincipal.Children.Add(new prueba3());
                    break;
                case 3:
                    //GridPrincipal.Children.Clear();
                    //GridPrincipal.Children.Add(new prueba3());
                    break;
                case 4:
                    //GridPrincipal.Children.Clear();
                    //GridPrincipal.Children.Add(new prueba3());
                    break;
                case 5:
                    //GridPrincipal.Children.Clear();
                    //GridPrincipal.Children.Add(new prueba3());
                    break;
                default:
                    if (MessageBox.Show("¿Esta seguro que desea Cerrar Sesion?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                    break;
            }
        }

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

       

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnEditarPerfil_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(new FormUsuarios());
        }
    }
}
