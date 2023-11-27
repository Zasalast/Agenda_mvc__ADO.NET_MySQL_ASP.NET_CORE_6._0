using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{

    public class ReservasController : Controller
    {

        private readonly ConexionBD _conexionBD;
        private readonly ReservasRepository _reservasRepository;

        public ReservasController(ConexionBD conexionBD, ReservasRepository reservasRepository)
        {
            _conexionBD = conexionBD;
            _reservasRepository = reservasRepository;
        }

        // GET: ReservasController
        public ActionResult Index()
        {
            var reservas = _reservasRepository.GetAllReservas;

            return View(reservas);
        }

        // GET: ReservasController/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: ReservasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(Agendamiento reserva)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = _reservasRepository.CreateReserva(reserva);

                    TempData["Mensaje"] = "Reserva creada exitosamente";

                    return RedirectToAction("Detalle", new { id });
                }

                return View(reserva);

            }
            catch (Exception ex)
            {
                // Log error
                return View(reserva);
            }
        }

        // GET: ReservasController/Delete/5  
        public ActionResult Eliminar(int id)
        {
            _reservasRepository.DeleteReserva(id);

            TempData["Mensaje"] = "Reserva eliminada";

            return RedirectToAction("Index");
        }

    }
}