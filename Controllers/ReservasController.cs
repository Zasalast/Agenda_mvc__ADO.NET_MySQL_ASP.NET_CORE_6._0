using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{
    public class ReservasController : Controller
    {
      
        private readonly MySqlConnection _connection;

        public ReservasController()
        {
            _connection = new MySqlConnection(connectionString);
        }

        public IActionResult Index()
        {
            List<Agendamiento> reservas = new List<Agendamiento>();

            string query = "SELECT * FROM agendamientos";

            using (var command = new MySqlCommand(query, _connection))
            {
                _connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var reserva = new Agendamiento();
                    reserva.IdAgendamiento = (int)reader["IdAgendamiento"];
                    //leer y asignar otras propiedades

                    reservas.Add(reserva);
                }

                _connection.Close();
            }

            return View(reservas);
        }
    }
}
