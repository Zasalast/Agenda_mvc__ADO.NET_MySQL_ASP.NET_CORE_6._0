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
        private readonly PermisosRepository _permisosRepository;
        public RolRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Rol> GetRoles()
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
                            var rol = new Rol
                            {
                                IdRol = reader.GetInt32("Id"),
                                Nombre = reader.GetString("Nombre")
                            };

                            // Cargar relación de permisos para el rol
                          //  rol.RolesPermisos = _permisosRepository.GetPermisosByRolId(rol.IdRol);

                            roles.Add(rol);
                        }
                    }
                }

                return roles;
            }
        }

        public Rol GetRolById(int id)
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

                            // Cargar relación de permisos para el rol
                          //  rol.RolesPermisos = _permisosRepository.GetPermisosByRolId(rol.IdRol);
                        }
                    }
                }

                return rol;
            }
        }

        public void CreateRol(Rol rol)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Rol (Nombre) VALUES (@Nombre)", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    command.ExecuteNonQuery();

                    // Obtener el ID del rol insertado
                    rol.IdRol = (int)command.LastInsertedId;
                }

                // Insertar las relaciones de permisos para el rol
                InsertRolPermisos(rol);
            }
        }

        public void UpdateRol(Rol rol)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE Rol SET Nombre = @Nombre WHERE IdRol = @IdRol", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    command.Parameters.AddWithValue("@IdRol", rol.IdRol);
                    command.ExecuteNonQuery();
                }

                // Actualizar las relaciones de permisos para el rol
                UpdateRolPermisos(rol);
            }
        }

        public void DeleteRol(int IdRol)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Eliminar las relaciones de permisos para el rol
                DeleteRolPermisos(IdRol);

                using (var command = new MySqlCommand("DELETE FROM Rol WHERE IdRol = @IdRol", connection))
                {
                    command.Parameters.AddWithValue("@IdRol", IdRol);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void InsertRolPermisos(Rol rol)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO RolPermiso (IdRol, IdPermiso) VALUES (@IdRol, @IdPermiso)", connection))
                {
                    foreach (var permiso in rol.RolesPermisos)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@IdRol", rol.IdRol);
                        command.Parameters.AddWithValue("@IdPermiso", permiso.IdPermiso);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void UpdateRolPermisos(Rol rol)
        {
            // Eliminar las relaciones existentes
            DeleteRolPermisos(rol.IdRol);

            // Insertar las nuevas relaciones
            InsertRolPermisos(rol);
        }

        private void DeleteRolPermisos(int rolId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM RolPermiso WHERE IdRol = @IdRol", connection))
                {
                    command.Parameters.AddWithValue("@IdRol", rolId);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
