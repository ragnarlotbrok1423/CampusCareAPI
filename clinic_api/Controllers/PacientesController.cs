using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using campusCareAPI.Models;
using System.Runtime.InteropServices;
using campusCareAPI.DTO_s;
using clinic_api.Models;

namespace campusCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly CampusCareContext _context;


        public PacientesController(CampusCareContext context)
        {
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginPacientDTO loginPacientDTO)
        {
            var pacientes = _context.Pacientes
                 .FirstOrDefault(u => u.NombreUsuario == loginPacientDTO.NombreUsuario);

            if (pacientes == null)
            {
                return Unauthorized("Usuario no registrado");
            }
            if (pacientes.Contraseña != loginPacientDTO.Contraseña)
            {
                return Unauthorized("Contraseña incorrecta");
            }


            var loginResponseDTO = new LoginResponseDTO
            {
                IdUsuarios = pacientes.IdPacientes,
                Nombre = pacientes.Nombre,
                Apellido = pacientes.Apellido
            };
            // Devolver el DTO con los valores deseados
            return Ok(loginResponseDTO);
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacientesDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Pacientes
                .Include(u => u.InformacionMedicaNavigation)
                .ThenInclude(im => im.TipajeNavigation)
                .Select(u => new PacientesDTO
                {
                    IdUsuarios = u.IdPacientes,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Cedula = u.Cedula,
                    Visible = u.Visibiliy,

                    NombreUsuario = u.NombreUsuario,
                    InformacionMedica = new InformacionMedicaDTO
                    {

                        Alergia = u.InformacionMedicaNavigation.Alergia,
                        NumeroSecundario = u.InformacionMedicaNavigation.NumeroSecundario,

                        TipajeSanguineo = new TipajesSanguineosDTO
                        {
                            TipoSanguineo = u.InformacionMedicaNavigation.TipajeNavigation.TipoSanguineo

                        }
                    },


                })
                .ToListAsync();
            return Ok(usuarios);



        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetUsuario(int id)
        {
            var usuario = await _context.Pacientes
                .Include(u => u.InformacionMedicaNavigation)
                .ThenInclude(im => im.TipajeNavigation)
                .Select(u => new PacientesDTO
                {
                    IdUsuarios = u.IdPacientes,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Cedula = u.Cedula,

                    NombreUsuario = u.NombreUsuario,
                    InformacionMedica = new InformacionMedicaDTO
                    {

                        Alergia = u.InformacionMedicaNavigation.Alergia,
                        NumeroSecundario = u.InformacionMedicaNavigation.NumeroSecundario,

                        TipajeSanguineo = new TipajesSanguineosDTO
                        {
                            TipoSanguineo = u.InformacionMedicaNavigation.TipajeNavigation.TipoSanguineo

                        }
                    },



                })

                .FirstOrDefaultAsync(e => e.IdUsuarios == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UpdateUsuariosDTO usuariosDTO)
        {
            if (id != usuariosDTO.IdUsuarios)
            {
                return BadRequest("El ID del usuario no coincide");
            }

            var usuario = await _context.Pacientes
                .Include(u => u.InformacionMedicaNavigation)
                .FirstOrDefaultAsync(u => u.IdPacientes == id);
            if (usuario == null)
            {
                return NotFound("Usuario no registrado");
            }
            usuario.Nombre = usuariosDTO.Nombre;
            usuario.Apellido = usuariosDTO.Apellido;
            usuario.Cedula = usuariosDTO.Cedula;
            usuario.Contraseña = usuariosDTO.Contraseña;
            usuario.NombreUsuario = usuariosDTO.NombreUsuario;
            usuario.Visibiliy = usuariosDTO.Visible;

            var informacionMedica = await _context.InformacionesMedicas
                .FirstOrDefaultAsync(im => im.IdinformacionesMedicas == usuario.InformacionMedica);

            if (informacionMedica != null)
            {
                informacionMedica.Alergia = usuariosDTO.Alergia;
                informacionMedica.NumeroSecundario = usuariosDTO.NumeroSecundario;
                var tipajeSanguineo = await _context.TipajesSanguineos
                    .FirstOrDefaultAsync(t => t.IdtipajesSanguineos == usuariosDTO.Tipaje);
                if (tipajeSanguineo == null)
                {
                    return BadRequest("El tipaje sanguineo no existe");
                }
                informacionMedica.Tipaje = tipajeSanguineo.IdtipajesSanguineos;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound("El usuario no existe");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }
        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paciente>> CreateUser(CreatePacienteDTO userDTO)
        {

            var tipajeSanguineo = await _context.TipajesSanguineos.FirstOrDefaultAsync(t => t.IdtipajesSanguineos == userDTO.Tipaje);
            if (tipajeSanguineo == null)
            {
                return BadRequest("El tipaje sanguineo no existe");
            }
            var informacionMedica = new InformacionesMedica
            {
                Alergia = userDTO.Alergia,
                NumeroSecundario = userDTO.NumeroSecundario,
                Tipaje = userDTO.Tipaje
            };
            _context.InformacionesMedicas.Add(informacionMedica);
            await _context.SaveChangesAsync();
            var paciente = new Paciente
            {
                Nombre = userDTO.Nombre,
                Apellido = userDTO.Apellido,
                Cedula = userDTO.Cedula,
                Contraseña = userDTO.Contraseña,
                NombreUsuario = userDTO.NombreUsuario,
                InformacionMedica = informacionMedica.IdinformacionesMedicas,

            };
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return Ok(paciente);

        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Pacientes.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Pacientes.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Pacientes.Any(e => e.IdPacientes == id);
        }
    }
}
