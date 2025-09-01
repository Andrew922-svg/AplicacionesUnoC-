using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Universidad.Models;
using UniversidadDos.Dto;
using UniversidadDos.Models;

namespace UniversidadDos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UniversidadDosContext _context;

        public UsuarioController(UniversidadDosContext context)
        {
            _context = context;
        }
        [HttpGet("listar/estudiante")]
        public async Task<ActionResult<IEnumerable<Estudiante>>> ListarEstudiante()
        {
            var estudiante = await _context.Estudiantes
               .Select(e => new EstudiantesDto
               {
                   Nombre = e.Nombre,
                   Apellido = e.Apellido,
                   Correo = e.Correo,
                   FechaNacimiento = e.FechaNacimiento
               })
                .ToListAsync();
            return Ok(estudiante);
        }
        /*[HttpPost("guardar/estudiante")]
        public async Task<ActionResult<Estudiante>> GuardarEstudiante([FromBody] Estudiante estudiante)
        {

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, estudiante);
        }*/

        [HttpPost("crear/estudiante")]
        public async Task<ActionResult> CrearEstudiante([FromForm] AgregarEstuDto dto)
        {
            var existente = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Nombre == dto.Nombre && e.Apellido == dto.Apellido);
            if (existente != null)
            {
                return BadRequest(new { mesaje = "Estudiante ya registrado.", existente.Id, existente.Nombre });
            }

            var estudiante = new Estudiante
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                FechaNacimiento = dto.Fecha
            };

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Estudiante creado con éxito", estudiante.Id });
        }

        [HttpPut("actualiozar/estudiante/{id}")]
        public async Task<ActionResult> ActualizarEstudiante(int id, ActualizarEstuDto actualizarEstuDto)
        {
            var estudianteActualizado = await _context.Estudiantes.FindAsync(id);
            if (estudianteActualizado == null)
            {
                return NotFound();
            }

            estudianteActualizado.Nombre = actualizarEstuDto.Nombre;
            estudianteActualizado.Apellido = actualizarEstuDto.Apellido;
            estudianteActualizado.Correo = actualizarEstuDto.Correo;
            estudianteActualizado.FechaNacimiento = actualizarEstuDto.FechaNacimiento;

            await _context.SaveChangesAsync();
            return Ok(estudianteActualizado);
        }

        //[Authorize(Roles = "Administrador")]
        [HttpDelete("eliminar/estudiante/{id}")]
        public async Task<ActionResult> eliminarEstidiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound(new { mensaje = "Estudiante no encontrado" });
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("listar/materias")]
        public async Task<ActionResult<IEnumerable<Materia>>> ListarMaterias()
        {
            var materia = await _context.Materias.
                Select(m => new ListarMaterDto
                {
                    Materia = m.NombreMateria
                })
                .ToListAsync();

            return Ok(materia);
        }

        [HttpPost("crar/materia")]
        public async Task<ActionResult<Materia>> CrearMate([FromForm] AgregarMateDto crearMateDto)
        {
            var mateExi = await _context.Materias.FirstOrDefaultAsync(m => m.NombreMateria == crearMateDto.NombreMaterias);
            if (mateExi != null)
            {
                return BadRequest(new { mesaje = "Materia ya registrada, porfavor ingrese una nueva", mateExi.NombreMateria });
            }

            var materia = new Materia { NombreMateria = crearMateDto.NombreMaterias };
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, materia);

        }

        [HttpPut("actualizar/materia/{nombre}")]
        public async Task<ActionResult> ActualizarMateria(string nombre, [FromForm] ActualizarMateDto actualizarMateDto)
        {
            var actualizarMateria = await _context.Materias.FirstOrDefaultAsync(m => m.NombreMateria == nombre);
            if (actualizarMateria == null)
            {
                return NotFound(new { mensaje = "Materia no encontrad." });
            }
            actualizarMateria.NombreMateria = actualizarMateDto.NuevaMateria;
            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Materia actualizada", actualizarMateria.NombreMateria });
        }

        [HttpDelete("eliminar/materia/{nombre}")]
        public async Task<ActionResult> EliminarMateria(string nombre)
        {
            var EliminarMateria = await _context.Materias.FirstOrDefaultAsync(m => m.NombreMateria == nombre);
            if (EliminarMateria == null)
            {
                return NotFound(new { mensaje = "Materia no encontrada" });
            }
            _context.Materias.Remove(EliminarMateria);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Materia eliminada", EliminarMateria.NombreMateria });
        }

        [HttpGet("listar/notas")]
        public async Task<ActionResult<IEnumerable<EstudianteMaterium>>> ListarNotas()
        {
            var listarnotas = await _context.EstudianteMateria
                .Include(em => em.Estu)
                .Include(em => em.Mate)
                .Select(em => new ListarNotaDto
                {

                    NombreEstu = em.Estu.Nombre + " " + em.Estu.Apellido,
                    NombreMate = em.Mate.NombreMateria,
                    Califi1 = em.Nota1,
                    Califi2 = em.Nota2,
                    Califi3 = em.Nota3
                }).ToListAsync();
            ;
            return Ok(listarnotas);
        }

        [HttpPost("agregar/notas")]

        public async Task<ActionResult> AgregarNotas([FromForm] AgregarNotas agregarNotas)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => (e.Nombre + " " + e.Apellido) == agregarNotas.NombreEstudiante);
            if (estudiante == null)
            {
                return NotFound($"Estudiante no encontrado {agregarNotas.NombreEstudiante}");
            }

            var materia = await _context.Materias.FirstOrDefaultAsync(m => (m.NombreMateria + " ") == agregarNotas.NombreMateria);
            if (materia == null)
            {
                return NotFound($"Materia no encontrada {agregarNotas.NombreMateria}");
            }

            var estuMate = await _context.EstudianteMateria.FirstOrDefaultAsync(em => em.EstuId == estudiante.Id && em.MateId == materia.Id);
            if (estuMate == null)
            {
                return NotFound($"El estudiante {agregarNotas.NombreEstudiante} no ha inscrito en la materia {agregarNotas.NombreMateria}");
            }

            estuMate.Nota1 = agregarNotas.CalificacionUno;
            estuMate.Nota2 = agregarNotas.CalificacionDos;
            estuMate.Nota3 = agregarNotas.CalificacionTres;
            await _context.SaveChangesAsync();
            return Ok("Notas registradas");
        }

        [HttpPut("actualizar/notas")]

        public async Task<ActionResult> ActualizarNotas([FromForm] ActualizarNotasDto actualizarNotasDto)
        {
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => (e.Nombre + " " + e.Apellido) == actualizarNotasDto.NombreEstudiante);
                    if (estudiante == null)
                        return NotFound($"Estudiante no encontrado {actualizarNotasDto.NombreEstudiante}");

            var materia = await _context.Materias
                .FirstOrDefaultAsync(m => m.NombreMateria == actualizarNotasDto.NombreMateria);
                    if (materia == null)
                        return NotFound($"Materia no encontrada {actualizarNotasDto.NombreMateria}");

            var estuMate = await _context.EstudianteMateria
                .FirstOrDefaultAsync(em => em.EstuId == estudiante.Id && em.MateId == materia.Id);
                    if (estuMate == null)
                        return NotFound($"El estudiante {actualizarNotasDto.NombreEstudiante} no está inscrito en {actualizarNotasDto.NombreMateria}");

            estuMate.Nota1 = actualizarNotasDto.CalificacionUno;
            estuMate.Nota2 = actualizarNotasDto.CalificacionDos;
            estuMate.Nota3 = actualizarNotasDto.CalificacionTres;

            await _context.SaveChangesAsync();
            return Ok("Notas actualizadas correctamente");
        }

        [HttpDelete("eliminar/notas")]

        public async Task<ActionResult> EliminarNotas([FromForm] EliminarNotasDto eliminarNotasDto)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => (e.Nombre + " " + e.Apellido) == eliminarNotasDto.NombreEstudiante);
            if (estudiante == null)
            {
                return NotFound($"Estudiante no encontrado {eliminarNotasDto.NombreEstudiante}");
            }
            
            var materia = await _context.Materias.FirstOrDefaultAsync(m => m.NombreMateria == eliminarNotasDto.NombreMateria);
            if (materia == null)
            {
                return NotFound($"Materia no encontrada {eliminarNotasDto.NombreMateria}"); 
            }

            var estuMate = await _context.EstudianteMateria
                .FirstOrDefaultAsync(em => em.EstuId == estudiante.Id && em.MateId == materia.Id);
            if (estuMate == null)
                return NotFound($"El estudiante {eliminarNotasDto.NombreEstudiante} no está inscrito en {eliminarNotasDto.NombreMateria}");

            
            estuMate.Nota1 = 0;
            estuMate.Nota2 = 0;
            estuMate.Nota3 = 0;

            await _context.SaveChangesAsync();
            return Ok("Notas eliminadas (reiniciadas a 0) correctamente");

        }
    }
}