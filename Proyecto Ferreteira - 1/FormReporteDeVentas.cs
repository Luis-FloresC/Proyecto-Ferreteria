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
    public partial class FormReporteDeVentas : Form
    {

        public DateTime FechaInicio; 
        public DateTime FechaFinal;
        public FormReporteDeVentas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor que recibe los parametro de un rango de la fecha para general el reporte
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFinal"></param>
        public FormReporteDeVentas(DateTime fechaInicio,DateTime fechaFinal)
        {
            InitializeComponent();
            FechaFinal = fechaFinal;
            FechaInicio = fechaInicio;
   
        }

        /// <summary>
        /// Metodo para cargar el reporte de ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormReporteDeVentas_Load(object sender, EventArgs e)
        {

            try
            { 
                this.reporteVentasTableAdapter1.Fill(this.ferreteriaDataSet8.ReporteVentas, FechaInicio,FechaFinal);
                this.ventasDiasTableAdapter.Fill(this.ferreteriaDataSet9.VentasDias, FechaInicio, FechaFinal);
                this.reportViewer1.RefreshReport();
            }
            catch ( Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
    }
}
