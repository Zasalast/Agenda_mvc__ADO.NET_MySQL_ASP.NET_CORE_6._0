using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories
{
    public class AgendamientosRepository
    {


        public AgendamientosRepository( )
        {
 

        }


        public List<Agendamiento> GetAgendamientos()
        {
            List<Agendamiento> agendamientos = new List<Agendamiento>();

            MySqlConnection conexion = new MySqlConnection();

            string sql = "SELECT * FROM agendamientos";

            using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
            {
                conexion.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Mapear registro 
                    }
                }

                conexion.Close();
            }

            return agendamientos;
        }



        public int CreateReserva(Agendamiento model)
        {
            MySqlConnection conexion = new MySqlConnection();

            string sql = "INSERT INTO agendamientos VALUES (...)";

            using (var cmd = new MySqlCommand(sql, conexion))
            {

                MapearModeloAParametros(model, cmd);

                conexion.Open();

                int id = (int)cmd.ExecuteScalar(); // id insertado

                conexion.Close();

                return id;

            }

        }

        private void MapearModeloAParametros(Agendamiento model, MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Nombre", model.Agenda);
            // ... mapear otras propiedades
        }


    }
}
