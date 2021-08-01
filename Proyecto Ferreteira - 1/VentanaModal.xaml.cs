using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para VentanaModal.xaml
    /// </summary>
    public partial class VentanaModal : Window
    {
        public VentanaModal()
        {
            InitializeComponent();
        }
        private string Descripcion;
        private double Total;
        private double Monto;
        private double Cambio=0;

        public delegate void pasarCambio(double cambio,double monto,bool Resp);
        public event pasarCambio pasar;

        /// <summary>
        /// Constructor para obtner un descripcion y el total a pagar
        /// </summary>
        /// <param name="descripcion">la descripcion puede ser Compra/Venta</param>
        /// <param name="total"></param>
        public VentanaModal(string descripcion,double total)
        {
            InitializeComponent();
            Descripcion = descripcion;
            Total = total;
            Properties.Settings.Default.OpenVentana = true;
        }

        /// <summary>
        /// Boton Aceptar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Descripcion == "Compra")
                {
                    if(CalcularCambio() == -1)
                    {
                        MessageBox.Show("Por favor ingrese una cantidad mayor o igual al total...","Advertencia",MessageBoxButton.OK,MessageBoxImage.Warning);
                        Properties.Settings.Default.PagoCorrecto = false;
                    }
                    else
                    {
                        Cambio = CalcularCambio();
                        pasar(Cambio,Monto,true);
                        MessageBox.Show("Su pago se realizó exitosamente", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        Properties.Settings.Default.OpenVentana = false;
                        Properties.Settings.Default.PagoCorrecto = true;
                        this.Visibility = Visibility.Hidden;
                    }
                  
                }
                else
                {
                    if (CalcularCambio() == -1)
                    {
                        MessageBox.Show("Por favor ingrese una cantidad mayor o igual a " + Total, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        Properties.Settings.Default.PagoCorrecto = false;
                    }
                    else
                    {
                        Cambio = CalcularCambio();
                        pasar(Cambio, Monto, true);
                        MessageBox.Show("Su pago se realizó exitosamente", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                        Properties.Settings.Default.OpenVentana = false;
                        Properties.Settings.Default.PagoCorrecto = true;
                        this.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
         


        }

        /// <summary>
        /// Permitir solo numeros 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Metodo para calcular el cambio de compra y venta
        /// </summary>
        /// <returns></returns>
        private double CalcularCambio()
        {
            try
            {
                bool MontoCorrecto = double.TryParse(txtCambio.Text, out double monto);

                if(MontoCorrecto && monto >= Total)
                {
                    Monto = monto;
                    return (monto - Total);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return -1;
            }

           
        }


        /// <summary>
        /// Metodo para cancelar el metodo de pago
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.PagoCorrecto = false;
                Properties.Settings.Default.OpenVentana = false;
                pasar(Cambio, Monto, false);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }

        }
    }
}
