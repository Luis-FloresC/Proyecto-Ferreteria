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
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : UserControl
    {
        List<Clases.ClsProducto> productos = new List<Clases.ClsProducto>();

        public Ventas()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            var Item = new Clases.ClsProducto
            {
                ID = Convert.ToInt32(txtIdProducto.Text),
                PRODUCTO = txtNombreProducto.Text,
                PRECIO = double.Parse(txtPrecio.Text),
                CANTIDAD = int.Parse(txtCantidad.Text),
                IMPORTE = (Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text))
            };
            productos.Add(new Clases.ClsProducto
            {
                ID = Convert.ToInt32(txtIdProducto.Text),
                PRECIO = double.Parse(txtPrecio.Text),
                CANTIDAD = int.Parse(txtCantidad.Text)
            });
            dgDetalleVenta.Items.Add(Item);

        }


    }
}
