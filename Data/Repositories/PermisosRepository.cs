using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Humanizer;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Logging;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers;
namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories
{
    public class PermisosRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<PermisosController> _logger;
        public PermisosRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Permiso> GetPermisos()
        {
            List<Permiso> permisos = new List<Permiso>();
            string sql = "SELECT * FROM permisos";

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Permiso permiso = MapperPermiso(reader);
                            GetPermisoRolesById(permiso);
                            permisos.Add(permiso);
                        }
                    }
                }

                conn.Close();
            }

            return permisos;
        }

        public Permiso GetPermisoById(int id)
        {
            Permiso permiso = new Permiso();
            string sql = @"SELECT p.*, rp.id_rol, r.nombre AS rol_nombre
                   FROM permisos p  
                   INNER JOIN roles_permisos rp ON rp.id_permiso = p.id_permiso
                   INNER JOIN roles r ON r.id_rol = rp.id_rol
                   WHERE p.id_permiso = @idPermiso";

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idPermiso", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            permiso = MapperPermiso(reader);
                            GetPermisoRolesById(permiso);
                        }
                    }
                }

                conn.Close();
            }

            return permiso;
        }


        // Asegúrate de que la clase Permiso tiene una propiedad Roles
        public class Permiso
        {
            // Otras propiedades de Permiso...

            public List<Rol> Roles { get; set; } = new List<Rol>();
        }

        // En el método GetPermisoRolesById
        private void GetPermisoRolesById(Permiso permiso)
        {
            string sql = "SELECT r.* FROM roles_permisos rp JOIN roles r ON r.id_rol = rp.id_rol  WHERE rp.id_permiso = @idPermiso";

            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idPermiso", permiso.Roles);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var rol = new Rol();

                            // Setear propiedades del rol
                            rol.IdRol = reader.GetInt32("id_rol");
                            rol.Nombre = reader.GetString("nombre");

                            // Agregar el rol a la lista de Roles en Permiso
                            permiso.Roles.Add(rol);
                        }
                    }
                }

                conn.Close();
            }
        }



        public void DeletePermiso(int id)
        {
            DeletePermisoRoles(id);
        }

        public void CreatePermiso(Permiso permiso)
        {
            try
            {
                InsertPermisoRoles(permiso);
            }catch(Exception ex) {_logger.LogError($"Error al crear el permiso: {ex.Message}");

            }
        }

        public void UpdatePermiso(int id,Permiso permiso)
        {
            DeletePermisoRoles(id);
            InsertPermisoRoles(permiso);
        }

        private void InsertPermisoRoles(Permiso permiso)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO roles_permisos (id_rol, id_permiso) 
                               VALUES (@idRol, @idPermiso)";

                foreach (var rolPermiso in permiso.RolesPermisos)
                {
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@idRol", rolPermiso.IdRol);
                        cmd.Parameters.AddWithValue("@idPermiso", permiso.IdPermiso);

                        conn.Open();

                        cmd.ExecuteNonQuery();

                        conn.Close();
                    }
                }
            }
        }

        private void DeletePermisoRoles(int permisoId)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = @"DELETE FROM roles_permisos  
                              WHERE id_permiso = @idPermiso";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idPermiso", permisoId);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

   

        private Permiso MapperPermiso(MySqlDataReader reader)
        {
            var permiso = new Permiso();

            permiso.IdPermiso = reader.GetInt32("id_permiso");

            while (reader.NextResult())
            {
                var rol = new Rol();

                // Mapear rol

            }

            return permiso;
        }
    }
}
