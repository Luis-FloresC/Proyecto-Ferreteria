using System;
using System.Collections.Generic;

//Namespace Requeridos
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_Ferreteira___1.Clases
{
    class Compras : Connection
    {

        //Propiedades

        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Descuento { get; set; }
        public double ISV { get; set; }
        public double SubTotal { get; set; }
        private static int CodigoCompras { get; set; }
        public double Flete { get; set; }

        public static double Cambio { get; set; }
        public static double Monto { get; set; }


        //Constructores
        public Compras() { }
        public Compras(int idProveedor, string nombreProveedor)
        {
            IdProveedor = idProveedor;
            NombreProveedor = nombreProveedor;
        }

        public Compras(double cambio, double monto)
        {
            Cambio = cambio;
            Monto = monto;
        }

        public void RegistroDeCambio()
        {
            Console.WriteLine("Listo");
        }

        public Compras(int idProducto, string nombreProducto, int cantidad)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Cantidad = cantidad;
        }

        public Compras(int idProveedor, int idProducto, int cantidad, double precioUnitario, double subtotal, double isv, double descuento, int codigoCompra, double flete , double cambio, double monto)
        {
            this.IdProveedor = idProveedor;
            this.IdProducto = idProducto;
            CodigoCompras = codigoCompra;
            this.Cantidad = cantidad;
            this.Descuento = descuento;
            this.ISV = isv;
            this.Precio = precioUnitario;
            this.SubTotal = subtotal;
            this.Flete = flete;
            Cambio = cambio;
            Monto = monto;

        }
        /// <summary>
        /// Se Encarga de llenar el combo box de producto 
        /// </summary>
        /// <returns> Una lista con los productos de la base de datos </returns>
        public List<Compras> LlenarComboProductos()
        {

            List<Compras> productos = new List<Compras>();

            //Conexion
            var connection = GetConnection();
            try
            {
                //Consulta SQL
                string query = @"SELECT [Codigo_Producto],[Nombre_Producto]
                                FROM [Ferreteria].[Productos].[Producto]";


                //Creacion del comando de consulta

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        productos.Add(new Compras { IdProducto = reader.GetInt32(0), NombreProducto = reader.GetString(1) });

                    }
                }

                return productos;
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
        /// Se Encarga de llenar el combo box de proveedores
        /// </summary>
        /// <returns>Una lista con los proveedores de la base de datos</returns>
        public List<Compras> LlenarComboProveedores()
        {

            List<Compras> proveedores = new List<Compras>();

            //Conexion
            var connection = GetConnection();
            try
            {
                //Consulta SQL
                string query = @"SELECT [Codigo_Proveedor]
                                ,[Nombre_Proveedor] FROM [Compras].[Proveedor]";

                //Creacion del comando de consulta

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        proveedores.Add(new Compras { IdProveedor = reader.GetInt32(0), NombreProveedor = reader.GetString(1) });
                    }
                }

                return proveedores;
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
        /// Se encarga de la insercion de los registros de compras de la Base de Datos
        /// </summary>
        /// <returns> Un mensaje de confirmacion si se realizo correctamente </returns>
        public string GuardarCompras()
        {
            try
            {

                using (var CN = GetConnection())
                {
                    CN.Open();
                    using (var CMD = new SqlCommand())
                    {
                        CMD.Connection = CN;
                        CMD.CommandText = "RegistrarCompras";
                        CMD.Parameters.Clear();
                        CMD.Parameters.AddWithValue("@codigoProveedor", IdProveedor);
                        CMD.Parameters.AddWithValue("@codigoEmpleado", CacheUsuario.IdUsuario);
                        CMD.Parameters.AddWithValue("@codigoCompra", CodigoCompras);
                        CMD.Parameters.AddWithValue("@subTotal", SubTotal);
                        CMD.Parameters.AddWithValue("@isv", ISV);
                        CMD.Parameters.AddWithValue("@descuento", Descuento);
                        CMD.Parameters.AddWithValue("@codigoProducto", IdProducto);
                        CMD.Parameters.AddWithValue("@precionUnitario", Precio);
                        CMD.Parameters.AddWithValue("@cantidad", Cantidad);
                        CMD.Parameters.AddWithValue("@flete", Flete);
                        CMD.Parameters.AddWithValue("@cambio", Cambio);
                        CMD.Parameters.AddWithValue("@monto", Monto);
                        CMD.Parameters.Add("@mensaje", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                        CMD.CommandType = CommandType.StoredProcedure;
                        CMD.ExecuteNonQuery();
                        return CMD.Parameters["@mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Obtiene el codigo de la compra 
        /// </summary>
        /// <returns> El codigo de la compra iterando en uno cada vez </returns>
        public int CodigoCompra()
        {
            var connection = GetConnection();
            int codigoComprar = 0;
            try
            {
                //Consulta SQL
                string query = @"select COAlesce(max([Codigo_Compra]),0)[Codi] from [Compras].[Compra]
";

                //Creacion del comando de consulta

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                //Obtencion de datos y almacenamiento en las propiedades
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        codigoComprar = reader.GetInt32(0);
                    }
                }

                return codigoComprar + 1;
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


    }
}







