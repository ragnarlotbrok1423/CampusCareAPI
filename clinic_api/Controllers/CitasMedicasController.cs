using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using campusCareAPI.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Linq.Expressions;
using campusCareAPI.DTO_s;
using clinic_api.DTO_s;
using clinic_api.Models;

namespace campusCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasMedicasController : ControllerBase
    {
        private readonly CampusCareContext _context;

        public CitasMedicasController(CampusCareContext context)
        {
            _context = context;
        }

        // GET: api/CitasMedicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitasMedicasDTO>>> GetCitasMedicas()
        {
            var citasMedicas = await _context.CitasMedicas

                .Include(c => c.TipoConsultaNavigation)
                .Include(c => c.EstadoNavigation)
                .Include(c => c.PacienteNavigation)
                .Include(c => c.DoctorNavigation)
                .Select(C => new CitasMedicasDTO
                {
                    IdcitasMedicas = C.IdcitasMedicas,
                    Fecha = C.Fecha,
                    
                    Estado = new EstadoDTO
                    {
                      NombreEstado  = C.EstadoNavigation.NombreEstado
                    },

                    TipoConsulta = new TipoConsultaDTO
                    {
                        TipoConsulta = C.TipoConsultaNavigation.TipoConsulta
                    },
                    Usuarios = new PacientesDTO
                    {
                        Nombre = C.PacienteNavigation.Nombre,
                        Apellido = C.PacienteNavigation.Apellido,
                        Cedula = C.PacienteNavigation.Cedula,
                        NombreUsuario = C.PacienteNavigation.NombreUsuario
                    },

                    Doctores = new DTO_s.DoctoresDTO
                    {
                        NombreCompleto = C.DoctorNavigation.NombreCompleto,
                        Cedula = C.DoctorNavigation.Cedula,
                        Diploma = C.DoctorNavigation.Diploma,
                        Perfil = C.DoctorNavigation.Perfil,
                    }


                })
                .ToListAsync();
            return Ok(citasMedicas);
        }
        // controlador para obtener pacientes por doctor asignado
        [HttpGet("doctor/{id}")]
        public async Task<ActionResult<IEnumerable<CitasMedicasDTO>>> GetCitasMedicasByDoctor(int id)
        {
            var citasMedicas = await _context.CitasMedicas
                .Include(c => c.PacienteNavigation)
                .Include(c => c.DoctorNavigation)
                .Where(c => c.Doctor == id)
                .Select(s => new CitasMedicasDTO
                {
                    IdcitasMedicas = s.IdcitasMedicas,
                    TipoConsulta = new TipoConsultaDTO
                    {
                        TipoConsulta = s.TipoConsultaNavigation.TipoConsulta
                    },
                    Usuarios = new PacientesDTO
                    {
                        IdUsuarios = s.PacienteNavigation.IdPacientes,
                        Nombre = s.PacienteNavigation.Nombre,
                        Apellido = s.PacienteNavigation.Apellido,
                        Cedula = s.PacienteNavigation.Cedula

                    },
                    Doctores = new DoctoresDTO
                    {
                        IdDoctores = s.DoctorNavigation.IdDoctores,
                        NombreCompleto = s.DoctorNavigation.NombreCompleto
                        
                    }

                })


                .ToListAsync();

            if (citasMedicas == null || !citasMedicas.Any())
            {
                return NotFound("No hay citas médicas registradas para este doctor.");
            }

            return Ok(citasMedicas);
        }

        [HttpGet("tipoCita/{id}")]
        public async Task<ActionResult<IEnumerable<CitasMedicasDTO>>> GetCitasMedicasByTipoCita(int id)
        {
            var citaMedica = await _context.CitasMedicas
                .Include(T => T.TipoConsultaNavigation)
                .Where(T => T.TipoConsulta == id)
                .Select(e => new CitasMedicasDTO
                    {
                        IdcitasMedicas = e.IdcitasMedicas,
                        Fecha = e.Fecha,
                        Descripcion = e.Descripcion,
                        TipoConsulta = new TipoConsultaDTO
                        {
                            TipoConsulta = e.TipoConsultaNavigation.TipoConsulta
                        },
                        Usuarios = new PacientesDTO
                        {
                            Nombre = e.PacienteNavigation.Nombre,
                            Apellido = e.PacienteNavigation.Apellido,
                            Cedula = e.PacienteNavigation.Cedula,
                        },
                        Doctores = new DoctoresDTO
                        {
                            NombreCompleto = e.DoctorNavigation.NombreCompleto,
                            Cedula = e.DoctorNavigation.Cedula,
                        }
                        

                    }
                ).ToListAsync();
            if (citaMedica == null || !citaMedica.Any())
            {
                return NotFound(new { message = "No hay registros para este tipo de citas" });
            }
            return Ok(citaMedica);
        }




        // GET: api/CitasMedicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitasMedica>> GetCitasMedica(int id)
        {
            var citasMedica = await _context.CitasMedicas.FindAsync(id);

            if (citasMedica == null)
            {
                return NotFound();
            }

            return citasMedica;
        }

        [HttpGet("paciente/{idPaciente}")]
        public async Task<ActionResult<IEnumerable<CitasMedicasDTO>>> GetCitasMedicasByPaciente(int idPaciente)
        {
            // Verificar si el paciente existe
            var paciente = await _context.Pacientes.FindAsync(idPaciente);
            if (paciente == null)
            {
                return NotFound("El paciente no existe.");
            }

            // Obtener las citas del paciente específico
            var citasMedicas = await _context.CitasMedicas
                .Where(c => c.Paciente == idPaciente)

                .Include(c => c.TipoConsultaNavigation)
                .Include(c => c.PacienteNavigation)
                .Include(c => c.DoctorNavigation)
                .Select(C => new CitasMedicasDTO
                {
                    IdcitasMedicas = C.IdcitasMedicas,
                    Fecha = C.Fecha,

                    TipoConsulta = new TipoConsultaDTO
                    {
                        TipoConsulta = C.TipoConsultaNavigation.TipoConsulta
                    },
                    Usuarios = new PacientesDTO
                    {
                        Nombre = C.PacienteNavigation.Nombre,
                        Apellido = C.PacienteNavigation.Apellido,
                        Cedula = C.PacienteNavigation.Cedula,
                        NombreUsuario = C.PacienteNavigation.NombreUsuario
                    },
                    Doctores = new DTO_s.DoctoresDTO
                    {
                        NombreCompleto = C.DoctorNavigation.NombreCompleto,
                        Cedula = C.DoctorNavigation.Cedula,

                        Diploma = C.DoctorNavigation.Diploma,
                        Perfil = C.DoctorNavigation.Perfil,
                    }
                })
                .ToListAsync();

            if (citasMedicas == null || !citasMedicas.Any())
            {
                return NotFound("No hay citas médicas registradas para este paciente.");
            }

            return Ok(citasMedicas);
        }







        // PUT: api/CitasMedicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitasMedica(int id, CitasMedica citasMedica)
        {
            if (id != citasMedica.IdcitasMedicas)
            {
                return BadRequest();
            }

            _context.Entry(citasMedica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitasMedicaExists(id))
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

        // POST: api/CitasMedicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
[HttpPost]
public async Task<ActionResult<CitasMedica>> PostCitasMedica(CreateCitasMedicasDTO citasMedicasDTO)
{
    var usuario = await _context.Pacientes.FirstOrDefaultAsync(c => c.IdPacientes == citasMedicasDTO.IdUsuario);
    if (usuario == null)
    {
        return BadRequest("El usuario no existe");
    }
    var doctor = await _context.Doctores.FirstOrDefaultAsync(c => c.IdDoctores == citasMedicasDTO.IdDoctor);
    if (doctor == null)
    {
        return BadRequest("El doctor no existe");
    }
    var tipoConsulta = await _context.TiposConsultas.FirstOrDefaultAsync(c => c.IdtiposConsultas == citasMedicasDTO.IdTipoConsulta);
    if (tipoConsulta == null)
    {
        return BadRequest("El tipo de consulta no existe");
    }

    // Asignar IdEstado con el valor predeterminado 1
    var citasMedica = new CitasMedica
    {
        Fecha = citasMedicasDTO.Fecha,
        TipoConsulta = tipoConsulta.IdtiposConsultas,
        Estado = 1, // Valor predeterminado
        Descripcion = citasMedicasDTO.Observaciones,
        Paciente = usuario.IdPacientes,
        Doctor = doctor.IdDoctores
    };

    _context.CitasMedicas.Add(citasMedica);
    await _context.SaveChangesAsync();
    return Ok(citasMedica);


}

        // DELETE: api/CitasMedicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitasMedica(int id)
        {
            var citasMedica = await _context.CitasMedicas.FindAsync(id);
            if (citasMedica == null)
            {
                return NotFound();
            }

            _context.CitasMedicas.Remove(citasMedica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitasMedicaExists(int id)
        {
            return _context.CitasMedicas.Any(e => e.IdcitasMedicas == id);
        }
    }
}
