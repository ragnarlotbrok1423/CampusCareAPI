using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clinic_api.Models;
using clinic_api.DTO_s;

namespace clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public DoctoresController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/Doctores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctore>>> GetDoctores()
        {
            return await _context.Doctores.ToListAsync();
        }

        // GET: api/Doctores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctore>> GetDoctore(int id)
        {
            var doctore = await _context.Doctores.FindAsync(id);

            if (doctore == null)
            {
                return NotFound();
            }

            return doctore;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] DoctorLoginDTO doctorLoginDTO)
        {
            var doctores = _context.Doctores
                .FirstOrDefault(d => d.Cedula == doctorLoginDTO.Cedula);
            if (doctores == null)
            {
                return Unauthorized("Usuario no registrado");
            }
            if (doctores.Contraseña != doctorLoginDTO.Contraseña)
            {
                return Unauthorized("Contraseña incorrecta");
            }
            var doctorLoginResponse = new DoctorLoginResponse
            {
                IdDoctores = doctores.IdDoctores,
                NombreCompleto = doctores.NombreCompleto,
                EspecialidadFk = doctores.EspecialidadFk
            };
            return Ok(doctorLoginResponse);
        }

        // PUT: api/Doctores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctore(int id, Doctore doctore)
        {
            if (id != doctore.IdDoctores)
            {
                return BadRequest();
            }

            _context.Entry(doctore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctoreExists(id))
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

        // POST: api/Doctores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctore>> PostDoctore(Doctore doctore)
        {
            _context.Doctores.Add(doctore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctore", new { id = doctore.IdDoctores }, doctore);
        }

        // DELETE: api/Doctores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctore(int id)
        {
            var doctore = await _context.Doctores.FindAsync(id);
            if (doctore == null)
            {
                return NotFound();
            }

            _context.Doctores.Remove(doctore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctoreExists(int id)
        {
            return _context.Doctores.Any(e => e.IdDoctores == id);
        }
    }
}
