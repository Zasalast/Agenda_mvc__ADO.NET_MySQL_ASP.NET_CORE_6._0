using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;  // Importante agregar para el manejo de errores

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{
    public class PermisosController : Controller
    {
        private readonly PermisosRepository _permisosRepository;
        private readonly ILogger<PermisosController> _logger;  // Agregar ILogger para el manejo de errores

        public PermisosController(PermisosRepository permisosRepository, ILogger<PermisosController> logger)
        {
            _permisosRepository = permisosRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var permisos = _permisosRepository.GetPermisos();
            return View(permisos);
        }

        public IActionResult Details(int id)
        {
            var permiso = _permisosRepository.GetPermisoById(id);
            return View(permiso);
        }

        [HttpPost]
        public IActionResult Create(Permiso permiso)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _permisosRepository.CreatePermiso(permiso);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al crear el permiso: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Se produjo un error al intentar crear el permiso.");
                }
            }

            return View(permiso);
        }

        public IActionResult Edit(int id)
        {
            var permiso = _permisosRepository.GetPermisoById(id);

            if (permiso == null)
            {
                return NotFound();
            }

            return View(permiso);
        }

        [HttpPost]
        public IActionResult Edit(int id, Permiso permiso)
        {
            if (id != permiso.IdPermiso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingPermiso = _permisosRepository.GetPermisoById(id);

                    if (existingPermiso == null)
                    {
                        return NotFound();
                    }

                    _permisosRepository.UpdatePermiso(permiso);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error al editar el permiso: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Se produjo un error al intentar editar el permiso.");
                }
            }

            return View(permiso);
        }

        public IActionResult Delete(int id)
        {
            var permiso = _permisosRepository.GetPermisoById(id);

            if (permiso == null)
            {
                return NotFound();
            }

            return View(permiso);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _permisosRepository.DeletePermiso(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el permiso: {ex.Message}");
                return RedirectToAction("Index");  // Puedes redirigir a una página de error específica si lo prefieres
            }
        }
    }
}
