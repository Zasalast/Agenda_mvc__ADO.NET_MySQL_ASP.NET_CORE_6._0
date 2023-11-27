using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories
{
    public class PermisosRepository
    {
        private readonly string _connectionString;

        public PermisosRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Permiso> GetPermisos()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM permisos";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    List<Permiso> permisos = new List<Permiso>();

                    while (reader.Read())
                    {
                        Permiso permiso = MapperPermiso(reader);
                        permisos.Add(permiso);
                    }

                    conn.Close();

                    return permisos;
                }
            }
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
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idPermiso", id);

                    conn.Open();

                    // Ejecutar y leer datos

                    conn.Close();
                }
            }

            GetPermisoRolesById(permiso);

            return permiso;
        }

        public void DeletePermiso(int id)
        {
            DeletePermisoRoles(id);
        }

        public void CreatePermiso(Permiso permiso)
        {
            InsertPermisoRoles(permiso);
        }

        public void UpdatePermiso(Permiso permiso)
        {
            DeletePermisoRoles(permiso.IdPermiso);
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
            Permiso permiso = new Permiso();
            permiso.IdPermiso = reader.GetInt32("id_permiso");
            permiso.Nombre = reader.GetString("nombre");

            return permiso;
        }
    }
}
