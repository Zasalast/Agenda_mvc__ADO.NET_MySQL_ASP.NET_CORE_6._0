using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Models;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Repositories;

namespace Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Controllers
{
    public class AgendamientosController : Controller
    {
        private readonly  AgendamientoRepository _agendamientoRepository;

        public AgendamientosController(AgendamientoRepository agendamientoRepository)
        {
            _agendamientoRepository = agendamientoRepository;
        }

        // GET: Agendamientos
        public IActionResult Index()
        {
            var agendamientos = _agendamientoRepository.GetAgendamientos();
            return View(agendamientos);
        }

        // GET: Agendamientos/Details/5
        public IActionResult Details(int id)
        {
            var agendamiento = _agendamientoRepository.GetAgendamientoById(id);
            if (agendamiento == null)
            {
                return NotFound();
            }
            return View(agendamiento);
        }

        // GET: Agendamientos/Create
        public IActionResult Create()
        {
            // Cargar lista de clientes y reservas disponibles
            ViewBag.Clientes = _clienteRepository.GetClientes();
            ViewBag.Reservas = _reservaRepository.GetReservasDisponibles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Agendamiento agendamiento)
        {
            if (ModelState.IsValid)
            {
                _agendamientoRepository.CreateAgendamiento(agendamiento);
                return RedirectToAction(nameof(Index));
            }
            return View(agendamiento);
        }

        // GET: Agendamientos/Edit/5
        public IActionResult Edit(int id)
        {
            var agendamiento = _agendamientoRepository.GetAgendamientoById(id);
            if (agendamiento == null)
            {
                return NotFound();
            }
            return View(agendamiento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Agendamiento agendamiento)
        {
            if (id != agendamiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _agendamientoRepository.UpdateAgendamiento(agendamiento);
                return RedirectToAction(nameof(Index));
            }
            return View(agendamiento);
        }

        // GET: Agendamientos/Delete/5
        public IActionResult Delete(int id)
        {
            var agendamiento = _agendamientoRepository.GetAgendamientoById(id);
            if (agendamiento == null)
            {
                return NotFound();
            }
            return View(agendamiento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _agendamientoRepository.DeleteAgendamiento(id);
            return RedirectToAction(nameof(Index));
        }
    }
}