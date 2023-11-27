using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using System.Data;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Persona> GetUsuarios()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var usuarios = new List<Persona>();

                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Persona", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Persona = new Persona
                            {
                                IdPersona = reader.GetInt32("IdPersona"),
                                Nombres = reader.GetString("Nombres"),
                                Apellidos = reader.GetString("Apellidos"),
                                IdRol = reader.GetInt32("IdRol")
                            };

                            // Cargar relación de Rol
                            var rol = _rolRepository.GetRolById(Persona.IdRol);
                            Persona.Rol = rol;

                            usuarios.Add(Persona);
                        }
                    }
                }

                return usuarios;
            }
        }

        public Persona GetUsuarioById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var usuario = new Persona();

                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Persona WHERE IdPersona = @IdPersona", connection))
                {
                    command.Parameters.AddWithValue("@IdPersona", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario.IdPersona = reader.GetInt32("IdPersona");
                            usuario.Nombres = reader.GetString("Nombre");
                            usuario.Apellidos = reader.GetString("Apellido");
                            usuario.IdRol = reader.GetInt32("IdRol");

                            // Cargar relación de Rol
                            var rol = _rolRepository.GetRolById(usuario.IdRol);
                            usuario.Rol = rol;
                        }
                    }
                }

                return usuario;
            }
        }

        public void CreateUsuario(Persona usuario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Persona (Nombres, Apellidos, IdRol) VALUES (@Nombres, @Apellidos, @IdRol)", connection))
                {
                    command.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                    command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@IdRol", usuario.IdRol);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUsuario(Persona usuario)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE Persona SET Nombres = @Nombres, Apellidos = @Apellidos, IdRol = @IdRol WHERE IdPersona = @IdPersona", connection))
                {
                    command.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                    command.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    command.Parameters.AddWithValue("@IdRol", usuario.IdRol);
                    command.Parameters.AddWithValue("@IdPersona", usuario.IdPersona);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeletePersona(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM Persona WHERE IdPersona = @Id";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdPersona", id);

                command.ExecuteNonQuery();
            }
        }

    }
}