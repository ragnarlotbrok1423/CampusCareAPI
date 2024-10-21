using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using campusCareAPI.Models;
using clinic_api.Models;

namespace campusCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformacionesMedicasController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public InformacionesMedicasController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/InformacionesMedicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InformacionesMedica>>> GetInformacionesMedicas()
        {
            return await _context.InformacionesMedicas.ToListAsync();
        }

        // GET: api/InformacionesMedicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InformacionesMedica>> GetInformacionesMedica(int id)
        {
            var informacionesMedica = await _context.InformacionesMedicas.FindAsync(id);

            if (informacionesMedica == null)
            {
                return NotFound();
            }

            return informacionesMedica;
        }

        // PUT: api/InformacionesMedicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInformacionesMedica(int id, InformacionesMedica informacionesMedica)
        {
            if (id != informacionesMedica.IdinformacionesMedicas)
            {
                return BadRequest();
            }

            _context.Entry(informacionesMedica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformacionesMedicaExists(id))
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

        // POST: api/InformacionesMedicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InformacionesMedica>> PostInformacionesMedica(InformacionesMedica informacionesMedica)
        {
            _context.InformacionesMedicas.Add(informacionesMedica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformacionesMedica", new { id = informacionesMedica.IdinformacionesMedicas }, informacionesMedica);
        }

        // DELETE: api/InformacionesMedicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformacionesMedica(int id)
        {
            var informacionesMedica = await _context.InformacionesMedicas.FindAsync(id);
            if (informacionesMedica == null)
            {
                return NotFound();
            }

            _context.InformacionesMedicas.Remove(informacionesMedica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InformacionesMedicaExists(int id)
        {
            return _context.InformacionesMedicas.Any(e => e.IdinformacionesMedicas == id);
        }
    }
}
