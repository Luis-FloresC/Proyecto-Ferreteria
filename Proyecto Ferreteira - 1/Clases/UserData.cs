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
        /// Propiedades 
        /// </summary>
        public int Id { get; set; }
        public string DNI { get; set; }

        public string Nombre { get; set; }

        public string Cargo { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int Edad { get; set; }

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
            try
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
                                CacheUsuario.DNI = reader.GetString(8);
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
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        /// <summary>
        /// Funcion para comprobar el estado del Empleado
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns>devuelve un estado logico</returns>
        public bool EstadoEmpleado(int codigo)
        {

            try
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
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        /// <summary>
        /// Funcion para Cargar el DatagridView con la infomrmacion de Empleados
        /// </summary>
        /// <returns>Devuelve el dataTable lleno con todos los datos de los empleados</returns>
        public DataTable DataTableEmpleado()
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (var conexion = GetConnection())
                {
                    string query = @"select
                               E.Codigo_Empleado [Id],
                               E.identidad [Identidad],
                               concat(E.Nombre_Empleado,' ',E.Apellido_empleado)[Nombre Completo],
                               E.Telefono,
                               E.Correo,
                               Datediff(YEAR,E.Fecha_Nacimiento,Getdate()) [Edad]
                               from [Recursos_humanos].[Empleado] E
                               join [Recursos_humanos].[Puesto] P  on  E.Codigo_Puesto = P.Codigo_Puesto
                               where E.Estado = 1";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        /// <summary>
        /// Metodo para obtener los proveedores de la base de datos
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerProveedores()
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (var conexion = GetConnection())
                {
                    string query = @"SELECT [Codigo_Proveedor] [#]
                                    ,[Nombre_Proveedor] [Nombre Completo]
                                    ,[Telefono] 
                                    ,[Direccion]
                                    ,[Correo] [Correo Electronico],
									case 
									when Estado = 1 then 'Activo'
									else 'Inactivo' 
									end [Estado]
                                    FROM [Ferreteria].[Compras].[Proveedor]";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Metodo para Buscar un Empleado en la base de datos
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public DataTable BuscarEmpleado(string nombre)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (var conexion = GetConnection())
                {
                    string query = @"select
                               E.Codigo_Empleado [Id],
                               E.identidad [Identidad],
                               concat(E.Nombre_Empleado,' ',E.Apellido_empleado)[Nombre Completo],
                               E.Telefono,
                               E.Correo,
                               Datediff(YEAR,E.Fecha_Nacimiento,Getdate()) [Edad]
                               from [Recursos_humanos].[Empleado] E
                               join [Recursos_humanos].[Puesto] P  on  E.Codigo_Puesto = P.Codigo_Puesto
                               where E.Estado = 1
                               and E.Nombre_Empleado like CONCAT('%',@nombre,'%')";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Metodo para Buscar un Proveedor en la base de datos
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public DataTable BuscarProveedores(string nombre)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (var conexion = GetConnection())
                {
                    string query = @"SELECT [Codigo_Proveedor] [#]
                                    ,[Nombre_Proveedor] [Nombre Completo]
                                    ,[Telefono] 
                                    ,[Direccion]
                                    ,[Correo] [Correo Electronico]
                                    FROM [Ferreteria].[Compras].[Proveedor]
                                    where Nombre_Proveedor like CONCAT('%',@nombre,'%')";

                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Metodo para insertar un nuevo proveedor en la base de datos
        /// </summary>
        /// <returns></returns>
        public string RegistrarProveedores(string nombre,string telefono,string direccion,string correo)
        {
            try
            {
                using (var CN = GetConnection())
                {
                    CN.Open();
                    using (var CMD = new SqlCommand())
                    {
                        CMD.Connection = CN;
                        CMD.CommandText = "ManteniemtoProveedores";
                        CMD.Parameters.AddWithValue("@nombre", nombre);
                        CMD.Parameters.AddWithValue("@codigo", 0);
                        CMD.Parameters.AddWithValue("@correo", correo);
                        CMD.Parameters.AddWithValue("@telefono", telefono);
                        CMD.Parameters.AddWithValue("@direccion", direccion);
                        CMD.Parameters.AddWithValue("@estado", true);
                        CMD.Parameters.AddWithValue("@accion", "G");
                        CMD.Parameters.Add("@mensaje", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                        CMD.CommandType = CommandType.StoredProcedure;
                        CMD.ExecuteNonQuery();
                        return CMD.Parameters["@mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Metodo para modiifcar Datos en la base de datos
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="telefono"></param>
        /// <param name="direccion"></param>
        /// <param name="correo"></param>
        /// <returns></returns>
        public string ModificarProveedores(int codigo,string nombre, string telefono, string direccion, string correo,bool estado)
        {
            try
            {
                using (var CN = GetConnection())
                {
                    CN.Open();
                    using (var CMD = new SqlCommand())
                    {
                        CMD.Connection = CN;
                        CMD.CommandText = "ManteniemtoProveedores";
                        CMD.Parameters.AddWithValue("@nombre", nombre);
                        CMD.Parameters.AddWithValue("@codigo", codigo);
                        CMD.Parameters.AddWithValue("@correo", correo);
                        CMD.Parameters.AddWithValue("@telefono", telefono);
                        CMD.Parameters.AddWithValue("@direccion", direccion);
                        CMD.Parameters.AddWithValue("@estado", estado);
                        CMD.Parameters.AddWithValue("@accion", "M");
                        CMD.Parameters.Add("@mensaje", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                        CMD.CommandType = CommandType.StoredProcedure;
                        CMD.ExecuteNonQuery();
                        return CMD.Parameters["@mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Metodo para eliminar un Proveedor de la base de datos
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string EliminarProveedor(int codigo,bool estado)
        {
            try
            {
                using (var CN = GetConnection())
                {
                    CN.Open();
                    using (var CMD = new SqlCommand())
                    {
                        CMD.Connection = CN;
                        CMD.CommandText = "ManteniemtoProveedores";
                        CMD.Parameters.AddWithValue("@nombre", "");
                        CMD.Parameters.AddWithValue("@correo", "");
                        CMD.Parameters.AddWithValue("@telefono", "");
                        CMD.Parameters.AddWithValue("@direccion", "");
                        CMD.Parameters.AddWithValue("@estado", estado);
                        CMD.Parameters.AddWithValue("@accion", "E");
                        CMD.Parameters.AddWithValue("@codigo", codigo);
                       
                        CMD.Parameters.Add("@mensaje", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                        CMD.CommandType = CommandType.StoredProcedure;
                        CMD.ExecuteNonQuery();
                        return CMD.Parameters["@mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }


        /// <summary>
        /// Funcion para Registrar un Nuevo empleado en la base de datos
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="codigoCargo"></param>
        /// <param name="telefono"></param>
        /// <param name="correo"></param>
        /// <param name="direccion"></param>
        /// <param name="estado"></param>
        /// <param name="fechaNac"></param>
        /// <param name="DNI"></param>
        /// <returns></returns>
        public string RegistrarEmpleados(string nombre, string apellido, int codigoCargo,string telefono,string correo,string direccion,bool estado,DateTime fechaNac,string DNI)
        {
            try
            {
                using (var CN = GetConnection())
                {
                    CN.Open();
                    using (var CMD = new SqlCommand())
                    {
                        CMD.Connection = CN;
                        CMD.CommandText = "RegistrarEmpleado";
                        CMD.Parameters.AddWithValue("@nombre", nombre);
                        CMD.Parameters.AddWithValue("@DNI", DNI);
                        CMD.Parameters.AddWithValue("@apellido", apellido);
                        CMD.Parameters.AddWithValue("@codigoPuesto", codigoCargo);
                        CMD.Parameters.AddWithValue("@correo", correo);
                        CMD.Parameters.AddWithValue("@telefono", telefono);
                        CMD.Parameters.AddWithValue("@fechaNacimiento", fechaNac);
                        CMD.Parameters.AddWithValue("@estado", estado);
                        CMD.Parameters.AddWithValue("@direccion", direccion);
                        CMD.Parameters.Add("@mensaje", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                        CMD.CommandType = CommandType.StoredProcedure;
                        CMD.ExecuteNonQuery();
                        return CMD.Parameters["@mensaje"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
           
        }

        /// <summary>
        /// Funcion para guardar un nuevo Usuario en la base de Datos
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contraseña"></param>
        /// <param name="codigoEmpleado"></param>
        /// <returns></returns>
        public string RegistrarUsuario(string nombreUsuario, string contraseña, int codigoEmpleado)
        {
            try
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
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
  
        }

        /// <summary>
        /// Funcion para Desactivar un Usuario
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string DesactivarUsuario(bool estado,int codigo)
        {
            string resutaldo = "";

            try
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

                        if (estado)
                        {
                            resutaldo = "La cuenta del empleado esta activa";
                        }
                        else
                        {
                            resutaldo = "La Cuenta del Empleado Esta Inactiva";
                        }

                        return resutaldo;
                    }
                }
            }
            catch ( Exception ex)
            {

                return ex.Message.ToString();
            }
           
        }

        /// <summary>
        /// Funcion para editar los datos personales del Usuario que actualmente incio sesion
        /// </summary>
        /// <param name="nombreEmpleado"></param>
        /// <param name="apellidoEmpleado"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="contraseña"></param>
        /// <param name="correo"></param>
        /// <param name="DNI"></param>
        /// <returns></returns>
        public string EditarDatosPerfil(string nombreEmpleado, string apellidoEmpleado, string nombreUsuario, string contraseña, string correo,string DNI)
        {
            try
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
                        CMD.Parameters.AddWithValue("@DNI", DNI);
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
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
          
        }

        /// <summary>
        /// Metodo para Actualizar Datos del empleado
        /// </summary>
        /// <param name="nombreEmpleado"></param>
        /// <param name="apellidoEmpleado"></param>
        /// <param name="dni"></param>
        /// <param name="email"></param>
        /// <param name="direccion"></param>
        /// <param name="fechaNacimiento"></param>
        /// <param name="codigoPuesto"></param>
        /// <param name="telefono"></param>
        /// <returns>Devuelve un mensaje de verificacion</returns>
        public string EditarDatosEmpleados(string nombreEmpleado, string apellidoEmpleado, string dni, string email, string direccion, DateTime fechaNacimiento,int codigoPuesto,string telefono)
        {
            try
            {


                using (var CN = GetConnection())
                {
                    CN.Open();
                    using (var CMD = new SqlCommand())
                    {
                        CMD.Connection = CN;
                        CMD.CommandText = "EditarEmpleado";
                        CMD.Parameters.AddWithValue("@codigo", CacheEmpleado.IdEmpleado);
                        CMD.Parameters.AddWithValue("@nombre", nombreEmpleado);
                        CMD.Parameters.AddWithValue("@apellido", apellidoEmpleado);
                        CMD.Parameters.AddWithValue("@correo", email);
                        CMD.Parameters.AddWithValue("@dni", dni);
                        CMD.Parameters.AddWithValue("@fecha_nac", fechaNacimiento);
                        CMD.Parameters.AddWithValue("@puesto", codigoPuesto);
                        CMD.Parameters.AddWithValue("@direccion", direccion);
                        CMD.Parameters.AddWithValue("@telefono", telefono);
                        CMD.Parameters.Add("@mensaje", SqlDbType.NVarChar, 150).Direction = ParameterDirection.Output;
                        CMD.CommandType = CommandType.StoredProcedure;
                        CMD.ExecuteNonQuery();
                        return CMD.Parameters["@mensaje"].Value.ToString();
                    }
                }


            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }

            
        }

        /// <summary>
        /// Metodo para llenar lista con los Cargos disponibles
        /// </summary>
        /// <returns></returns>
        public List<UserData> ComboBoxCargo()
        {
            List<UserData> allCargos = new List<UserData>();
            var connection = GetConnection();
            try
            {
                string query = @"SELECT [Codigo_Puesto],[Descripcion] FROM [Recursos_humanos].[Puesto]";

                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        allCargos.Add(new UserData
                        {
                            Id = reader.GetInt32(0),
                            Cargo = reader.GetString(1)
                        });
                    }

                }
                return allCargos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            { connection.Close(); }

        }

        /// <summary>
        /// Metodo para Buscar un Empleado en Especifico
        /// </summary>
        /// <param name="codigoEmpleado"></param>
        /// <returns> devuelve un estado logico indicando si se encontro el empleado</returns>
        public bool BuscarEmpleado(int codigoEmpleado)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "BuscarEmpleado";
                    comando.Parameters.AddWithValue("@codigo", codigoEmpleado);
                 
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = comando.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            CacheEmpleado.IdEmpleado = reader.GetInt32(0);
                            CacheEmpleado.DNI = reader.GetString(1);
                            CacheEmpleado.NombreEmpleado = reader.GetString(2);
                            CacheEmpleado.ApellidoEmpleado = reader.GetString(3);
                            CacheEmpleado.Telefono = reader.GetString(4);
                            CacheEmpleado.Email = reader.GetString(5);
                            CacheEmpleado.FechaNacimiento = reader.GetDateTime(6);
                            CacheEmpleado.Cargo = reader.GetString(7);
                            CacheEmpleado.Direccion = reader.GetString(8);

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


        public int BuscarClienteAnonimo()
        {
            int codigo = 0;
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = @"SELECT [codigo_cliente] FROM[Ferreteria].[Ventas].[Cliente] where identidad = '-'";
                    

                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader = comando.ExecuteReader();
                  
                        while (reader.Read())
                        {
                            codigo = reader.GetInt32(0);
                        }
                        return codigo;                  
                }
            }
        }


        public decimal BuscarPrecioProducto(int cod)
        {
            decimal precio = 0;
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = @"select cast(Precio_Estandar as decimal(10,2))[Precio] from [Productos].[Producto] where Codigo_Producto = @cod";
                    comando.Parameters.AddWithValue("@cod", cod);

                    comando.CommandType = CommandType.Text;
                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        precio = reader.GetDecimal(0);
                    }
                    return precio;
                }
            }
        }

        /// <summary>
        /// Lista para obtener el nombre de los empleaodos
        /// </summary>
        /// <returns></returns>
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
                        empleados.Add(new UserData { Id = rdr.GetInt32(0), Nombre = rdr.GetString(1) });
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
