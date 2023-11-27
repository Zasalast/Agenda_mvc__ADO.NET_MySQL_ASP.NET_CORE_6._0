using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories
{
    public class ReservasRepository
    {
        private readonly string _connectionString;

        public ReservasRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public int CreateReserva(Agendamiento reserva)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = "INSERT INTO agendamientos (IdCliente, Fecha, Hora, IdAgenda) " +
                             "VALUES (@IdCliente, @Fecha, @Hora, @IdAgenda);";

                conn.Open();

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", reserva.IdCliente);
                    cmd.Parameters.AddWithValue("@Fecha", reserva.Fecha);
                    cmd.Parameters.AddWithValue("@Hora", reserva.Hora);
                    cmd.Parameters.AddWithValue("@IdAgenda", reserva.IdAgenda);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DeleteReserva(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = "DELETE FROM agendamientos WHERE IdAgendamiento = @IdAgendamiento;";

                conn.Open();

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdAgendamiento", id);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public Agendamiento GetReserva(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM agendamientos WHERE IdAgendamiento = @IdAgendamiento;";

                conn.Open();

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdAgendamiento", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var reserva = new Agendamiento()
                            {
                                IdAgendamiento = Convert.ToInt32(reader["IdAgendamiento"]),
                                IdCliente = reader["IdCliente"] != DBNull.Value ? Convert.ToInt32(reader["IdCliente"]) : (int?)null,
                                Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : (DateTime?)null,
                                Hora = reader["Hora"] != DBNull.Value ? TimeSpan.Parse(reader["Hora"].ToString()) : (TimeSpan?)null,
                                Estado = reader["Estado"] != DBNull.Value ? Convert.ToChar(reader["Estado"]) : (char?)null,
                                IdAgenda = reader["IdAgenda"] != DBNull.Value ? Convert.ToInt32(reader["IdAgenda"]) : (int?)null
                            };

                            return reserva;
                        }
                    }
                }
            }

            return null;
        }

        public List<Agendamiento> GetAllReservas()
        {
            var reservas = new List<Agendamiento>();

            using (var conn = new MySqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM agendamientos;";

                conn.Open();

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var reserva = new Agendamiento()
                            {
                                IdAgendamiento = Convert.ToInt32(reader["IdAgendamiento"]),
                                IdCliente = reader["IdCliente"] != DBNull.Value ? Convert.ToInt32(reader["IdCliente"]) : (int?)null,
                                Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : (DateTime?)null,
                                Hora = reader["Hora"] != DBNull.Value ? TimeSpan.Parse(reader["Hora"].ToString()) : (TimeSpan?)null,
                                Estado = reader["Estado"] != DBNull.Value ? Convert.ToChar(reader["Estado"]) : (char?)null,
                                IdAgenda = reader["IdAgenda"] != DBNull.Value ? Convert.ToInt32(reader["IdAgenda"]) : (int?)null
                            };

                            reservas.Add(reserva);
                        }
                    }
                }
            }

            return reservas;
        }
    }
}