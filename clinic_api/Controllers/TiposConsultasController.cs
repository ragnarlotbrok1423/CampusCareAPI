using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clinic_api.Models;

namespace clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposConsultasController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public TiposConsultasController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/TiposConsultas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposConsulta>>> GetTiposConsultas()
        {
            return await _context.TiposConsultas.ToListAsync();
        }

        // GET: api/TiposConsultas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TiposConsulta>> GetTiposConsulta(int id)
        {
            var tiposConsulta = await _context.TiposConsultas.FindAsync(id);

            if (tiposConsulta == null)
            {
                return NotFound();
            }

            return tiposConsulta;
        }

        // PUT: api/TiposConsultas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTiposConsulta(int id, TiposConsulta tiposConsulta)
        {
            if (id != tiposConsulta.IdtiposConsultas)
            {
                return BadRequest();
            }

            _context.Entry(tiposConsulta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiposConsultaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TiposConsultas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TiposConsulta>> PostTiposConsulta(TiposConsulta tiposConsulta)
        {
            _context.TiposConsultas.Add(tiposConsulta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTiposConsulta", new { id = tiposConsulta.IdtiposConsultas }, tiposConsulta);
        }

        // DELETE: api/TiposConsultas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiposConsulta(int id)
        {
            var tiposConsulta = await _context.TiposConsultas.FindAsync(id);
            if (tiposConsulta == null)
            {
                return NotFound();
            }

            _context.TiposConsultas.Remove(tiposConsulta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiposConsultaExists(int id)
        {
            return _context.TiposConsultas.Any(e => e.IdtiposConsultas == id);
        }
    }
}
