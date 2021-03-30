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
    /// Lógica de interacción para Compra.xaml
    /// </summary>
    public partial class Compra : UserControl
    {
        Clases.Compras compra = new Clases.Compras();
        public Compra()
        {
            InitializeComponent();
            MostrarProveedores();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           /*
            compra.MostrarProveedores();
            cmbProveedor.Items
            txtIdProveedor.Text = compra.IdProveedor.ToString();
           */
        }

        private List<string> Proveedores;
        private List<int> ObtenerIdentificador;
        private List<string> Productos;


        private void MostrarProveedores()
        {
            Proveedores = compra.Proveedores();
            ObtenerIdentificador = compra.Identificador;
            cmbProveedor.ItemsSource = Proveedores;
        }

        private void MostrarProductos()
        {
            cmbProducto.ItemsSource = Productos;
        }

        private void cmbProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int c = 0;
            foreach (int i in ObtenerIdentificador)
            {
                if (cmbProveedor.SelectedIndex == c)
                {
                    Productos = compra.ObtenerProductos(i);
                    MostrarProductos();
                }
                c++;
            }
        }
    }
}
