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
        //Listas
        List<Clases.ClsProducto> productos = new List<Clases.ClsProducto>();

        //Objetos
        Clases.ClsVenta venta = new Clases.ClsVenta();

        //Variables globales
        double existenciaProduc = 0;
        double descuento = 0;
        //Constantes
        const double isv = 0.15;

        private double Monto = 0;
        private double Cambio = 0;
        public Ventas()
        {
            InitializeComponent();
            RegistroCl.IsChecked = true;
            Properties.Settings.Default.PagoCorrecto = false;

        }


        private void AggAlCarrito()
        {
            var Item = new Clases.ClsProducto
            {
                ID = Convert.ToInt32(txtIdProducto.Text),
                PRODUCTO = txtNombreProducto.Text,
                PRECIO = double.Parse(txtPrecio.Text),
                CANTIDAD = int.Parse(txtCantidad.Text),
                IMPORTE = (Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text))
            };
            //Ingresa el producto en la lista productos
            productos.Add(new Clases.ClsProducto
            {
                ID = Convert.ToInt32(txtIdProducto.Text),
                PRECIO = double.Parse(txtPrecio.Text),
                CANTIDAD = int.Parse(txtCantidad.Text),
                IMPORTE = (Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text))
            });

            dgDetalleVenta.Items.Add(Item);
            calculos();
            limpiarProducto();
        }

        //METODOS



        private void Cambio_pasar(double cambio, double monto, bool Resp)
        {
            if (!Resp)
            {
                
                btnRealizarPago.IsEnabled = true;
                MessageBox.Show("Debe Realizar el pago para realizar la compra");
            }
            else
            {
                Cambio = cambio;
                Monto = monto;
            }

        }

        private void IngresarCambio()
        {
            VentanaModal ventanaModal = new VentanaModal("Venta", double.Parse(txtTotal.Text));
            ventanaModal.pasar += Cambio_pasar;
            ventanaModal.Show();
        }

        /// <summary>
        /// Agrega el producto seleccionado al DataGrid y a la lista productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtCantidad.Text.Equals(string.Empty) || Convert.ToInt32(txtCantidad.Text) <= 0)
                {
                    MessageBox.Show("La cantidad que usted ingreso no es valida", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (Convert.ToInt32(txtCantidad.Text) > existenciaProduc)
                {
                    MessageBox.Show("La existencia de este producto es insuficiente", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (cbTipoPago.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un tipo de pago", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                { 
                    if (productos.Count == 0)
                    {
                        AggAlCarrito();
                        btnRealizarPago.IsEnabled = true;
                    }
                    else
                    {

                        foreach (Clases.ClsProducto Item in productos)
                        {

                            if (int.Parse(txtIdProducto.Text) == Item.ID)
                            {
                                MessageBox.Show("El Producto ya se encuentra en el detalle de la venta...", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                                limpiarProducto();
                            }
                            else
                            {
                                AggAlCarrito();
                                btnRealizarPago.IsEnabled = true;
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {
               
            }
          
        }

        /// <summary>
        /// Elimina el producto seleccionado del DataGrid y de la lista productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int indice = dgDetalleVenta.SelectedIndex;

            if (indice == -1)
            {
                MessageBox.Show("Seleccione el producto a eliminar","Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                dgDetalleVenta.Items.RemoveAt(indice);
                productos.RemoveAt(indice);

                calculos();
            }
        }

        /// <summary>
        /// Se encarga de registrar la compra en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFactura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validacion())
                {
                    guardarDatos();
                    venta.facturar();

                    //Ciclo que recorre la lista producto para ingresar cada producto del detalle
                    foreach (var producto in productos)
                    {
                        venta.CodigoProducto = producto.ID;
                        venta.PrecioProducto = producto.PRECIO;
                        venta.CantidadProducto = producto.CANTIDAD;

                        venta.agregarDetalle();
                    }

                    MessageBox.Show("Factura realizada con exito", "Aviso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    MessageBox.Show("Su Cambio es: " + Cambio, "Aviso");
                    Properties.Settings.Default.PagoCorrecto = false;
                   


                    if (MessageBox.Show("¿Necesita servicio adomicilio?", "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        FrmEnvio Envio = new FrmEnvio(venta.CodigoVenta(), venta.CodigoCliente);
                        Envio.Show();
                    }
                    else
                    {
                        FormFacturasVentas ventas = new FormFacturasVentas(venta.CodigoVenta(), venta.CodigoCliente);
                        ventas.Show();
                        limpiar();
                    }


                }
                else
                {
                    if (Properties.Settings.Default.PagoCorrecto == false)
                    {
                        MessageBox.Show("Realizar el pago antes de facturar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if(txtIdCliente.Text.Equals("") || cbTipoPago.SelectedIndex == -1 || dgDetalleVenta.Items.Count <= 0)
                    {
                        MessageBox.Show("Llene todos los campos antes de realizar la venta.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                  
                }
            }
            catch(Exception)
            {
                MessageBox.Show("¡Error al facturar!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
     

        }

        /// <summary>
        /// Cancela la compra que se esta llevando acabo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
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
        /// y determina su descuento segun la edad
        /// de busqueda
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <param name="nombreCliente"></param>
        private void BuscarCliente_pasar(string codigoCliente, string nombreCliente, int edad)
        {
            txtIdCliente.Text = codigoCliente;
            txtNombreCliente.Text = nombreCliente;
            
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
            txtPrecio.Text = Math.Round(double.Parse(precioProducto)).ToString();
        }

        /// <summary>
        /// Guarda los datos de la venta en la clase ClsVenta para su facturación
        /// </summary>
        private void guardarDatos()
        {
            venta.CodigoCliente = Convert.ToInt32(txtIdCliente.Text);
            venta.TipoPago = cbTipoPago.Text;
            venta.Subtotal = Convert.ToDouble(txtSubtotal.Text);
            venta.ISV = isv;
            venta.Descuento = descuento;
            venta.Monto = Monto;
            venta.Cambio = Cambio;
        }

        /// <summary>
        /// Cada vez que se agregue o elimine un producto este metodo se encargara
        /// de realizar los calculos totales de la compra
        /// </summary>
        public void calculos()
        {
            try
            {
                double subtotal = 0;

                //Recorre todos los campos de importe de la lista para calcular el subtotal
                foreach (var producto in productos)
                {
                    subtotal += Convert.ToDouble(producto.IMPORTE);
                }

                txtSubtotal.Text = subtotal.ToString();
                txtIsv.Text = Convert.ToString(subtotal * isv);
                txtDescuento.Text = Convert.ToString(subtotal * descuento);
                txtTotal.Text = Convert.ToString(subtotal + (subtotal * isv) - (subtotal * descuento));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        /// <summary>
        /// Metodo de limpiar la informacion de los productos en los textBox
        /// </summary>
        private void limpiarProducto()
        {
            txtIdProducto.Text = "";
            txtNombreProducto.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
        }

        /// <summary>
        /// Limpia toda la informacion de la venta
        /// </summary>
        private void limpiar()
        {
            txtNombreCliente.Text = "";
            txtIdCliente.Text = "";
            cbTipoPago.SelectedIndex = -1;
            txtSubtotal.Text = "0";
            txtIsv.Text = "0";
            txtDescuento.Text = "0";
            txtSubtotal.Text = "0";
            txtTotal.Text = "0";
            limpiarProducto();
            dgDetalleVenta.Items.Clear();
            productos.Clear();
            RegistroCl.IsChecked = true;
        }

        /// <summary>
        /// Valida que todos los campos necesarios para  la venta esten llenos
        /// </summary>
        /// <returns></returns>
        private bool validacion()
        {
            if (!txtIdCliente.Text.Equals("") && cbTipoPago.SelectedIndex > -1 && dgDetalleVenta.Items.Count > 0 && Properties.Settings.Default.PagoCorrecto == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Valida solo numeros en el texbox de cantidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCantidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }


        

        private void RegistroCl_Checked(object sender, RoutedEventArgs e)
        {
            Clases.UserData user = new Clases.UserData();

            if (RegistroCl.IsChecked != true)
            {

                btnBuscarCliente.IsEnabled = false;
                txtIdCliente.Text = user.BuscarClienteAnonimo().ToString();
                txtNombreCliente.Text = "-";
                
            }
            else
            {

                btnBuscarCliente.IsEnabled = true;
                txtIdCliente.Text = string.Empty;
                txtNombreCliente.Text = string.Empty;

            }

        }

        private void cbTipoPago_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnRealizarPago_Click(object sender, RoutedEventArgs e)
        {
            IngresarCambio();
            btnRealizarPago.IsEnabled = false;
        }
    }
}
