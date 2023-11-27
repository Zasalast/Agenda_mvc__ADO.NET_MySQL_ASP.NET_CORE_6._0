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

        public void CreateRol(Rol rol, List<int> idPermisos)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Insertar el nuevo rol
                string insertRolQuery = "INSERT INTO roles (Nombre) VALUES (@Nombre)";
                using (var insertRolCommand = new MySqlCommand(insertRolQuery, connection))
                {
                    insertRolCommand.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    insertRolCommand.ExecuteNonQuery();

                    // Obtener el ID del rol insertado
                    rol.IdRol = (int)insertRolCommand.LastInsertedId;
                }

                // Insertar las relaciones de permisos para el rol
                string insertPermisosQuery = "INSERT INTO rolespermisos (IdRol, IdPermiso) VALUES (@IdRol, @IdPermiso)";
                using (var insertPermisosCommand = new MySqlCommand(insertPermisosQuery, connection))
                {
                    foreach (var idPermiso in idPermisos)
                    {
                        insertPermisosCommand.Parameters.Clear();
                        insertPermisosCommand.Parameters.AddWithValue("@IdRol", rol.IdRol);
                        insertPermisosCommand.Parameters.AddWithValue("@IdPermiso", idPermiso);
                        insertPermisosCommand.ExecuteNonQuery();
                    }
                }
            }
        }


        public void UpdateRol(Rol rol, List<int> idPermisos)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE roles SET Nombre = @Nombre WHERE IdRol = @IdRol", connection))
                {
                    command.Parameters.AddWithValue("@Nombre", rol.Nombre);
                    command.Parameters.AddWithValue("@IdRol", rol.IdRol);
                    command.ExecuteNonQuery();
                }

                // Actualizar las relaciones de permisos para el rol
                UpdateRolPermisos(rol, idPermisos);
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

        private void InsertarRolPermisos(int idRol, List<int> idPermisos)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO rolespermisos (IdRol, IdPermiso) VALUES (@IdRol, @IdPermiso)", connection))
                {
                    foreach (var idPermiso in idPermisos)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@IdRol", idRol);
                        command.Parameters.AddWithValue("@IdPermiso", idPermiso);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        private void UpdateRolPermisos(int idRol, IEnumerable<int> permisos)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Eliminar las relaciones existentes
                string deleteQuery = "DELETE FROM rolespermisos WHERE IdRol = @IdRol";
                using (var deleteCommand = new MySqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@IdRol", idRol);
                    deleteCommand.ExecuteNonQuery();
                }

                // Insertar las nuevas relaciones
                string insertQuery = "INSERT INTO rolespermisos (IdRol, IdPermiso) VALUES (@IdRol, @IdPermiso)";
                using (var insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    foreach (var idPermiso in permisos)
                    {
                        insertCommand.Parameters.Clear();
                        insertCommand.Parameters.AddWithValue("@IdRol", idRol);
                        insertCommand.Parameters.AddWithValue("@IdPermiso", idPermiso);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
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
