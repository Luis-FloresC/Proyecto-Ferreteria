using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;


namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para Compra.xaml
    /// </summary>
    public partial class Compra : UserControl
    {
        public Clases.Calculos Calculo { get; set; }

        Clases.Compras compra = new Clases.Compras();

        const double isv = 0.12;
        const double descuento = 0.10;
     

        public Compra()
        {
            InitializeComponent();
            MostrarProveedores();
            MostrarProductos();
            calculos();
            btnRealizarCompra.IsEnabled=false;

        }
        private List<Clases.Compras> Carrito = new List<Clases.Compras>();
        private List<Clases.Compras> ObtenerProductos;
        private List<Clases.Compras> ObtenerProveedores;
        private double Monto=0;
        private double Cambio = 0;

        /// <summary>
        /// Llena el combobox de Proveedores
        /// </summary>
        private void MostrarProveedores()
        {
            ObtenerProveedores = compra.LlenarComboProveedores();
            cmbProveedor.SelectedValuePath = "IdProveedor";
            cmbProveedor.DisplayMemberPath = "NombreProveedor";
            cmbProveedor.ItemsSource = ObtenerProveedores;
        }
        /// <summary>
        /// Llena el combobox de Productos
        /// </summary>
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
            bool comprobacion = Comprobacion();
            if (comprobacion == true)
            {
                MessageBox.Show("Por favor, llenar todos los datos requeridos", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
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
                    CalcularDetalle();
                    //Activacion de botones
                    btnRealizarCompra.IsEnabled = false;
                    btnRealizarPago.IsEnabled = true;
                    btnEliminarPedido.IsEnabled = true;

                    //Limpiar los texbox de productos
                    txtCantidad.Text = "0";
                    txtPrecio.Text = "0";
                    cmbProducto.SelectedIndex = -1;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString(),"Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }

        }

      
        /// <summary>
        /// Es la encargada de ingresar un cambio
        /// </summary>
      private void IngresarCambio()
        {
            VentanaModal ventanaModal = new VentanaModal("Compra", double.Parse(txtTotal.Text));
            ventanaModal.pasar += Cambio_pasar;
            ventanaModal.Show();
           
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
                if(Carrito.Count == 0)
                {
                    MessageBox.Show("La lista de compras no puede estar vacía", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
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
                                 Convert.ToDouble(Item.Precio.ToString()),
                                 double.Parse(txtSubtotal.Text),
                                 double.Parse(txtISV.Text),
                                 double.Parse(txtDescuento.Text),
                                 Codigo,
                                 double.Parse(txtFlete.Text),
                                 Cambio,
                                 Monto
                                );
                            Resultado = compras.GuardarCompras();
                        }
                        MessageBox.Show(Resultado, "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Habilitacion y deshabilitacion de botones
                        btnAgregarPedido.IsEnabled = true;
                        btnRealizarCompra.IsEnabled = false;
                        btnEliminarPedido.IsEnabled = false;

                        //Limpiar
                        Limpieza();
                        dgbInformacion.Items.Clear();
                        Carrito.Clear();
                    MessageBox.Show("Su cambio es: " + Cambio, "Aviso",MessageBoxButton.OK,MessageBoxImage.Information);


                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ten en cuenta que no puedes hacer múltiples compras de un producto en un mismo carrito." +
                    "\nSi ese no es tu problema verifica los datos ingresados", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
          
        }

        /// <summary>
        /// se encarga de hacer el cambio
        /// </summary>
        /// <param name="cambio">Trae el parametro de cambio</param>
        /// <param name="monto">Trae el parmetro de monto</param>
        /// <param name="Resp">Trae el parametro de respuesta</param>
        private void Cambio_pasar(double cambio, double monto,bool Resp)
        {
            if(!Resp)
            {
                btnRealizarCompra.IsEnabled = false;
                btnRealizarPago.IsEnabled = true;
                MessageBox.Show("Debe hacer el pago para realizar la compra", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Cambio = cambio;
                Monto = monto;
            }
         
        }
        /// <summary>
        /// Se encarga de la elimanacion de datos del data Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Eliminar_Click_3(object sender, RoutedEventArgs e)
        {
            int index = dgbInformacion.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Seleccione la compra a eliminar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                for (int i = 0; i < Carrito.Count; i++)
                {
                    if (i == (index))
                    {
                        Carrito.RemoveAt(i);

                    }
                }
                dgbInformacion.Items.RemoveAt(index);
                btnAgregarPedido.IsEnabled = true;
                calculos();
                CalcularDetalle();
            }
        }
        /// <summary>
        /// Se encarga de la establecer el valor de las entradas de datos a su estado original
        /// </summary>
        private void Limpieza()
        {
            cmbProducto.SelectedValue = null;
            cmbProveedor.SelectedValue = null;
            txtCantidad.Text = String.Empty;
            txtDescuento.Text = String.Empty;
            txtFlete.Text = String.Empty;
            txtISV.Text = String.Empty;
            txtPrecio.Text = String.Empty;
            txtSubtotal.Text = String.Empty;
            txtTotal.Text = String.Empty;
        }
        /// <summary>
        /// Hace la validacíon de la entrada de datos
        /// </summary>
        /// <returns>True si la entrada de datos es erronea False si la entrada de datos es correcta</returns>
        private bool Comprobacion()
        {
            bool resultado;
            if (cmbProducto.SelectedValue == null || cmbProveedor.SelectedValue == null || txtCantidad.Text == "0" ||
                txtPrecio.Text == "0"
               )
            { resultado = true; }
            else { resultado = false; }
            return resultado;
        }

        /// <summary>
        /// Funcion encargada de calcular el detalle de la compra
        /// </summary>
        public void CalcularDetalle()
        {
            try
            {
                double subtotal = 0;

                //Recorre todos los campos de importe de la lista para calcular el subtotal
                foreach (var producto in Carrito)
                {
                    subtotal += (Convert.ToDouble(producto.Precio) * Convert.ToDouble(producto.Cantidad));
                }

                txtSubtotal.Text = subtotal.ToString();
                txtISV.Text = Convert.ToString(subtotal * isv);
                txtDescuento.Text = Convert.ToString(subtotal * descuento);
                txtTotal.Text = Convert.ToString(subtotal + (subtotal * isv) - (subtotal * descuento) + double.Parse(txtFlete.Text));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        /// <summary>
        /// restablecer los valores a  0
        /// </summary>
        private void calculos()
        {
            Calculo = new Clases.Calculos { Precio = "0", Cantidad = "0", Flete = "0" };
            this.DataContext = Calculo;
        }

        /// <summary>
        /// Validar el textbox para que solo acepte datos numericos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Validar el textbox para que solo acepte datos numericos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Obtener el precio de dicho producto cuando cambie de producto en el combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Clases.UserData data = new Clases.UserData();
            string Precio = data.BuscarPrecioProducto(Convert.ToInt32(cmbProducto.SelectedValue)).ToString();

            txtPrecio.Text = Precio;

        }

        /// <summary>
        /// Restablece el flete a vacio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFlete_MouseEnter(object sender, MouseEventArgs e)
        {
            if(txtFlete.Text == "0")
            {
                txtFlete.Text = "";
            }
        }

        private void txtFlete_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txtFlete.Text == "")
            {
                txtFlete.Text = "0";
            }
        }

        private void txtCantidad_MouseEnter(object sender, MouseEventArgs e)
        {
            if (txtCantidad.Text == "0")
            {
                txtCantidad.Text = "";
            }
        }

        private void txtCantidad_MouseLeave(object sender, MouseEventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                txtCantidad.Text = "0";
            }
        }
        /// <summary>
        /// Realizo el pago de la compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRealizarPago_Click(object sender, RoutedEventArgs e)
        {
            IngresarCambio();
            btnRealizarCompra.IsEnabled = true;
            btnRealizarPago.IsEnabled = false;
            btnAgregarPedido.IsEnabled = false;

           
        }

      
    }
}
