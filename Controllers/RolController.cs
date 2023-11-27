using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.AspNetCore.Mvc;


namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{
    public class RolController : Controller
    {
        private readonly RolRepository _rolRepository;
        private readonly PermisosRepository _permisosRepository;

        public RolController(RolRepository rolRepository, PermisosRepository permisosRepository)
        {
            _rolRepository = rolRepository;
            _permisosRepository = permisosRepository;
        }

        // GET: /Rol
        public IActionResult Index()
        {
            var roles = _rolRepository.GetRoles();
            return View(roles);
        }

        // GET: /Rol/Details/5
        public IActionResult Details(int id)
        {
            var rol = _rolRepository.GetRolById(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // GET: /Rol/Create
        public IActionResult Create()
        {
            // Cargar lista de permisos disponibles
            ViewBag.Permisos = _permisosRepository.GetPermisos();
            return View();
        }

        // POST: /Rol/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rol rol, List<int> idPermisos)
        {
            if (ModelState.IsValid)
            {
                _rolRepository.CreateRol(rol, idPermisos);
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // GET: /Rol/Edit/5
        public IActionResult Edit(int id)
        {
            var rol = _rolRepository.GetRolById(id);
            if (rol == null)
            {
                return NotFound();
            }
            // Cargar lista de permisos disponibles
            ViewBag.Permisos = _permisosRepository.GetPermisos();
            return View(rol);
        }

        // POST: /Rol/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Rol rol, List<int> idPermisos)
        {
            if (id != rol.IdRol)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _rolRepository.UpdateRol(rol, idPermisos);
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // GET: /Rol/Delete/5
        public IActionResult Delete(int id)
        {
            var rol = _rolRepository.GetRolById(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: /Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _rolRepository.DeleteRol(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
