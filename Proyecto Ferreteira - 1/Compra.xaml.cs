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

        public Compra()
        {
            InitializeComponent();
            MostrarProveedores();
            MostrarProductos();
            calculos();
            //Calculo = new Clases.Calculos { Precio = "0", Cantidad = "0", Flete = "0" };
            //this.DataContext = Calculo;

        }
        private List<Clases.Compras> Carrito = new List<Clases.Compras>();
        private List<Clases.Compras> ObtenerProductos;
        private List<Clases.Compras> ObtenerProveedores;

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
                MessageBox.Show("Por favor llenar todos los datos requeridos", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                    //Activacion de botones
                    btnRealizarCompra.IsEnabled = true;
                    btnEliminarPedido.IsEnabled = true;

                    //Limpiar los texbox de productos
                    txtCantidad.Text = "0";
                    txtPrecio.Text = "0";
                    cmbProducto.SelectedIndex = -1;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message.ToString());
                }
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
                if(Carrito.Count == 0)
                {
                    MessageBox.Show("La lista de compras no puede estar vacía", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
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
                             double.Parse(txtFlete.Text)
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
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ten en cuenta que no puedes hacer multiples compras de un producto en un mismo carrito." +
                    "\nSi ese no es tu problema verifica los datos ingresados", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                MessageBox.Show("Seleccione la compra a eliminar", "AVISO", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            txtFlete.Focus();
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

        private void calculos()
        {
            Calculo = new Clases.Calculos { Precio = "0", Cantidad = "0", Flete = "0" };
            this.DataContext = Calculo;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberValidationTextBox2(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
