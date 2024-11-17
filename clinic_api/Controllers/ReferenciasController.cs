using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clinic_api.Models;
using clinic_api.DTO_s;
using campusCareAPI.DTO_s;
using campusCareAPI.Models;

namespace clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenciasController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public ReferenciasController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/Referencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferenciasDTO>>> GetReferencias()
        {
            var referencias = await _context.Referencias
                .Include(r => r.DoctorFkNavigation)
                .Include(r => r.PacienteFkNavigation)
                .Select(r => new ReferenciasDTO
                {
                    Idreferencias = r.Idreferencias,
                    Fecha = r.Fecha,
                    CondicionMedica = r.CondicionMedica,
                    Sintomas = r.Sintomas,
                    Diagnostico = r.Diagnostico,
                    Especialidad = r.Especialidad,
                    Pdf = r.Pdf,
                    Doctor = new DoctoresDTO
                    {
                        NombreCompleto = r.DoctorFkNavigation.NombreCompleto,
                        Cedula = r.DoctorFkNavigation.Cedula
                    },
                    Paciente = new PacientesDTO
                    {
                        Nombre = r.PacienteFkNavigation.Nombre,
                        Apellido = r.PacienteFkNavigation.Apellido,
                        Cedula = r.PacienteFkNavigation.Cedula,
                    }

                })
                .ToListAsync();
            return Ok(referencias);
        }

        // GET: api/Referencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Referencia>> GetReferencia(int id)
        {
            var referencia = await _context.Referencias.FindAsync(id);

            if (referencia == null)
            {
                return NotFound();
            }

            return referencia;
        }

        // PUT: api/Referencias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferencia(int id, Referencia referencia)
        {
            if (id != referencia.Idreferencias)
            {
                return BadRequest();
            }

            _context.Entry(referencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenciaExists(id))
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

        // POST: api/Referencias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Referencia>> PostReferencia(Referencia referencia)
        {
            _context.Referencias.Add(referencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReferencia", new { id = referencia.Idreferencias }, referencia);
        }

        // DELETE: api/Referencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferencia(int id)
        {
            var referencia = await _context.Referencias.FindAsync(id);
            if (referencia == null)
            {
                return NotFound();
            }

            _context.Referencias.Remove(referencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReferenciaExists(int id)
        {
            return _context.Referencias.Any(e => e.Idreferencias == id);
        }
    }
}
