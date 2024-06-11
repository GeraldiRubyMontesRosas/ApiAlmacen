using Almacen.DTOs;
using Almacen.Entities;
using Almacen.Migrations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Almacen.Controllers
{
    [Route("api/responsable")]
    [ApiController]
    public class ResponsablesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ResponsablesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<ResponsableDTO>> GetById(int id)
        {
            var responsable = await context.Responsables
                 .FirstOrDefaultAsync(b => b.Id == id);

            if (responsable == null)
            {
                return NotFound();
            }

            var responsableDTO = mapper.Map<ResponsableDTO>(responsable);
            return Ok(responsableDTO);
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<ResponsableDTO>>> GetAll()
        {
            var responsable = await context.Responsables
                .ToListAsync();

            if (!responsable.Any())
            {
                return NotFound();
            }

            var operadoresDTO = mapper.Map<List<ResponsableDTO>>(responsable);
            return Ok(operadoresDTO);
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(ResponsableDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existeResponsable = await context.Responsables.AnyAsync(n => n.Nombres == dto.Nombres &&
                                                                  n.ApellidoPaterno == dto.ApellidoPaterno &&
                                                                  n.ApellidoMaterno == dto.ApellidoMaterno);
            if (existeResponsable)
            {
                return Conflict();
            }
           
            

            var responsables = mapper.Map<Responsable>(dto);
            string nombreCompleto = User.FindFirst("nombreCompleto")?.Value;
            responsables.UsuarioCreacionNombre = nombreCompleto;
            // Incluir la entidad en el contexto
            context.Add(responsables);

            try
            {
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var responsable = await context.Responsables.FindAsync(id);

            if (responsable == null)
            {
                return NotFound();
            }

            context.Responsables.Remove(responsable);
            await context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ResponsableDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var responsable = await context.Responsables.FindAsync(id);

            if (responsable == null)
            {
                return NotFound();
            }

            mapper.Map(dto, responsable);

            context.Update(responsable);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsableExiste(id))
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

        private bool ResponsableExiste(int id)
        {
            return context.Responsables.Any(e => e.Id == id);
        }
    }
}
