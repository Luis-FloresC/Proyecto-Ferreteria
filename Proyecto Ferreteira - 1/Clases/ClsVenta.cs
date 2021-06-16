using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace Proyecto_Ferreteira___1.Clases
{
    class ClsVenta : Connection
    {
        //Atributos 
        private int codigoCliente;
        private int codigoEmpleado = CacheUsuario.IdUsuario;
        private int codigoProducto;
        private int cantidadProducto;
        private double precioProducto;
        private double isv;
        private double descuento;
        private double subtotal;
        private string tipoPago;

        //Propiedades
        public int CodigoCliente { get => codigoCliente; set => codigoCliente = value; }
        public int CodigoEmpleado { get => codigoEmpleado; set => codigoEmpleado = value; }
        public int CodigoProducto { get => codigoProducto; set => codigoProducto = value; }
        public int CantidadProducto { get => cantidadProducto; set => cantidadProducto = value; }
        public double PrecioProducto { get => precioProducto; set => precioProducto = value; }
        public double ISV { get => isv; set => isv = value; }
        public double Descuento { get => descuento; set => descuento = value; }
        public double Subtotal { get => subtotal; set => subtotal = value; }
        public string TipoPago { get => tipoPago; set => tipoPago = value; }

        //Metodos
        /// <summary>
        /// Es el metodo encargado de ingresar la venta a la base de datos
        /// </summary>
        public void facturar()
        {
            var conexion = GetConnection();

            try
            {
                //Query a ejecutar en la base de datos
                string query = "EXEC [dbo].[Facturar] @codigoEmpleado, @codigoCliente, @tipoPago, @subtotal, @isv, @descuento";

                //Establece la conexión
                conexion.Open();

                //Crea el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                //Remplazar los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@codigoEmpleado", CodigoEmpleado);
                sqlCommand.Parameters.AddWithValue("@codigoCliente", CodigoCliente);
                sqlCommand.Parameters.AddWithValue("@tipoPago", TipoPago);
                sqlCommand.Parameters.AddWithValue("@subtotal", Subtotal);
                sqlCommand.Parameters.AddWithValue("@isv", isv);
                sqlCommand.Parameters.AddWithValue("@descuento", Descuento);

                //Ejecutar el comando de insercion
                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                //Mensaje de error
                MessageBox.Show(e.ToString());
            }
            finally
            {
                //Cerrar la conexión
                conexion.Close();
            }
        }


        public int CodigoVenta()
        {
            var connection = GetConnection();
            int codigoVenta = 0;
            try
            {
                //Consulta SQL
                string query = @"select COAlesce(max([codigo_venta]),0)[Codi] from [Ventas].[venta]";

                //Creacion del comando de consulta

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        codigoVenta = reader.GetInt32(0);
                    }
                }

                return codigoVenta;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                connection.Close();
            }
        }


        /// <summary>
        /// Metodo encargado de agregar el detalle de la factura proviamente ingresada
        /// </summary>
        public void agregarDetalle()
        {
            var conexion = GetConnection();

            try
            {
                //Query a ejecutar en la base de datos
                string query = "EXEC [dbo].[AgregarDetalle] @codigoProducto, @precio, @cantidad";

                //Establece la conexión
                conexion.Open();

                //Crea el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                //Remplazar los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@codigoProducto", CodigoProducto);
                sqlCommand.Parameters.AddWithValue("@precio", PrecioProducto);
                sqlCommand.Parameters.AddWithValue("@cantidad", CantidadProducto);

                //Ejecutar el comando de insercion
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //Mensaje de error
                MessageBox.Show(e.ToString());
            }
            finally
            {
                //Cerrar la conexión
                conexion.Close();
            }
        }
    }
}
