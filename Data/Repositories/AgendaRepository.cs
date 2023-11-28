using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using System.Data;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories

{

    public class AgendaRepository
    {

        private readonly string _connectionString;


        public AgendaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }


        public List<Agendamiento> GetAgendamientos(List<Agendamiento> agendamiento)
        {
             
            MySqlConnection conexion = new MySqlConnection(_connectionString);
            conexion.Open();

            // ejecutar consulta

            conexion.Close();
            return agendamiento;
        }


        private List<Agendamiento> MapToObject(MySqlDataReader reader)

        {

            // Implementar mapeo 
            // Crear lista
            // por cada registro en reader agregar a lista

            List<Agendamiento> reservas = new List<Agendamiento>();

            while (reader.Read())
            {
                Agendamiento temp = new Agendamiento();
                temp.IdAgendamiento = reader.GetInt32(0);

                reservas.Add(temp);
            }

            return reservas;

        }
        public int CreateAgenda2(Agendamiento reserva)
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

        public int DeleteAgenda2(int id)
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

        public Agendamiento GetAgenda2(int id)
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
                            var agenda = new Agendamiento()
                            {
                                IdAgendamiento = Convert.ToInt32(reader["IdAgendamiento"]),
                                IdCliente = reader["IdCliente"] != DBNull.Value ? Convert.ToInt32(reader["IdCliente"]) : (int?)null,
                                Fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]) : (DateTime?)null,
                                Hora = reader["Hora"] != DBNull.Value ? TimeSpan.Parse(reader["Hora"].ToString()) : (TimeSpan?)null,
                                Estado = reader["Estado"] != DBNull.Value ? Convert.ToChar(reader["Estado"]) : (char?)null,
                                IdAgenda = reader["IdAgenda"] != DBNull.Value ? Convert.ToInt32(reader["IdAgenda"]) : (int?)null
                            };

                            return agenda;
                        }
                    }
                }
            }

            return null;
        }

        public List<Agendamiento> GetAllAgendas2()
        {
            var agendas = new List<Agendamiento>();

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

                            agendas.Add(reserva);
                        }
                    }
                }
            }

            return agendas;
        }
        //debe ser corregido
        public int GetReservasDisponibles2()
        {
            return 1;
        }

        // public int CreateAgenda2(Agendamiento model)
        public int CreateAgenda(Agendamiento reserva)
        {
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                conn.Open();
                    //string sql = "INSERT INTO agendamientos (IdCliente, Fecha, Hora) VALUES (@IdCliente, @Fecha, @Hora)";
                    string sql = "CALL spCreateReservation(@IdCliente, @Fecha, @Hora)";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                    cmd.Parameters.AddWithValue("@IdCliente", reserva.IdCliente);
                    cmd.Parameters.AddWithValue("@Fecha", reserva.Fecha);
                    cmd.Parameters.AddWithValue("@Hora", reserva.Hora);

                    conn.Open();
                    int id = (int)cmd.ExecuteScalar();  

                    return id;
                    }

                }
            }catch (Exception ex) {
                Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                return 0;
            }
            
            }
    

    private int DeleteReserva()
        {
            int id = 0;
            return id;
        }
        private int UpdateReserva()
        {
            int id = 0;
            return id;
        }
    }

}



