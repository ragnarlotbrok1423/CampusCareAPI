using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clinic_api.Models;
using clinic_api.DTO_s;
using campusCareAPI.Models;
using campusCareAPI.DTO_s;

namespace clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonantesSangresController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public DonantesSangresController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/DonantesSangres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonantesDTO>>> GetDonantesSangres()
        {
            var donantes = await _context.DonantesSangres
                 .Include(d => d.PacienteFkNavigation)
                 .Include(d => d.DoctorFkNavigation)
                 .Select(p => new DonantesDTO
                 {
                     IddonantesSangre = p.IddonantesSangre,
                     Fecha = p.Fecha,
                     paciente = new PacientesDTO
                     {
                         Nombre = p.PacienteFkNavigation.Nombre,
                         Apellido = p.PacienteFkNavigation.Apellido,
                         Cedula = p.PacienteFkNavigation.Cedula,
                         InformacionMedica = new InformacionMedicaDTO
                         {
                             TipajeSanguineo = new TipajesSanguineosDTO
                             {
                                 TipoSanguineo = p.PacienteFkNavigation.InformacionMedicaNavigation.TipajeNavigation.TipoSanguineo
                             }
                         }

                     },
                     doctores = new DoctoresDTO
                     {
                         NombreCompleto = p.DoctorFkNavigation.NombreCompleto,
                         Cedula = p.DoctorFkNavigation.Cedula,
                     }

                 }).ToListAsync();
            return Ok(donantes);
        }

        // GET: api/DonantesSangres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonantesSangre>> GetDonantesSangre(int id)
        {
            var donantesSangre = await _context.DonantesSangres.FindAsync(id);

            if (donantesSangre == null)
            {
                return NotFound();
            }

            return donantesSangre;
        }

        // PUT: api/DonantesSangres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonantesSangre(int id, DonantesSangre donantesSangre)
        {
            if (id != donantesSangre.IddonantesSangre)
            {
                return BadRequest();
            }

            _context.Entry(donantesSangre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonantesSangreExists(id))
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

        // POST: api/DonantesSangres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DonantesSangre>> PostDonantesSangre(DonantesSangre donantesSangre)
        {
            _context.DonantesSangres.Add(donantesSangre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonantesSangre", new { id = donantesSangre.IddonantesSangre }, donantesSangre);
        }

        // DELETE: api/DonantesSangres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonantesSangre(int id)
        {
            var donantesSangre = await _context.DonantesSangres.FindAsync(id);
            if (donantesSangre == null)
            {
                return NotFound();
            }

            _context.DonantesSangres.Remove(donantesSangre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonantesSangreExists(int id)
        {
            return _context.DonantesSangres.Any(e => e.IddonantesSangre == id);
        }
    }
}
