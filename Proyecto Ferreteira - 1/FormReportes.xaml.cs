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
    /// Lógica de interacción para FormReportes.xaml
    /// </summary>
    public partial class FormReportes : UserControl
    {
        public FormReportes()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Boton de Imprimir Reporte de Ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            if ((dtpFechaInicio.SelectedDate != null && dtpFechaFinal.SelectedDate != null))
            {

                if (Validaciones(dtpFechaInicio.SelectedDate.Value, dtpFechaFinal.SelectedDate.Value))
                {
                    FormReporteDeVentas ventas = new FormReporteDeVentas(dtpFechaInicio.SelectedDate.Value, dtpFechaFinal.SelectedDate.Value);
                    ventas.Show();
                }
                else
                    dtpFechaFinal.Focus();

            }
            else
            {
                MessageBox.Show("Por Favor seleccione una Fecha", "Aviso");
            }
              
          
        }

        /// <summary>
        /// Metodo para Limpiar las cajas de texto
        /// </summary>
        /// <param name="date">nombre del DataPicker</param>
        private void Limpiar(DatePicker date)
        {
            date.Text = string.Empty;
        }
        /// <summary>
        /// Boton para imprimir los reportes de Compras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImprimirCompra_Click(object sender, RoutedEventArgs e)
        {

            if((dtpFechaInicioCompra.SelectedDate != null && dtpFechaFinalCompra.SelectedDate != null))
            {
                if (Validaciones(dtpFechaInicioCompra.SelectedDate.Value, dtpFechaFinalCompra.SelectedDate.Value))
                {
                    FormReportCompras compras = new FormReportCompras(dtpFechaInicioCompra.SelectedDate.Value, dtpFechaFinalCompra.SelectedDate.Value);
                    compras.Show();
                }
                else
                    dtpFechaFinalCompra.Focus();
            }
            else
            {
                MessageBox.Show("Por Favor seleccione una Fecha","Aviso");
            }
        
           
        }

        /// <summary>
        /// Metodo para validar la fecha
        /// </summary>
        /// <param name="FechaInicio">Fecha de Inicio</param>
        /// <param name="FechaFinal">Fecha Final</param>
        /// <returns></returns>
        private bool Validaciones(DateTime FechaInicio,DateTime FechaFinal)
        {
            try
            {
                DateTime dt1 = FechaInicio;
                DateTime dt2 = FechaFinal;

                if (dt1.Date > dt2.Date)
                {

                    MessageBox.Show("Error: La Fecha Final debe ser mayor o igual a la fecha inicial","Aviso");
                    
                    return false;
                }
                else
                {

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message.ToString(),"Aviso");
                return false;
            }
          
        }

        /// <summary>
        /// Botn de Cancelar Reporte de Compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelarCompra_Click(object sender, RoutedEventArgs e)
        {
            Limpiar(dtpFechaFinalCompra);
            Limpiar(dtpFechaInicioCompra);
        }

        /// <summary>
        /// Boton de Cancelar reporte de Ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar(dtpFechaFinal);
            Limpiar(dtpFechaInicio);
        }

        /// <summary>
        /// Evento para no permitir que el usuario pueda escribir en la caja de texto para que la pueda solo seleccionar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpFechaFinal_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
