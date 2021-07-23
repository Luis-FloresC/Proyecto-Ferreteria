using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Ferreteira___1
{
    public partial class FormFacturasVentas : Form
    {
        public FormFacturasVentas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor para obtener el codigo de Cliente y Venta
        /// </summary>
        /// <param name="C1">Codigo Venta</param>
        /// <param name="C2">Codigo Cliente</param>
        public FormFacturasVentas(int C1,int C2)
        {
            InitializeComponent();
            codC = C2;
            codV = C1;
        }

        private int codV;
        private int codC;

        /// <summary>
        /// Metodo para mostrar el reporte de la factura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormFacturasVentas_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: esta línea de código carga datos en la tabla 'FerreteriaDataSet2.TotalFacturas' Puede moverla o quitarla según sea necesario.
                this.TotalFacturasTableAdapter.Fill(this.FerreteriaDataSet2.TotalFacturas, codV, codC);
                // TODO: esta línea de código carga datos en la tabla 'FerreteriaDataSet1.Factura' Puede moverla o quitarla según sea necesario.
                this.FacturaTableAdapter.Fill(this.FerreteriaDataSet1.Factura, codV, codC);
                // TODO: esta línea de código carga datos en la tabla 'FerreteriaDataSet1.ClienteFactura' Puede moverla o quitarla según sea necesario.
                this.ClienteFacturaTableAdapter.Fill(this.FerreteriaDataSet1.ClienteFactura, codC);

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
