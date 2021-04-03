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
        public  Clases.Calculos Calculo { get; set; }

        Clases.Compras compra = new Clases.Compras();

        public Compra()
        {
            InitializeComponent();
            MostrarProveedores();
            MostrarProductos();

            Calculo = new Clases.Calculos { Precio = "0", Cantidad = "0" };
            this.DataContext = Calculo;

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
            Productos = compra.ObtenerProductos();
            cmbProducto.ItemsSource = Productos;
        }

        private void Agregar_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Realizar_Click_2(object sender, RoutedEventArgs e)
        {
           
        }

        private void Eliminar_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
