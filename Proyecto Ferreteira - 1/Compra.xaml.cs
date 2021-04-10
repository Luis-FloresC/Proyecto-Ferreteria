using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para Compra.xaml
    /// </summary>
    public partial class Compra : UserControl
    {
        public Clases.Calculos Calculo { get; set; }

        Clases.Compras compra = new Clases.Compras();

        public Compra()
        {
            InitializeComponent();
            MostrarProveedores();
            MostrarProductos();

            Calculo = new Clases.Calculos { Precio = "0", Cantidad = "0" };
            this.DataContext = Calculo;

        }

        private List<Clases.Compras> ObtenerProductos;
        private List<Clases.Compras> ObtenerProveedores;

        private void MostrarProveedores()
        {
            ObtenerProveedores = compra.LlenarComboProveedores();
            cmbProveedor.SelectedValuePath = "IdProveedor";
            cmbProveedor.DisplayMemberPath = "NombreProveedor";
            cmbProveedor.ItemsSource = ObtenerProveedores;
        }

        private void MostrarProductos()
        {
            ObtenerProductos = compra.LlenarComboProductos();
            cmbProducto.SelectedValuePath = "IdProducto";
            cmbProducto.DisplayMemberPath = "NombreProducto";
            cmbProducto.ItemsSource = ObtenerProductos;
        }

        private void Agregar_Click_1(object sender, RoutedEventArgs e)
        {
            var Item = new Clases.Compras
            {
                IdProducto = Convert.ToInt32(cmbProducto.SelectedValue),
                NombreProducto = cmbProducto.Text,
                Cantidad = int.Parse(txtCantidad.Text),
                Precio = double.Parse(txtPrecio.Text)
            };
            dgbInformacion.Items.Add(Item);
        }

        private void Realizar_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Eliminar_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
