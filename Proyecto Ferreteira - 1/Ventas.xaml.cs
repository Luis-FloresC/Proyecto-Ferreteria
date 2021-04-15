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
using System.Data;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : UserControl
    {
        List<Clases.ClsProducto> productos = new List<Clases.ClsProducto>();
        //Objetos
        Clases.ClsVenta venta = new Clases.ClsVenta();

        //Variables globales
        double existenciaProduc = 0;
        double subtotal = 0;
        double descuento = 0;
        double total = 0;
        //Constantes
        const double isv = 0.15;
        

        public Ventas()
        {
            InitializeComponent();
        }

        //METODOS

        /// <summary>
        /// Agrega el producto seleccionado al DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                calculos();

                limpiarProducto();
            }
        }

        /// <summary>
        /// Elimina el producto seleccionado del DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(dgDetalleVenta.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el producto a eliminar");
            }
            else
            {
                dgDetalleVenta.Items.RemoveAt(dgDetalleVenta.SelectedIndex);

                calculos();
            }
        }

        private void btnFactura_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            dgDetalleVenta.Items.Clear();
        }

        /// <summary>
        /// Abre una ventana donde podemos buscar al cliente que 
        /// realizara la compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            BuscarCliente buscarCliente = new BuscarCliente();
            buscarCliente.pasar += BuscarCliente_pasar;
            buscarCliente.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Es el metodo encargado de pasar el cliente que seleccionamos en la venta
        /// de busqueda
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <param name="nombreCliente"></param>
        private void BuscarCliente_pasar(string codigoCliente, string nombreCliente, int edad)
        {
            txtIdCliente.Text = codigoCliente;
            txtNombreCliente.Text = nombreCliente;

            MessageBox.Show("" + edad);

            if (edad >= 60)
                descuento = 0.30;
            else
                descuento = 0;
        }

        /// <summary>
        /// Abre una venta para buscar el producto que se desea ingresar al DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            BuscarProducto buscarProducto = new BuscarProducto();
            buscarProducto.pasar += BuscarProducto_pasar;
            buscarProducto.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Es el metodo encargado de pasar los datos del producto que seleccionamos en la venta de busqueda
        /// </summary>
        /// <param name="codigoProducto"></param>
        /// <param name="nombreProducto"></param>
        /// <param name="existenciaProducto"></param>
        /// <param name="precioProducto"></param>
        private void BuscarProducto_pasar(string codigoProducto, string nombreProducto, string existenciaProducto, string precioProducto)
        {
            txtIdProducto.Text = codigoProducto;
            txtNombreProducto.Text = nombreProducto;
            existenciaProduc = Convert.ToInt32(existenciaProducto);
            txtPrecio.Text = precioProducto;
        }

        private void guardarDatos()
        {
            venta.CodigoCliente = Convert.ToInt32(txtIdCliente.Text);
            venta.TipoPago = cbTipoPago.Text;
            venta.Subtotal = subtotal;
            venta.ISV = isv;
            venta.Descuento = descuento;
        }

        public void calculos()
        {
            for(int fila = 1; fila < dgDetalleVenta.Items.Count; fila++)
            {
                subtotal += Convert.ToDouble();
            }

            /*foreach (DataGridRow fila in dgDetalleVenta.ItemsSource)
            {
                subtotal += fila.Item[4]
            }*/

            txtSubtotal.Text = subtotal.ToString();
            txtIsv.Text = Convert.ToString(subtotal * isv);
            txtDescuento.Text = Convert.ToString(subtotal * descuento);
            txtTotal.Text = Convert.ToString(subtotal + (subtotal * isv) - (subtotal * descuento));
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

        
    }
}
