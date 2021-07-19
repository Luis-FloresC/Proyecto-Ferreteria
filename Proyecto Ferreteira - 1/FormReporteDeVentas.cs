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

        public FormReporteDeVentas(DateTime fechaInicio,DateTime fechaFinal)
        {
            InitializeComponent();
            FechaFinal = fechaFinal;
            FechaInicio = fechaInicio;
        //    MessageBox.Show(FechaFinal.ToString());
        }

        private void FormReporteDeVentas_Load(object sender, EventArgs e)
        {

            // TODO: esta línea de código carga datos en la tabla 'FerreteriaDataSet5.ReporteVentas' Puede moverla o quitarla según sea necesario.
            //     this.ReporteVentasTableAdapter.Fill(this.FerreteriaDataSet8.ReporteVentas, FechaInicio.ToString(), FechaFinal.ToString());

            try
            {
                string txtFechaFinal = FechaFinal.ToString().Replace("00:00:00", "23:59:59");
                this.reporteVentasTableAdapter1.Fill(this.ferreteriaDataSet8.ReporteVentas, FechaInicio.ToString(), txtFechaFinal);
                this.ventasDiasTableAdapter.Fill(this.ferreteriaDataSet9.VentasDias, FechaInicio.ToString(), txtFechaFinal);

                this.reportViewer1.RefreshReport();
            }
            catch ( Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        
            
         
        }
    }
}
