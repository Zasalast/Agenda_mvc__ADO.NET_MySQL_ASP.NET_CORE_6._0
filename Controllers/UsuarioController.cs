using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly RolRepository _rolRepository;
        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        // GET: Usuarios
        public IActionResult Index()
        {
            var usuarios = _usuarioRepository.GetUsuarios();
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public IActionResult Details(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        // GET: Usuarios/Create
        public IActionResult Create()
        {
            // Cargar lista de roles disponibles
            ViewBag.Roles = _rolRepository.GetRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Persona usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.CreateUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public IActionResult Edit(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            // Cargar lista de roles disponibles
            ViewBag.Roles = _rolRepository.GetRoles();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Persona usuario)
        {
            if (id != usuario.IdPersona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _usuarioRepository.UpdateUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public IActionResult Delete(int id)
        {
            var usuario = _usuarioRepository.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _usuarioRepository.DeletePersona(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
    
 
