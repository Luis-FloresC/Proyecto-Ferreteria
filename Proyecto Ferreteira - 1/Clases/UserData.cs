using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Ferreteira___1.Clases
{
    class UserData:Connection
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


    }
}
