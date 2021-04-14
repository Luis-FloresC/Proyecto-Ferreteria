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
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : UserControl
    {
        List<Clases.ClsProducto> productos = new List<Clases.ClsProducto>();

        double existenciaProduc = 0;

        public Ventas()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if( txtCantidad.Text.Equals(string.Empty) || Convert.ToInt32(txtCantidad.Text) <= 0)
            {
                MessageBox.Show("La cantidad que usted ingreso no es valida");
            }
            else if (Convert.ToInt32(txtCantidad.Text) > existenciaProduc)
            {
                MessageBox.Show("La existencia de este producto es insuficiente");
            }
            else
            {
                var Item = new Clases.ClsProducto
                {
                    ID = Convert.ToInt32(txtIdProducto.Text),
                    PRODUCTO = txtNombreProducto.Text,
                    PRECIO = double.Parse(txtPrecio.Text),
                    CANTIDAD = int.Parse(txtCantidad.Text),
                    IMPORTE = (Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text))
                };
                /*productos.Add(new Clases.ClsProducto
                {
                    ID = Convert.ToInt32(txtIdProducto.Text),
                    PRECIO = double.Parse(txtPrecio.Text),
                    CANTIDAD = int.Parse(txtCantidad.Text)
                });*/
                dgDetalleVenta.Items.Add(Item);

                limpiarProducto();
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(dgDetalleVenta.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el producto a eliminar");
            }
            else
            {
                dgDetalleVenta.Items.RemoveAt(dgDetalleVenta.SelectedIndex);
            }
        }

        private void btnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            BuscarCliente buscarCliente = new BuscarCliente();
            buscarCliente.pasar += BuscarCliente_pasar;
            buscarCliente.Visibility = Visibility.Visible;
        }

        private void BuscarCliente_pasar(string codigoCliente, string nombreCliente)
        {
            txtIdCliente.Text = codigoCliente;
            txtNombreCliente.Text = nombreCliente;
        }

        private void btnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            BuscarProducto buscarProducto = new BuscarProducto();
            buscarProducto.pasar += BuscarProducto_pasar;
            buscarProducto.Visibility = Visibility.Visible;
        }

        private void BuscarProducto_pasar(string codigoProducto, string nombreProducto, string existenciaProducto, string precioProducto)
        {
            txtIdProducto.Text = codigoProducto;
            txtNombreProducto.Text = nombreProducto;
            existenciaProduc = Convert.ToInt32(existenciaProducto);
            txtPrecio.Text = precioProducto;
        }

        private void limpiarProducto()
        {
            txtIdProducto.Text = "";
            txtNombreProducto.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
        }

        private void limpiar()
        {
            txtNombreCliente.Text = "";
            txtIdCliente.Text = "";
            cbTipoPago.SelectedIndex = -1;
            txtSubtotal.Text = "0.00";
            txtIsv.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtSubtotal.Text = "0.00";
            txtTotal.Text = "0.00";
            limpiarProducto();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            dgDetalleVenta.Items.Clear();
        }
    }
}
