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
    [Route("api/[controller]/[action]")]
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
            var result = from d in _context.Actividades
                         join b in _context.Categorias on d.CategoriaId equals b.Id
                         select new
                         {
                             Descripcion = d.Descripcion,
                             Fecha = d.Fecha,
                             HoraInicial = d.HoraInicial,
                             HoraFinal = d.HoraFinal,
                             Id = d.Id,
                             CategoriaId = d.CategoriaId,
                             NombreDeCategoria = b.Nombre
                         };

            return Ok(result.ToListAsync());
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

        [HttpGet]
        public async Task<IActionResult> GetActividadByFecha()
        {
            // var actividad = await _context.Actividades.FirstOrDefaultAsync(x => x.Fecha == new DateTime());
            // var actividades = await _context.Actividades.Where(t => t.Fecha.Date == DateTime.Today).ToListAsync();
            var result = from d in _context.Actividades
                         join b in _context.Categorias on d.CategoriaId equals b.Id
                         where d.Fecha.Date == DateTime.Today
                         select new
                         {
                             Descripcion = d.Descripcion,
                             Fecha = d.Fecha,
                             HoraInicial = d.HoraInicial,
                             HoraFinal = d.HoraFinal,
                             Id = d.Id,
                             CategoriaId = d.CategoriaId,
                             NombreDeCategoria = b.Nombre
                         };

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetActividadByFilter(string fechaInicial, string fechaFinal)
        {
            // var actividad = await _context.Actividades.FirstOrDefaultAsync(x => x.Fecha == new DateTime());
            // var actividades = await _context.Actividades.Where(t => t.Fecha.Date == DateTime.Today).ToListAsync();

            var fi = Convert.ToDateTime(fechaInicial);
            var ff = Convert.ToDateTime(fechaFinal);

            var result = from d in _context.Actividades
                         join b in _context.Categorias on d.CategoriaId equals b.Id
                         where d.Fecha.Date >= fi && d.Fecha.Date <= ff
                         select new
                         {
                             Descripcion = d.Descripcion,
                             Fecha = d.Fecha,
                             HoraInicial = d.HoraInicial,
                             HoraFinal = d.HoraFinal,
                             Id = d.Id,
                             CategoriaId = d.CategoriaId,
                             NombreDeCategoria = b.Nombre
                         };

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.ToListAsync());
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