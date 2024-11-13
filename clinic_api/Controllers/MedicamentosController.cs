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
    public class MedicamentosController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public MedicamentosController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/Medicamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicamentosDTO>>> GetMedicamentos()
        {
            var medicamentos = await _context.Medicamentos
                 .Include(m => m.CategoriaFkNavigation)
                 .Select(m => new MedicamentosDTO
                 {
                     Idmedicamento = m.Idmedicamento,
                     Nombre = m.Nombre,
                     CantidadStock = m.CantidadStock,
                     Categoria = new CategoriaDTO
                     {
                         NombreCategoria = m.CategoriaFkNavigation.NombreCategoria
                     }

                 })
                 .ToListAsync();
            return Ok(medicamentos);
        }

        [HttpGet("categoria/{id}")]
        public async Task<ActionResult<IEnumerable<MedicamentosDTO>>> GetMedicamentoByCategoria(int id)
        {
            var medicamentos = await _context.Medicamentos
                .Include(m => m.CategoriaFkNavigation)
                .Where(m => m.CategoriaFk == id)
                .Select(e => new MedicamentosDTO
                {

                    Idmedicamento = e.Idmedicamento,
                    Nombre = e.Nombre,
                    Categoria = new CategoriaDTO
                    {
                        NombreCategoria = e.CategoriaFkNavigation.NombreCategoria
                    }

                })
                .ToListAsync();
            if (medicamentos == null || !medicamentos.Any())
            {
                return NotFound(new { message = "No hay Medicamentos para esta categoria" });
            }
            return Ok(medicamentos);







        }




        // GET: api/Medicamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);

            if (medicamento == null)
            {
                return NotFound();
            }

            return medicamento;
        }

        // PUT: api/Medicamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento(int id, Medicamento medicamento)
        {
            if (id != medicamento.Idmedicamento)
            {
                return BadRequest();
            }

            _context.Entry(medicamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentoExists(id))
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

        // POST: api/Medicamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicamento>> PostMedicamento(AddMedicamentoDTO medicamentoDTO)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == medicamentoDTO.CategoriaFk);
            if (categoria == null)
            {
                return BadRequest("La categoria no existe");
            }
            var medicamento = new Medicamento
            {
                Nombre = medicamentoDTO.Nombre,
                CantidadStock = medicamentoDTO.CantidadStock,
                CategoriaFk = categoria.IdCategoria

            };

            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();
            return Ok(medicamento);

        }

        // DELETE: api/Medicamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicamentoExists(int id)
        {
            return _context.Medicamentos.Any(e => e.Idmedicamento == id);
        }
    }
}
