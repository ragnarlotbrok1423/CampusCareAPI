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
    public class TipajesSanguineosController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public TipajesSanguineosController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/TipajesSanguineos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipajesSanguineo>>> GetTipajesSanguineos()
        {
            return await _context.TipajesSanguineos.ToListAsync();
        }

        // GET: api/TipajesSanguineos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipajesSanguineo>> GetTipajesSanguineo(int id)
        {
            var tipajesSanguineo = await _context.TipajesSanguineos.FindAsync(id);

            if (tipajesSanguineo == null)
            {
                return NotFound();
            }

            return tipajesSanguineo;
        }

        // PUT: api/TipajesSanguineos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipajesSanguineo(int id, TipajesSanguineo tipajesSanguineo)
        {
            if (id != tipajesSanguineo.IdtipajesSanguineos)
            {
                return BadRequest();
            }

            _context.Entry(tipajesSanguineo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipajesSanguineoExists(id))
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

        // POST: api/TipajesSanguineos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipajesSanguineo>> PostTipajesSanguineo(TipajesSanguineo tipajesSanguineo)
        {
            _context.TipajesSanguineos.Add(tipajesSanguineo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TipajesSanguineoExists(tipajesSanguineo.IdtipajesSanguineos))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTipajesSanguineo", new { id = tipajesSanguineo.IdtipajesSanguineos }, tipajesSanguineo);
        }

        // DELETE: api/TipajesSanguineos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipajesSanguineo(int id)
        {
            var tipajesSanguineo = await _context.TipajesSanguineos.FindAsync(id);
            if (tipajesSanguineo == null)
            {
                return NotFound();
            }

            _context.TipajesSanguineos.Remove(tipajesSanguineo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipajesSanguineoExists(int id)
        {
            return _context.TipajesSanguineos.Any(e => e.IdtipajesSanguineos == id);
        }
    }
}
