using Almacen.DTOs;
using Almacen.Entities;
using Almacen.Migrations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Almacen.Controllers
{
    [Authorize]
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var usuario = await context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Responsable)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UsuarioDTO>(usuario));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<UsuarioDTO>>> GetAll()
        {
            var usuarios = await context.Usuarios
                 .Include(i => i.Rol)
                .Include(i => i.Responsable)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!usuarios.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<UsuarioDTO>>(usuarios));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(UsuarioDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var usuario = mapper.Map<Usuario>(dto);
                usuario.Rol = await context.Rols.SingleOrDefaultAsync(r => r.Id == dto.Rol.Id);
                usuario.ResponsableId = null;
                usuario.Responsable = null;


                if (dto.Rol.Id == 2)
                {

                    if (await context.Usuarios.AnyAsync(c => c.Responsable.Id == dto.Responsable.Id))
                    {
                        return Conflict();
                    }

                    usuario.Responsable = await context.Responsables.SingleOrDefaultAsync(o => o.Id == dto.Responsable.Id);
                }
            
                context.Add(usuario);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al guardar al usuario.", details = ex.Message });
            }
        }

        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] UsuarioDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var usuario = await context.Usuarios.FindAsync(id);
            var currentResponsableId = usuario?.ResponsableId;

            if (usuario == null)
            {
                return NotFound();
            }

            mapper.Map(dto, usuario);
            usuario.Rol = await context.Rols.SingleOrDefaultAsync(r => r.Id == dto.Rol.Id);
            usuario.ResponsableId = null;
            usuario.Responsable = null;

            if (dto.Rol.Id == 2)
            {
                if (currentResponsableId != null && currentResponsableId != dto.Responsable.Id)
                {
                    var existsUserResponsable = await context.Usuarios.AnyAsync(c => c.Responsable.Id == dto.Responsable.Id);
                    if (existsUserResponsable)
                    {
                        return Conflict();
                    }
                }

                usuario.Responsable = await context.Responsables.SingleOrDefaultAsync(o => o.Id == dto.Responsable.Id);
                usuario.ResponsableId = usuario.Responsable.Id;
            }

            context.Update(usuario);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        private bool UsuarioExists(int id)
        {
            return context.Usuarios.Any(e => e.Id == id);
        }
    }
}
