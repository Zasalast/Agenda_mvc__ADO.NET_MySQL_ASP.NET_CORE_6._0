using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories
{
    public class CancelacionRepository
    {
        private readonly string connectionString;

        public CancelacionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Cancelacion> GetAllCancelaciones()
        {
            List<Cancelacion> cancelaciones = new List<Cancelacion>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM cancelaciones";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cancelacion cancelacion = new Cancelacion
                        {
                            IdCancelacion = Convert.ToInt32(reader["IdCancelacion"]),
                            FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                            Motivo = reader["Motivo"].ToString(),
                            IdAgendamiento = Convert.ToInt32(reader["IdAgendamiento"])
                        };

                        cancelaciones.Add(cancelacion);
                    }
                }
            }

            return cancelaciones;
        }

        public Cancelacion GetCancelacionById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM cancelaciones WHERE IdCancelacion = @Id";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Cancelacion cancelacion = new Cancelacion
                        {
                            IdCancelacion = Convert.ToInt32(reader["IdCancelacion"]),
                            FechaHora = Convert.ToDateTime(reader["FechaHora"]),
                            Motivo = reader["Motivo"].ToString(),
                            IdAgendamiento = Convert.ToInt32(reader["IdAgendamiento"])
                        };

                        return cancelacion;
                    }
                }
            }

            return null;
        }

        public void CreateCancelacion(Cancelacion cancelacion)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO cancelaciones (FechaHora, Motivo, IdAgendamiento) VALUES (@FechaHora, @Motivo, @IdAgendamiento)";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@FechaHora", cancelacion.FechaHora);
                command.Parameters.AddWithValue("@Motivo", cancelacion.Motivo);
                command.Parameters.AddWithValue("@IdAgendamiento", cancelacion.IdAgendamiento);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateCancelacion(Cancelacion cancelacion)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE cancelaciones SET FechaHora = @FechaHora, Motivo = @Motivo, IdAgendamiento = @IdAgendamiento WHERE IdCancelacion = @Id";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdAgendamiento", cancelacion.IdAgendamiento);
                command.Parameters.AddWithValue("@FechaHora", cancelacion.FechaHora);
                command.Parameters.AddWithValue("@Motivo", cancelacion.Motivo);
                command.Parameters.AddWithValue("@Id", cancelacion.IdCancelacion);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteCancelacion(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM cancelaciones WHERE IdCancelacion = @Id";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
