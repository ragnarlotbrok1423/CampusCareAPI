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

namespace clinic_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public PedidosController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidosDTO>>> GetPedidos()
        {
            var pedido = await _context.Pedidos
                .Include(r => r.MedicamentoFkNavigation)
                .ThenInclude(mc => mc.CategoriaFkNavigation)
                .Include(r => r.FarmaceuticoFkNavigation)
                .ThenInclude(fr => fr.EspecialidadFkNavigation)
                .Select(r => new PedidosDTO
                {

                    Idpedidos = r.Idpedidos,
                    Cantidad = r.Cantidad,
                    Fecha = r.Fecha,

                    Medicamentos = new MedicamentosDTO
                    {
                        Nombre = r.MedicamentoFkNavigation.Nombre,

                        Categoria = new CategoriaDTO
                        {
                            NombreCategoria = r.MedicamentoFkNavigation.CategoriaFkNavigation.NombreCategoria
                        }
                    },
                    Doctores = new DoctoresDTO
                    {
                        NombreCompleto = r.FarmaceuticoFkNavigation.NombreCompleto,
                        Cedula = r.FarmaceuticoFkNavigation.Cedula,
                        Especialidad = new EspecialidadesDTO
                        {
                            Especialidad = r.FarmaceuticoFkNavigation.EspecialidadFkNavigation.Especialidad
                        }
                    }



                })
                .ToListAsync();
            return Ok(pedido);

        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Idpedidos)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] createPedidoDTO createPedidoDTO)
        {
            if (createPedidoDTO == null || createPedidoDTO.Cantidad <= 0)
            {
                return BadRequest("no se aceptan numero negativos ni iguales que cero");
            }
            var medicamentos = await _context.Medicamentos.FindAsync(createPedidoDTO.MedicamentoFk);
            if (medicamentos == null)
            {
                return BadRequest("El medicamento no existe");
            }

            var farmaceutico = await _context.Pacientes.FirstOrDefaultAsync(p => p.IdPacientes == createPedidoDTO.FarmaceuticoFk);
            if (farmaceutico == null)
            {
                return BadRequest("El farmaceutico no existe");
            }
            var crearPedido = new Pedido
            {
                Cantidad = createPedidoDTO.Cantidad,
                Fecha = createPedidoDTO.Fecha,
                MedicamentoFk = medicamentos.Idmedicamento,
                FarmaceuticoFk = farmaceutico.IdPacientes
            };
            medicamentos.CantidadStock += createPedidoDTO.Cantidad;
            _context.Pedidos.Add(crearPedido);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPedido", new { id = crearPedido.Idpedidos }, crearPedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al guardar el pedido: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Idpedidos == id);
        }
    }
}
