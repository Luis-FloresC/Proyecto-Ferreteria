using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows;

namespace Proyecto_Ferreteira___1.Clases
{
    class ClsCliente: Connection
    {
        //Atributos
        private int codigo;
        private string nombres;
        private string apellidos;
        private string identidad;
        private DateTime fechaNacimiento;
        private string telefono;
        private string rtn;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Identidad { get => identidad; set => identidad = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Rtn { get => rtn; set => rtn = value; }

        public void AgregarCliente()
        {
            //Variable miembro para obtener la conexión
            var conexion = GetConnection();

            try
            {
                //Query a ejecutar en la base de datos
                string query = "EXEC [dbo].[IngresarCliente] @nombres, @apellidos, @identidad, @fechaNacimiento, @telefono, @rtn";

                //Establece la conexión
                conexion.Open();

                //Crea el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                //Remplazar los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@nombres", nombres);
                sqlCommand.Parameters.AddWithValue("@apellidos", apellidos);
                sqlCommand.Parameters.AddWithValue("@identidad", identidad);
                sqlCommand.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
                sqlCommand.Parameters.AddWithValue("@telefono", telefono);
                sqlCommand.Parameters.AddWithValue("@rtn", rtn);

                //Ejecutar el comando de insercion
                sqlCommand.ExecuteNonQuery();

                //Mensaje de éxito
                MessageBox.Show("Cliente agregado con exito");
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

        public void consultarCliente(int codigoCliente)
        {
            //Variable miembro para obtener la conexión
            var conexion = GetConnection();

            try
            {
                // Query de búsqueda
                string query = @"Select * From [Ventas].[Cliente]
                                 Where codigo_cliente = @codigo";

                // Establecer la conexión
                conexion.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                // Establecer el valor del parámetro
                sqlCommand.Parameters.AddWithValue("@codigo", codigoCliente);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        codigo = Convert.ToInt32(rdr["codigo_cliente"]);
                        nombres = Convert.ToString(rdr["nombres"]);
                        apellidos = Convert.ToString(rdr["apellidos"]);
                        identidad = Convert.ToString(rdr["identidad"]);
                        fechaNacimiento = Convert.ToDateTime(rdr["fecha_nacimiento"]);
                        telefono = Convert.ToString(rdr["telefono"]);
                        rtn = Convert.ToString(rdr["rtn"]);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                // Cerrar la conexión
                conexion.Close();
            }
        }

        public void editarCliente()
        {
            //Variable miembro para obtener la conexión
            var conexion = GetConnection();

            try
            {
                //Query de actualización
                string query = @"EXEC [dbo].[ModificarCliente] @codigo, @nombres, @apellidos, @identidad, @fechaNacionalidad, @telefono, @rtn";

                //Establece la conexión con la base de datos
                conexion.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@codigo", codigo);
                sqlCommand.Parameters.AddWithValue("@nombres", nombres);
                sqlCommand.Parameters.AddWithValue("@apellidos", apellidos);
                sqlCommand.Parameters.AddWithValue("@identidad", identidad);
                sqlCommand.Parameters.AddWithValue("@fechaNacionalidad", fechaNacimiento);
                sqlCommand.Parameters.AddWithValue("@telefono", telefono);
                sqlCommand.Parameters.AddWithValue("@rtn", rtn);

                //Ejecutar el comando sql
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                //Cerrar la conexión con la base de datos
                conexion.Close();
            }
        }

        public void eliminarCliente(int codigoCliente)
        {
            //Variable miembro para obtener la conexión
            var conexion = GetConnection();

            try
            {
                //Query de actualización
                string query = @"UPDATE [Ventas].[Cliente] SET [estado] = 0
                                 WHERE codigo_cliente = @codigo";

                //Establece la conexión con la base de datos
                conexion.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@codigo", codigoCliente);

                //Ejecutar el comando sql
                sqlCommand.ExecuteNonQuery();

                //Mensaje de confirmacion 
                MessageBox.Show("El cliente a sido eliminado");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                //Cerrar la conexión con la base de datos
                conexion.Close();
            }
        }
    }
}
