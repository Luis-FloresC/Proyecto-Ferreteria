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
using System.Windows.Shapes;

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
                if (CadenaSoloEspacios(txtTelefono.Text) || CadenaSoloEspacios(txtDireccion.Text))
                {
                    //ERROR
                }
                else
                { 
                  //guardar
                }
            }
            catch (Exception)
            {

                throw;
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
}
