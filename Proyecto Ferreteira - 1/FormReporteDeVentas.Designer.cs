
namespace Proyecto_Ferreteira___1
{
    partial class FormReporteDeVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReporteVentasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FerreteriaDataSet5 = new Proyecto_Ferreteira___1.FerreteriaDataSet5();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReporteVentasTableAdapter = new Proyecto_Ferreteira___1.FerreteriaDataSet5TableAdapters.ReporteVentasTableAdapter();
            this.ferreteriaDataSet8 = new Proyecto_Ferreteira___1.FerreteriaDataSet8();
            this.reporteVentasBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.reporteVentasTableAdapter1 = new Proyecto_Ferreteira___1.FerreteriaDataSet8TableAdapters.ReporteVentasTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteVentasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ferreteriaDataSet8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reporteVentasBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // ReporteVentasBindingSource
            // 
            this.ReporteVentasBindingSource.DataMember = "ReporteVentas";
            this.ReporteVentasBindingSource.DataSource = this.FerreteriaDataSet5;
            // 
            // FerreteriaDataSet5
            // 
            this.FerreteriaDataSet5.DataSetName = "FerreteriaDataSet5";
            this.FerreteriaDataSet5.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ReportesDataSet";
            reportDataSource1.Value = this.reporteVentasBindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Proyecto_Ferreteira___1.ReportVentas.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(980, 693);
            this.reportViewer1.TabIndex = 0;
            // 
            // ReporteVentasTableAdapter
            // 
            this.ReporteVentasTableAdapter.ClearBeforeFill = true;
            // 
            // ferreteriaDataSet8
            // 
            this.ferreteriaDataSet8.DataSetName = "FerreteriaDataSet8";
            this.ferreteriaDataSet8.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reporteVentasBindingSource1
            // 
            this.reporteVentasBindingSource1.DataMember = "ReporteVentas";
            this.reporteVentasBindingSource1.DataSource = this.ferreteriaDataSet8;
            // 
            // reporteVentasTableAdapter1
            // 
            this.reporteVentasTableAdapter1.ClearBeforeFill = true;
            // 
            // FormReporteDeVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 693);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormReporteDeVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormReporteDeVentas";
            this.Load += new System.EventHandler(this.FormReporteDeVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReporteVentasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ferreteriaDataSet8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reporteVentasBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ReporteVentasBindingSource;
        private FerreteriaDataSet5 FerreteriaDataSet5;
        private FerreteriaDataSet5TableAdapters.ReporteVentasTableAdapter ReporteVentasTableAdapter;
        private System.Windows.Forms.BindingSource reporteVentasBindingSource1;
        private FerreteriaDataSet8 ferreteriaDataSet8;
        private FerreteriaDataSet8TableAdapters.ReporteVentasTableAdapter reporteVentasTableAdapter1;
    }
}