using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using System.Data;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories
{
    public class RolRepository
    {
        private readonly string _connectionString;

        public RolRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Rol> GetRol()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var roles = new List<Rol>();

                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Rol", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Rol = new Rol
                            {
                                IdRol = reader.GetInt32("IdRol"),
                                Nombre = reader.GetString("Nombre")
                            };

                            // Cargar relación de Rol
                            var permiso = _permisoRepository.GetPermisoById(Rol.IdPermiso);
                            Rol.Permiso = permiso;

                            rol.Add(Rol);
                        }
                    }
                }

                return rol;
            }
        }

        public Persona GetRolById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var rol = new Rol();

                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Rol WHERE IdRol = @IdRol", connection))
                {
                    command.Parameters.AddWithValue("@IdRol", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rol.IdRol = reader.GetInt32("IdRol");
                            rol.Nombre = reader.GetString("Nombre");
                           
                            rol.IdPermiso = reader.GetInt32("IdPermiso");

                            // Cargar relación de Rol
                            var permiso = _permisoRepository.GetRolById(rol.IdPermiso);
                            rol.Permiso = permiso;
                        }
                    }
                }

                return rol;
            }
        }

        public void CreateUsuario(Persona usuario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Rol (Nombre, IdPermiso) VALUES (@Nombre,   @IdPermiso)", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    
                    command.Parameters.AddWithValue("@IdRol", rol.IdPermiso);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUsuario(Rol rol)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, IdRol = @IdRol WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                  
                    command.Parameters.AddWithValue("@IdRol", rol.IdPermiso);
                    command.Parameters.AddWithValue("@IdPersona", rol.IdRol);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteUsuario(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Usuarios WHERE IdUsuario = @Id";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

    }
}
