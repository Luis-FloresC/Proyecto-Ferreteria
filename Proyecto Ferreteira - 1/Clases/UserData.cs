using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace Proyecto_Ferreteira___1.Clases
{
    public class UserData : Connection
    {
        /// <summary>
        /// Constractor para crear una instancia
        /// </summary>
        public UserData() { }

        /// <summary>
        /// Funcion para Verificar la Existencia de un Usuario en la Base de Datos
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <param name="pass">Contraseña</param>
        /// <returns></returns>
        public bool Login(string user, string pass)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "Verificar_Usuario";
                    comando.Parameters.AddWithValue("@user", user);
                    comando.Parameters.AddWithValue("@contrasenia", pass);
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            CacheUsuario.IdUsuario = reader.GetInt32(0);
                            CacheUsuario.Usuario = reader.GetString(1);
                            CacheUsuario.Contraseña = reader.GetString(2);
                            CacheUsuario.NombreCompleto = reader.GetString(3);
                            CacheUsuario.ApellidoCompleto = reader.GetString(4);
                            CacheUsuario.Cargo = reader.GetString(5);
                            CacheUsuario.Email = reader.GetString(6);
                            CacheUsuario.Estado = reader.GetBoolean(7);

                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }


        public bool EstadoEmpleado(int codigo)
        {
            bool EstadoEmpleado = false;
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "select Estado from [Recursos_humanos].[Empleado] where Codigo_Empleado = @codigo";
                    comando.Parameters.AddWithValue("@codigo", codigo);

                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        EstadoEmpleado = reader.GetBoolean(0);
                 
                    }

                    return EstadoEmpleado;

                }
            }

           


        }


        public DataTable DataTableEmpleado()
        {
            DataTable dataTable = new DataTable();
            using (var conexion = GetConnection())
            {
                string query = "select Codigo_Empleado,Nombre_Empleado from [Recursos_humanos].[Empleado]";

                SqlCommand cmd = new SqlCommand(query, conexion);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                return dataTable;
            }
        }


        public string RegistrarUsuario(string nombreUsuario, string contraseña, int codigoEmpleado)
        {
            using (var CN = GetConnection())
            {
                CN.Open();
                using (var CMD = new SqlCommand())
                {
                    CMD.Connection = CN;
                    CMD.CommandText = "RegistrarUsuario";
                    CMD.Parameters.AddWithValue("@codigo", codigoEmpleado);
                    CMD.Parameters.AddWithValue("@usuario", nombreUsuario);
                    CMD.Parameters.AddWithValue("@contraseña", contraseña);
                    CMD.Parameters.Add("@mensaje", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.ExecuteNonQuery();
                    return CMD.Parameters["@mensaje"].Value.ToString();
                }
            }
        }


        public string DesactivarUsuario(bool estado,int codigo)
        {
            using (var CN = GetConnection())
            {
                CN.Open();
                using (var CMD = new SqlCommand())
                {
                    CMD.Connection = CN;
                    CMD.CommandText = @"UPDATE [Recursos_humanos].[Empleado]
                                      SET[Estado] = @Estado
                                      WHERE Codigo_Empleado = @codigo";
                    CMD.Parameters.AddWithValue("@codigo", codigo);
                    CMD.Parameters.AddWithValue("@Estado", estado);
                  
                
                    CMD.CommandType = CommandType.Text;
                    CMD.ExecuteNonQuery();
                    return "La Cuenta de Usuario Esta Inactiva";
                }
            }
        }


        public string EditarDatosPerfil(string nombreEmpleado, string apellidoEmpleado, string nombreUsuario, string contraseña, string correo)
        {
            using (var CN = GetConnection())
            {
                CN.Open();
                using (var CMD = new SqlCommand())
                {
                    CMD.Connection = CN;
                    CMD.CommandText = "EditarPerfilUsuario";
                    CMD.Parameters.AddWithValue("@id_Empleado", CacheUsuario.IdUsuario);
                    CMD.Parameters.AddWithValue("@Nombre_Empleado", nombreEmpleado);
                    CMD.Parameters.AddWithValue("@Apellido_empleado", apellidoEmpleado);
                    CMD.Parameters.AddWithValue("@Correo", correo);
                    CMD.Parameters.AddWithValue("@Usuario", nombreUsuario);
                    CMD.Parameters.AddWithValue("@Contraseña", contraseña);
                    CMD.Parameters.AddWithValue("@Estado", CacheUsuario.Estado);
                    CMD.Parameters.Add("@Mensaje", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.ExecuteNonQuery();
                    return CMD.Parameters["@Mensaje"].Value.ToString();
                }
            }
        }

        public int Id { get; set; }

        public string NombreEmpleado { get; set; }



        public List<UserData> MostrarEmpleados()
        {
            // Inicializar una lista vacía de habitaciones
            List<UserData> empleados = new List<UserData>();
            var conexion = GetConnection();
            try
            {
                // Query de selección
                string query = @"select
                                [Codigo_Empleado] [id],
                                CONCAT([Nombre_Empleado],' ',[Apellido_empleado])[Nombre]
                                from [Recursos_humanos].[Empleado]";


                // Establecer la conexión
                conexion.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, conexion);

                // Obtener los datos de las habitaciones
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                        empleados.Add(new UserData { Id = rdr.GetInt32(0), NombreEmpleado = rdr.GetString(1) });
                }

                return empleados;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                conexion.Close();
                

            }
        }


    }
}
