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
    /// Lógica de interacción para Producto.xaml
    /// </summary>
    public partial class Producto : UserControl
    {
        Clases.Producto producto = new Clases.Producto();
        public Producto()
        {
            InitializeComponent();
        }

        //Creacion de la lista privada
        private List<string> Categoria;
        private void MostrarCategorias()
        {
            Categoria = producto.Categoria();
            cbNombreCategoria.ItemsSource = Categoria;
        }
    }
}
