using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using MySql.Data.MySqlClient;
using System.Data;
namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories
{
    public class AsistenciaRepository
    {
        private readonly string connectionString;

        public AsistenciaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Asistencia> GetAllAsistencias()
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM asistencias";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Asistencia asistencia = new Asistencia
                        {
                            IdAsistencia = Convert.ToInt32(reader["IdAsistencia"]),
                            EstadoAsistencia = Convert.ToChar(reader["EstadoAsistencia"]),
                            IdAgendamiento = Convert.ToInt32(reader["IdAgendamiento"])
                        };

                        asistencias.Add(asistencia);
                    }
                }
            }

            return asistencias;
        }

        public Asistencia GetAsistenciaById(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM asistencias WHERE IdAsistencia = @Id";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Asistencia asistencia = new Asistencia
                        {
                            IdAsistencia = Convert.ToInt32(reader["IdAsistencia"]),
                            EstadoAsistencia = Convert.ToChar(reader["EstadoAsistencia"]),
                            IdAgendamiento = Convert.ToInt32(reader["IdAgendamiento"])
                        };

                        return asistencia;
                    }
                }
            }

            return null;
        }

        public void CreateAsistencia(Asistencia asistencia)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO asistencias (EstadoAsistencia, IdAgendamiento) VALUES (@EstadoAsistencia, @IdAgendamiento)";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@EstadoAsistencia", asistencia.EstadoAsistencia);
                command.Parameters.AddWithValue("@IdAgendamiento", asistencia.IdAgendamiento);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateAsistencia(Asistencia asistencia)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE asistencias SET EstadoAsistencia = @EstadoAsistencia, IdAgendamiento = @IdAgendamiento WHERE IdAsistencia = @Id";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdAgendamiento", asistencia.IdAgendamiento);
                command.Parameters.AddWithValue("@EstadoAsistencia", asistencia.EstadoAsistencia);
                command.Parameters.AddWithValue("@Id", asistencia.IdAsistencia);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteAsistencia(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM asistencias WHERE IdAsistencia = @Id";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
