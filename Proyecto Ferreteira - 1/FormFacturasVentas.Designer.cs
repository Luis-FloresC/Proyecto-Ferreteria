﻿
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.FerreteriaDataSet1 = new Proyecto_Ferreteira___1.FerreteriaDataSet1();
            this.FacturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FacturaTableAdapter = new Proyecto_Ferreteira___1.FerreteriaDataSet1TableAdapters.FacturaTableAdapter();
            this.ClienteFacturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ClienteFacturaTableAdapter = new Proyecto_Ferreteira___1.FerreteriaDataSet1TableAdapters.ClienteFacturaTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FacturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClienteFacturaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "FacturarVentas";
            reportDataSource1.Value = this.FacturaBindingSource;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.ClienteFacturaBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Proyecto_Ferreteira___1.FacturaVentas.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // FerreteriaDataSet1
            // 
            this.FerreteriaDataSet1.DataSetName = "FerreteriaDataSet1";
            this.FerreteriaDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FacturaBindingSource
            // 
            this.FacturaBindingSource.DataMember = "Factura";
            this.FacturaBindingSource.DataSource = this.FerreteriaDataSet1;
            // 
            // FacturaTableAdapter
            // 
            this.FacturaTableAdapter.ClearBeforeFill = true;
            // 
            // ClienteFacturaBindingSource
            // 
            this.ClienteFacturaBindingSource.DataMember = "ClienteFactura";
            this.ClienteFacturaBindingSource.DataSource = this.FerreteriaDataSet1;
            // 
            // ClienteFacturaTableAdapter
            // 
            this.ClienteFacturaTableAdapter.ClearBeforeFill = true;
            // 
            // FormFacturasVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormFacturasVentas";
            this.Text = "FormFacturasVentas";
            this.Load += new System.EventHandler(this.FormFacturasVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FerreteriaDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FacturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClienteFacturaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource FacturaBindingSource;
        private FerreteriaDataSet1 FerreteriaDataSet1;
        private System.Windows.Forms.BindingSource ClienteFacturaBindingSource;
        private FerreteriaDataSet1TableAdapters.FacturaTableAdapter FacturaTableAdapter;
        private FerreteriaDataSet1TableAdapters.ClienteFacturaTableAdapter ClienteFacturaTableAdapter;
    }
}