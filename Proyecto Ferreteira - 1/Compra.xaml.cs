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

            Calculo = new Clases.Calculos { Precio = "0", Cantidad = "0", Flete="0" };
            this.DataContext = Calculo;

        }
        private List<Clases.Compras> Carrito = new List<Clases.Compras>();
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
        /// <summary>
        /// Se encarga llenar la lista de Carrito con los datos referentes a los productos de la BDD y enviarlos al constructor requerido.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Agregar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var Item = new Clases.Compras
                {
                    IdProducto = Convert.ToInt32(cmbProducto.SelectedValue),
                    NombreProducto = cmbProducto.Text,
                    Cantidad = int.Parse(txtCantidad.Text),
                    Precio = double.Parse(txtPrecio.Text)
                };
                Carrito.Add(new Clases.Compras
                {
                    IdProducto = Convert.ToInt32(cmbProducto.SelectedValue),
                    Cantidad = int.Parse(txtCantidad.Text),
                    Precio = double.Parse(txtPrecio.Text)
                });
                dgbInformacion.Items.Add(Item);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
           
        }
        /// <summary>
        /// Se encarga de enviar los parametros requeridos al constructor 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Realizar_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                string Resultado = "";
                int Codigo = compra.CodigoCompra();
                foreach (Clases.Compras Item in Carrito)
                {

                    Clases.Compras compras = new Clases.Compras
                        (
                         Convert.ToInt32(cmbProveedor.SelectedValue),
                         Convert.ToInt32(Item.IdProducto.ToString()),
                         Convert.ToInt32(Item.Cantidad.ToString()),
                         Convert.ToInt32(Item.Precio.ToString()),
                         double.Parse(txtSubtotal.Text),
                         double.Parse(txtISV.Text),
                         double.Parse(txtDescuento.Text),
                         Codigo,
                         double.Parse(txtFlete.Text)
                        );

                    Resultado = compras.GuardarCompras();


                }
                MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           
        }

        private void Eliminar_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
