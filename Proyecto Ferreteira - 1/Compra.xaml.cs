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
    /// Lógica de interacción para Compra.xaml
    /// </summary>
    public partial class Compra : UserControl
    {
        
     
        public Compra()
        {
            InitializeComponent();  
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clases.Compras compra = new Clases.Compras();
            compra.MostrarProveedores();
            cmbProveedor.Items.Add(compra.NombreProveedor);
            txtIdProveedor.Text = compra.IdProveedor.ToString();
        }

 
    }
}
