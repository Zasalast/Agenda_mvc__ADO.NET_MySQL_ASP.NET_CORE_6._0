using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{
    public class AgendamientosController : Controller
    {
        private readonly AgendamientosRepository _agendamientoRepository;
        private readonly MySQLSettings _connectionString;
        public IActionResult Index()
        {
            List<Agendamiento> model = _agendamientoRepository.GetAgendamientos();
            return View(model);
        }
    }
}
