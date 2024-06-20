using Almacen.DTOs;
using Almacen.Entities;
using Almacen.Migrations;
using Almacen.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Almacen.Controllers
{
    [Authorize]
    [Route("api/traslado")]
    [ApiController]
    public class TrasladosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<TrasladosController> _logger;

        public TrasladosController(
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<TrasladosController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            _logger = logger;
        }

        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<TrasladoDTO>> GetById(int id)
        {
            var traslado = await context.Traslados
                 .Include(g => g.Inmueble)
                 .Include(g => g.AreaOrigen)
                 .Include(g => g.Usuario)
                 .Include(g => g.AreaDestino)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (traslado == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TrasladoDTO>(traslado));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult<List<TrasladoDTO>>> GetAll()
        {

            var area = await context.Traslados
                .Include(g => g.Inmueble)
                 .Include(g => g.AreaOrigen)
                 .Include(g => g.Usuario)
                 .Include(g => g.AreaDestino)
                .OrderBy(u => u.Id)
                .ToListAsync();

            if (!area.Any())
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<TrasladoDTO>>(area));
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(TrasladoDTO dto)
        {
            // Validación del modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int usuarioId = -1;
            // Mapeo del DTO a la entidad

            var traslado = mapper.Map<Traslado>(dto);
            traslado.FechaHoraCreacion = DateTime.Now;
        
            var usuarioIdClaim = User.FindFirst("usuarioId");
            if (usuarioIdClaim != null && int.TryParse(usuarioIdClaim.Value, out usuarioId))
            {
                _logger.LogInformation($"ID de usuario obtenido correctamente: {usuarioId}");
            }
            else
            {
                _logger.LogWarning("No se pudo obtener correctamente el ID de usuario.");
            }
            _logger.LogInformation($"ID de usuario obtenido: {usuarioId}");

            traslado.Usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);

            traslado.AreaDestino = await context.Areas.SingleOrDefaultAsync(b => b.Id == dto.AreaDestino.Id);
            if (traslado.AreaDestino == null)
            {
                return BadRequest("Área no encontrada.");
            }
            traslado.AreaOrigen = await context.Areas.SingleOrDefaultAsync(b => b.Id == dto.AreaOrigen.Id);
            if (traslado.AreaOrigen == null)
            {
                return BadRequest("Área no encontrada.");
            }
            traslado.Inmueble = await context.Inmuebles.SingleOrDefaultAsync(b => b.Id == dto.Inmueble.Id);
            if (traslado.Inmueble == null)
            {
                return BadRequest("Inmuble no encontrado.");
            }
           


            // Incluir la entidad en el contexto
            context.Add(traslado);

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
            var traslado = await context.Traslados.FindAsync(id);

            if (traslado == null)
            {
                return NotFound();
            }

            context.Traslados.Remove(traslado);
            await context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, TrasladoDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var traslado = await context.Traslados.FindAsync(id);

            if (traslado == null)
            {
                return NotFound();
            }

            int usuarioId = -1;
            mapper.Map(dto, traslado);
            traslado.FechaHoraCreacion = DateTime.Now;
            traslado.AreaOrigen = await context.Areas.SingleOrDefaultAsync(c => c.Id == dto.AreaOrigen.Id);
            traslado.AreaDestino = await context.Areas.SingleOrDefaultAsync(c => c.Id == dto.AreaDestino.Id);
            var usuarioIdClaim = User.FindFirst("usuarioId");
            if (usuarioIdClaim != null && int.TryParse(usuarioIdClaim.Value, out usuarioId))
            {
                _logger.LogInformation($"ID de usuario obtenido correctamente: {usuarioId}");
            }
            else
            {
                _logger.LogWarning("No se pudo obtener correctamente el ID de usuario.");
            }
            _logger.LogInformation($"ID de usuario obtenido: {usuarioId}");
            traslado.Inmueble = await context.Inmuebles.SingleOrDefaultAsync(c => c.Id == dto.Inmueble.Id);
            context.Update(traslado);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrasladoExiste(id))
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
        private bool TrasladoExiste(int id)
        {
            return context.Inmuebles.Any(e => e.Id == id);
        }
    }
}
