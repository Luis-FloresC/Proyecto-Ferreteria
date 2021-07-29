using System;
using System.Windows;

namespace Proyecto_Ferreteira___1
{
    /// <summary>
    /// Lógica de interacción para FrmEnvio.xaml
    /// </summary>
    public partial class FrmEnvio : Window
    {
        public FrmEnvio()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string direccion2 = txtDireccion.Text;
                if (CadenaSoloEspacios(txtTelefono.Text) || CadenaSoloEspacios(txtDireccion.Text))
                {
                    MessageBox.Show("No se puede ingresar el nombre de un producto con espacios",
                           "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (direccion2.Length <= 1)
                {
                    MessageBox.Show("La dirección no puede ser menor o igual a un caracter", "Advertencia",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    int telefono = Convert.ToInt32(txtTelefono.Text);
                    string direccion = txtDireccion.Text;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor verifique que esta ingresando los valores correctos en los campos", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

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

}
