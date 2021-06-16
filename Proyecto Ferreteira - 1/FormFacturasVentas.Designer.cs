
namespace Proyecto_Ferreteira___1
{
    partial class FormFacturasVentas
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFacturasVentas));
            this.FacturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FerreteriaDataSet1 = new Proyecto_Ferreteira___1.FerreteriaDataSet1();
            this.ClienteFacturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TotalFacturasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FerreteriaDataSet2 = new Proyecto_Ferreteira___1.FerreteriaDataSet2();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.FacturaTableAdapter = new Proyecto_Ferreteira___1.FerreteriaDataSet1TableAdapters.FacturaTableAdapter();
            this.ClienteFacturaTableAdapter = new Proyecto_Ferreteira___1.FerreteriaDataSet1TableAdapters.ClienteFacturaTableAdapter();
            this.TotalFacturasTableAdapter = new Proyecto_Ferreteira___1.FerreteriaDataSet2TableAdapters.TotalFacturasTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.FacturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClienteFacturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalFacturasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet2)).BeginInit();
            this.SuspendLayout();
            // 
            // FacturaBindingSource
            // 
            this.FacturaBindingSource.DataMember = "Factura";
            this.FacturaBindingSource.DataSource = this.FerreteriaDataSet1;
            // 
            // FerreteriaDataSet1
            // 
            this.FerreteriaDataSet1.DataSetName = "FerreteriaDataSet1";
            this.FerreteriaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ClienteFacturaBindingSource
            // 
            this.ClienteFacturaBindingSource.DataMember = "ClienteFactura";
            this.ClienteFacturaBindingSource.DataSource = this.FerreteriaDataSet1;
            // 
            // TotalFacturasBindingSource
            // 
            this.TotalFacturasBindingSource.DataMember = "TotalFacturas";
            this.TotalFacturasBindingSource.DataSource = this.FerreteriaDataSet2;
            // 
            // FerreteriaDataSet2
            // 
            this.FerreteriaDataSet2.DataSetName = "FerreteriaDataSet2";
            this.FerreteriaDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "FacturarVentas";
            reportDataSource1.Value = this.FacturaBindingSource;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.ClienteFacturaBindingSource;
            reportDataSource3.Name = "DataSet2";
            reportDataSource3.Value = this.TotalFacturasBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Proyecto_Ferreteira___1.FacturaVentas.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(675, 749);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // FacturaTableAdapter
            // 
            this.FacturaTableAdapter.ClearBeforeFill = true;
            // 
            // ClienteFacturaTableAdapter
            // 
            this.ClienteFacturaTableAdapter.ClearBeforeFill = true;
            // 
            // TotalFacturasTableAdapter
            // 
            this.TotalFacturasTableAdapter.ClearBeforeFill = true;
            // 
            // FormFacturasVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 749);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFacturasVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Factura de Ventas";
            this.Load += new System.EventHandler(this.FormFacturasVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FacturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClienteFacturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalFacturasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource FacturaBindingSource;
        private FerreteriaDataSet1 FerreteriaDataSet1;
        private System.Windows.Forms.BindingSource ClienteFacturaBindingSource;
        private FerreteriaDataSet1TableAdapters.FacturaTableAdapter FacturaTableAdapter;
        private FerreteriaDataSet1TableAdapters.ClienteFacturaTableAdapter ClienteFacturaTableAdapter;
        private System.Windows.Forms.BindingSource TotalFacturasBindingSource;
        private FerreteriaDataSet2 FerreteriaDataSet2;
        private FerreteriaDataSet2TableAdapters.TotalFacturasTableAdapter TotalFacturasTableAdapter;
    }
}