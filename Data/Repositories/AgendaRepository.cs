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


       // public int CreateReserva(Agendamiento model)
        public int CreateReserva(Agendamiento reserva)
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



