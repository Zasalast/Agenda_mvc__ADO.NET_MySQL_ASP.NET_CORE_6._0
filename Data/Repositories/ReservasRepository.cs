using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using MySql.Data.MySqlClient;

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
                //     string sql = "CALL spCreateReservation(@IdCliente, @Fecha, @Hora)";
                string sql = "INSERT INTO agendamientos ("
                              + "IdCliente, Fecha, Hora, IdAgenda) "
                              + "VALUES (@IdCliente, @Fecha, @Hora, @IdAgenda);";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", reserva.IdCliente);
                    cmd.Parameters.AddWithValue("@Fecha", reserva.Fecha);
                    cmd.Parameters.AddWithValue("@Hora", reserva.Hora);
                    cmd.Parameters.AddWithValue("@IdAgenda", reserva.IdAgenda);

                    conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public int DeleteReserva(int id)
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                //     string sql = "CALL spCreateReservation(@IdCliente, @Fecha, @Hora)";
                string sql = "DELETE FROM reservas WHERE id_reserva = 1;  ";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                                       conn.Open();

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

    }
}
