using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ConexionBD conexionBD;
        private readonly ReservasRepository _reservasRepository;
        public ReservasController(ReservasRepository reservasRepository)
        {
            _reservasRepository = reservasRepository;
        }


        public IActionResult Index()
        {
            List<Agendamiento> reservas = new List<Agendamiento>();

            string query = "SELECT * FROM agendamientos";

            using (var command = new MySqlCommand(query, conexionBD.ObtenerConexion())
            {
                
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var reserva = new Agendamiento();
                    reserva.IdAgendamiento = (int)reader["IdAgendamiento"];
                    //leer y asignar otras propiedades

                    reservas.Add(reserva);
                }

            _connectionString.Close();
            }

            return View(reservas);
        }

        // Controlador

        public IActionResult Create(Agendamiento reserva)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = _reservasRepository.CreateReserva(reserva);

                    TempData["Msg"] = "Reserva creada";

                    return RedirectToAction("Detalle", new { id });
                }
            }
            catch (Exception ex)
            {
                // Log error

                return View(reserva);
            }
        }
    // Controlador 
    public int Delete(int id)
    {
        int id = _reservasRepository.DeleteReserva(id);
        return id;
    }


}



        
     
}
