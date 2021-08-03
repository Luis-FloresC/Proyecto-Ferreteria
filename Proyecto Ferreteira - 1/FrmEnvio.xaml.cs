using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Proyecto_Ferreteira___1
{

    /// <summary>
    /// Lógica de interacción para FrmEnvio.xaml
    /// </summary>
    /// 


    public partial class FrmEnvio : Window
    {
        public int CodigoCliente;
        public int CodigoVenta;
        public FrmEnvio()
        {
            InitializeComponent();
        }
        public FrmEnvio(int codigoVenta, int codigoCliente)
        {
            InitializeComponent();
            CodigoCliente = codigoCliente;
            CodigoVenta = codigoVenta;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            FormFacturasVentas ventas = new FormFacturasVentas(CodigoVenta, CodigoCliente);
            ventas.Show();
        }

        Clases.clsEnvio envio = new Clases.clsEnvio();

        /// <summary>
        /// Metodo para validar si la cadena de texto solo contiene espacios
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        private bool CadenaSoloEspacios(string cadena)
        {

            try
            {
                String source = cadena; //Original text

                if (source.Trim().Length <= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return false;
            }

        }
        /// <summary>
        /// Validación para que solamente se acepten campos numéricos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }



        /// <summary>
        /// Metodo para validar el numero de telefono
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public bool VerificarNumeroTelefono(string numero)
        {
            try
            {

                if (numero.Length > 8 || numero.Length < 8)
                {
                    return false;
                }

                if (int.Parse(numero.Substring(0, 1)) == 9 || int.Parse(numero.Substring(0, 1)) == 8 || int.Parse(numero.Substring(0, 1)) == 3 || int.Parse(numero.Substring(0, 1)) == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Insertar los datos solicitados en la BDD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtDireccion.Text == String.Empty || txtTelefono.Text == String.Empty)
                {
                    MessageBox.Show("¡Por favor, llenar todos los campos solicitados!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string direccion = txtDireccion.Text;
                    string telefono = txtTelefono.Text;
               
                    if (direccion.Length <= 1)
                    {
                        MessageBox.Show("La dirección no puede ser menor o igual a un caracter", "Advertencia",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if(!VerificarNumeroTelefono(telefono))
                    {
                        MessageBox.Show("Ingrese un número de teléfono válido", "Advertencia",
                           MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                   else if (CadenaSoloEspacios(direccion) || CadenaSoloEspacios(telefono))
                    {
                        MessageBox.Show("No se pueden ingresar solo espacios",
                               "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        envio.envio(txtTelefono.Text, txtDireccion.Text);
                        MessageBox.Show("El envio se registró correctamente", "Aviso",
                           MessageBoxButton.OK, MessageBoxImage.Information);
                        FormFacturasVentas ventas = new FormFacturasVentas(CodigoVenta, CodigoCliente);
                        ventas.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor verifique que esta ingresando los valores correctos en los campos", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
