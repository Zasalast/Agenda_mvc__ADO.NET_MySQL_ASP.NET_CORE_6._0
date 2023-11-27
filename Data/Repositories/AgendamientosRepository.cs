using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using System.Data;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Repositories
{
    public class AgendamientoRepository
    {
        private readonly string _connectionString;

        public AgendamientoRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Agendamiento> GetAgendamientos()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var agendamientos = new List<Agendamiento>();

                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Agendamientos", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var agendamiento = new Agendamiento
                            {
                                IdAgendamiento = reader.GetInt32("Id"),
                                Fecha = reader.GetDateTime("Fecha"),
                                Hora = reader.GetTimeSpan("Hora"),
                                IdCliente = reader.GetInt32("IdCliente"),
                                IdReserva = reader.IsDBNull("IdReserva") ? null : (int?)reader.GetInt32("IdReserva"),
                                IdCancelacion = reader.IsDBNull("IdCancelacion") ? null : (int?)reader.GetInt32("IdCancelacion")
                            };

                            agendamientos.Add(agendamiento);
                        }
                    }
                }

                return agendamientos;
            }
        }

        public Agendamiento GetAgendamientoById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var agendamiento = new Agendamiento();

                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM Agendamientos WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            agendamiento.IdAgendamiento = reader.GetInt32("Id");
                            agendamiento.Fecha = reader.GetDateTime("Fecha");
                            agendamiento.Hora = reader.GetTimeSpan("Hora");
                            agendamiento.IdCliente = reader.GetInt32("IdCliente");
                            agendamiento.IdReserva = reader.IsDBNull("IdReserva") ? null : (int?)reader.GetInt32("IdReserva");
                            agendamiento.IdCancelacion = reader.IsDBNull("IdCancelacion") ? null : (int?)reader.GetInt32("IdCancelacion");
                        }
                    }
                }

                return agendamiento;
            }
        }

        public void CreateAgendamiento(Agendamiento agendamiento)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("INSERT INTO Agendamientos (Fecha, Hora, IdCliente) VALUES (@Fecha, @Hora, @IdCliente)", connection))
                {
                    command.Parameters.AddWithValue("@Fecha", agendamiento.Fecha);
                    command.Parameters.AddWithValue("@Hora", agendamiento.Hora);
                    command.Parameters.AddWithValue("@IdCliente", agendamiento.IdCliente);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAgendamiento(Agendamiento agendamiento)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("UPDATE Agendamientos SET Fecha = @Fecha, Hora = @Hora, IdCliente = @IdCliente, IdReserva = @IdReserva, IdCancelacion = @IdCancelacion WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Fecha", agendamiento.Fecha);
                    command.Parameters.AddWithValue("@Hora", agendamiento.Hora);
                    command.Parameters.AddWithValue("@IdCliente", agendamiento.IdCliente);
                    command.Parameters.AddWithValue("@IdReserva", agendamiento.IdReserva != null ? (object)agendamiento.IdReserva : DBNull.Value);
                    command.Parameters.AddWithValue("@IdCancelacion", agendamiento.IdCancelacion != null ? (object)agendamiento.IdCancelacion : DBNull.Value);
                    command.Parameters.AddWithValue("@Id", agendamiento.IdAgendamiento);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteAgendamiento(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM Agendamientos WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}