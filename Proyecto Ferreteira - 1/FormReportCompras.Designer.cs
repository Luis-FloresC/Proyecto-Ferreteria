
namespace Proyecto_Ferreteira___1
{
    partial class FormReportCompras
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.DeatalleProveedoresBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FerreteriaDataSet10 = new Proyecto_Ferreteira___1.FerreteriaDataSet10();
            this.ReporteComprasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DeatalleProveedoresTableAdapter = new Proyecto_Ferreteira___1.FerreteriaDataSet10TableAdapters.DeatalleProveedoresTableAdapter();
            this.ReporteComprasTableAdapter = new Proyecto_Ferreteira___1.FerreteriaDataSet10TableAdapters.ReporteComprasTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DeatalleProveedoresBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteComprasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // DeatalleProveedoresBindingSource
            // 
            this.DeatalleProveedoresBindingSource.DataMember = "DeatalleProveedores";
            this.DeatalleProveedoresBindingSource.DataSource = this.FerreteriaDataSet10;
            // 
            // FerreteriaDataSet10
            // 
            this.FerreteriaDataSet10.DataSetName = "FerreteriaDataSet10";
            this.FerreteriaDataSet10.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ReporteComprasBindingSource
            // 
            this.ReporteComprasBindingSource.DataMember = "ReporteCompras";
            this.ReporteComprasBindingSource.DataSource = this.FerreteriaDataSet10;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetListProveedores";
            reportDataSource1.Value = this.DeatalleProveedoresBindingSource;
            reportDataSource2.Name = "DataSetDetallesCompras";
            reportDataSource2.Value = this.ReporteComprasBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Proyecto_Ferreteira___1.ReporteDeCompras.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1085, 681);
            this.reportViewer1.TabIndex = 0;
            // 
            // DeatalleProveedoresTableAdapter
            // 
            this.DeatalleProveedoresTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteComprasTableAdapter
            // 
            this.ReporteComprasTableAdapter.ClearBeforeFill = true;
            // 
            // FormReportCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 681);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormReportCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormReportCompras";
            this.Load += new System.EventHandler(this.FormReportCompras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DeatalleProveedoresBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReporteComprasBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DeatalleProveedoresBindingSource;
        private FerreteriaDataSet10 FerreteriaDataSet10;
        private System.Windows.Forms.BindingSource ReporteComprasBindingSource;
        private FerreteriaDataSet10TableAdapters.DeatalleProveedoresTableAdapter DeatalleProveedoresTableAdapter;
        private FerreteriaDataSet10TableAdapters.ReporteComprasTableAdapter ReporteComprasTableAdapter;
    }
}