using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{

    public class AgendaController : Controller
    {

        private readonly ConexionBD _conexionBD;
        private readonly AgendaRepository _agendasRepository;

        public AgendaController(ConexionBD conexionBD, AgendaRepository agendasRepository)
        {
            _conexionBD = conexionBD;
            _agendasRepository = agendasRepository;
        }

        // GET: ReservasController
        public ActionResult Index()
        {
            var reservas = _agendasRepository.GetAllAgendas2;

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
                    int id = _agendasRepository.CreateAgenda(reserva);

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
            _agendasRepository.DeleteAgenda2(id);

            TempData["Mensaje"] = "Reserva eliminada";

            return RedirectToAction("Index");
        }

    }
}