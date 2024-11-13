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
    public class RecetasController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public RecetasController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/Recetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecetasDTO>>> GetRecetas()
        {
            var recetas = await _context.Recetas
                .Include(r => r.PacienteFkkNavigation)
                .Include(r => r.FarmaceuticoFkkNavigation)
                .ThenInclude(fm => fm.EspecialidadFkNavigation)
                .Include(r => r.MedicamentoFkNavigation)
                .ThenInclude(m => m.CategoriaFkNavigation)
                .Select(r => new RecetasDTO
                {
                    IdRegistroDeEntrega = r.IdRegistroDeEntrega,
                    FechaDeEntrega = r.FechaDeEntrega,
                    CantidadDeEntrega = r.CantidadDeEntrega,
                    Observaciones = r.Observaciones,
                    Paciente = new PacientesDTO
                    {
                        Nombre = r.PacienteFkkNavigation.Nombre,
                        Apellido = r.PacienteFkkNavigation.Apellido,
                        Cedula = r.PacienteFkkNavigation.Cedula,
                    },
                    Doctor = new DoctoresDTO
                    {
                        NombreCompleto = r.FarmaceuticoFkkNavigation.NombreCompleto,
                        Cedula = r.FarmaceuticoFkkNavigation.Cedula,
                        Especialidad = new EspecialidadesDTO
                        {
                            Especialidad = r.FarmaceuticoFkkNavigation.EspecialidadFkNavigation.Especialidad
                        }

                    },
                    Medicamento = new MedicamentosDTO
                    {
                        Nombre = r.MedicamentoFkNavigation.Nombre,
                        CantidadStock = r.MedicamentoFkNavigation.CantidadStock,
                        Categoria = new CategoriaDTO
                        {
                            NombreCategoria = r.MedicamentoFkNavigation.CategoriaFkNavigation.NombreCategoria
                        }
                    }

                }
                )
                .ToListAsync();
            return Ok(recetas);
        }






        // GET: api/Recetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receta>> GetReceta(int id)
        {
            var receta = await _context.Recetas.FindAsync(id);

            if (receta == null)
            {
                return NotFound();
            }

            return receta;
        }

        // PUT: api/Recetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceta(int id, Receta receta)
        {
            if (id != receta.IdRegistroDeEntrega)
            {
                return BadRequest();
            }

            _context.Entry(receta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetaExists(id))
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

        // POST: api/Recetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754



        // Cada vez que se agregue una cantidad de medicamento a la receta, se debe restar de la cantidad en stock
        [HttpPost]
        public async Task<IActionResult> CrearReceta([FromBody] CreateReceta recetasDTO)
        {
            if (recetasDTO == null || recetasDTO.CantidadDeEntrega <= 0)
            {
                BadRequest("No se permiten cantidades iguales o menores que cero");
            }
            var medicamentos = await _context.Medicamentos.FindAsync(recetasDTO.IdMedicamento);

            if (medicamentos == null)
            {
                return BadRequest("El medicamento no existe");
            }
            if (medicamentos.CantidadStock < recetasDTO.CantidadDeEntrega)
            {
                return BadRequest("No hay suficiente medicamento en stock");
            }

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(r => r.IdPacientes == recetasDTO.IdPaciente);
            if (paciente == null)
            {
                return BadRequest("No existe el paciente");
            }


            var doctor = await _context.Doctores.FirstOrDefaultAsync(r => r.IdDoctores == recetasDTO.IdDoctor);
            if (doctor == null)
            {
                return BadRequest("No existe el doctor");
            }


            var medicamento = await _context.Medicamentos.FirstOrDefaultAsync(r => r.Idmedicamento == recetasDTO.IdMedicamento);
            if (medicamento == null)
            {
                return BadRequest("No existe el medicamento");
            }

            var nuevaReceta = new Receta
            {
                FechaDeEntrega = recetasDTO.FechaDeEntrega,
                CantidadDeEntrega = recetasDTO.CantidadDeEntrega,
                Observaciones = recetasDTO.Observaciones,
                PacienteFkk = paciente.IdPacientes,
                FarmaceuticoFkk = doctor.IdDoctores,
                MedicamentoFk = medicamento.Idmedicamento
            };
            medicamentos.CantidadStock -= recetasDTO.CantidadDeEntrega;
            _context.Recetas.Add(nuevaReceta);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetReceta", new { id = nuevaReceta.IdRegistroDeEntrega }, nuevaReceta);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar la receta: {ex.Message}");
            }
        }

        // DELETE: api/Recetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            var receta = await _context.Recetas.FindAsync(id);
            if (receta == null)
            {
                return NotFound();
            }

            _context.Recetas.Remove(receta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecetaExists(int id)
        {
            return _context.Recetas.Any(e => e.IdRegistroDeEntrega == id);
        }
    }
}
