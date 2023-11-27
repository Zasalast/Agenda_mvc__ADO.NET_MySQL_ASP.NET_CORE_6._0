using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Agenda()
        {
            return View();
        }
        public IActionResult Agendamiento()
        {
            return View();
        }
        public IActionResult AgendamientoDelete()
        {
            return View();
        }
        public IActionResult AgendamientoList()
        {
            return View();
        }
        public IActionResult AgendamientoCreate()
        {
            return View();
        }
        public IActionResult AgendaDelete()
        {
            return View();
        }
        public IActionResult AgendaList()
        {
            return View();
        }
        public IActionResult AgendaCreate()
        {
            return View();
        }
        public IActionResult AgendamientoEdit()
        {
            return View();
        }

        public IActionResult AgendaEdit()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Cancelar()
        {
            return View();
        }
        
        public IActionResult Asistencia()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
