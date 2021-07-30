using System;
using System.Windows;

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



        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                string direccion = txtDireccion.Text;
                string telefono = txtDireccion.Text;

                if (CadenaSoloEspacios(direccion) || CadenaSoloEspacios(telefono))
                {
                    MessageBox.Show("No se puede ingresar el nombre de un producto con espacios",
                           "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (direccion.Length <= 1)
                {
                    MessageBox.Show("La dirección no puede ser menor o igual a un caracter", "Advertencia",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    envio.envio(txtTelefono.Text,txtDireccion.Text);
                    MessageBox.Show("El envio se registro correctamente", "Aviso",
                       MessageBoxButton.OK, MessageBoxImage.Information);
                    FormFacturasVentas ventas = new FormFacturasVentas(CodigoVenta, CodigoCliente);
                    ventas.Show();
                    this.Close();

                    

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor verifique que esta ingresando los valores correctos en los campos", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }


}
