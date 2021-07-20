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
    public partial class FormReportCompras : Form
    {


        public DateTime FechaInicio;
        public DateTime FechaFinal;
        public FormReportCompras()
        {
            InitializeComponent();
        }


        public FormReportCompras(DateTime fromDate ,DateTime toDate)
        {
            InitializeComponent();
            FechaInicio = fromDate;
            FechaFinal = toDate;
        }

        private void FormReportCompras_Load(object sender, EventArgs e)
        {
            try
            {
                string txtFechaFinal = FechaFinal.ToString().Replace("00:00:00", "23:59:59");
                // TODO: esta línea de código carga datos en la tabla 'FerreteriaDataSet10.DeatalleProveedores' Puede moverla o quitarla según sea necesario.
                this.DeatalleProveedoresTableAdapter.Fill(this.FerreteriaDataSet10.DeatalleProveedores,FechaInicio.ToString(),txtFechaFinal);
                // TODO: esta línea de código carga datos en la tabla 'FerreteriaDataSet10.ReporteCompras' Puede moverla o quitarla según sea necesario.
                this.ReporteComprasTableAdapter.Fill(this.FerreteriaDataSet10.ReporteCompras, FechaInicio.ToString(), txtFechaFinal);

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message.ToString(),"Error");
            }
           
        }
    }
}
