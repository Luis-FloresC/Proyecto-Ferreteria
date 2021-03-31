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
    /// Lógica de interacción para FormEliminarUsuario.xaml
    /// </summary>
    public partial class FormEliminarUsuario : UserControl
    {
        public FormEliminarUsuario()
        {
            InitializeComponent();
            MostrarEmpleados();
        }




        Clases.Usuarios usuarios = new Clases.Usuarios();
        private List<string> ListaDeEmpleados;

        private void MostrarEmpleados()
        {
            ListaDeEmpleados = usuarios.ListaEmpleados();

            cmbNombreEmpleado.ItemsSource = ListaDeEmpleados;
        }


        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
