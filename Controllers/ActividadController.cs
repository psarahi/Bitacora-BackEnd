using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bitacora.API.Data;
using Bitacora.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bitacora.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadController : ControllerBase
    {

        private readonly DataContext _context;
        public ActividadController(DataContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> GetAll()
        {
            var Categorias = await _context.Actividades.ToListAsync();
            return Ok(Categorias);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Actividad>> GetActividadById(int id)
        {
            var actividad = await _context.Actividades.FindAsync(id);

            if (actividad == null)
            {
                return NotFound();
            }

            return actividad;
        }

        [HttpPost]
        public async Task<ActionResult<Actividad>> PostActividad(Actividad actividad)
        {
            _context.Actividades.Add(actividad);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetActividadById), new { id = actividad.Id }, actividad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActividad(int id, Actividad actividad)
        {
            if (id != actividad.Id)
            {
                return BadRequest();
            }

            _context.Entry(actividad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Actividad>> DeleteActividad(int id)
        {
            var actividad = await _context.Actividades.FindAsync(id);

            if (actividad == null)
            {
                return NotFound();
            }

            _context.Actividades.Remove(actividad);
            await _context.SaveChangesAsync();

            return actividad;
        }

    }
}