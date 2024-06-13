using Almacen.DTOs;
using Almacen.Entities;
using Almacen.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Almacen.Controllers
{
    [Route("api/inmuebles")]
    [ApiController]
    public class InmueblesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorImagenes almacenadorImagenes;
        private readonly string directorioInmuebles = "inmuebles";
        private readonly string directorioQr = "Qr";

        public InmueblesController(
                   ApplicationDbContext context,
                   IMapper mapper,
                   IAlmacenadorImagenes almacenadorImagenes)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorImagenes = almacenadorImagenes;
        }
        [HttpGet("obtener-por-id/{id:int}")]
        public async Task<ActionResult<InmuebleDTO>> GetById(int id)
        {
            var inmueble = await context.Inmuebles
                .Include(g => g.Area)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (inmueble == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<InmuebleDTO>(inmueble));
        }
    
        [HttpGet("obtener-por-codigo/{codigo}")]
        public async Task<ActionResult<InmuebleDTO>> GetBycodigo(string codigo)
        {
            var inmueble = await context.Inmuebles
                .Include(g => g.Area)
                .FirstOrDefaultAsync(b => b.Codigo == codigo);

            if (inmueble == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<InmuebleDTO>(inmueble));
        }

        [HttpGet("obtener-todos")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var inmuebles = await context.Inmuebles
                    .Include(t => t.Area)
                    .ToListAsync();

                if (!inmuebles.Any())
                {
                    return NotFound();
                }

                return Ok(mapper.Map<List<InmuebleDTO>>(inmuebles));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del inmuebles ", details = ex.Message });
            }
        }

        [HttpPost("crear")]
        public async Task<ActionResult> Post(InmuebleDTO dto)
        {
           

            try
            {
                if (!string.IsNullOrEmpty(dto.ImagenBase64))
                {
                    dto.Imagen = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioInmuebles);
                }

                if (!string.IsNullOrEmpty(dto.QrBase64))
                {
                    dto.Qr = await almacenadorImagenes.GuardarImagen(dto.QrBase64, directorioQr);
                }

                var inmueble = mapper.Map<Inmueble>(dto);

                inmueble.Area = await context.Areas.SingleOrDefaultAsync(b => b.Id == dto.Area.Id);
                if (inmueble.Area == null)
                {
                    return BadRequest("Área no encontrada.");
                }

                context.Inmuebles.Add(inmueble);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al crear inmueble", details = ex.Message });
            }
        }


        [HttpDelete("eliminar/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var inmueble = await context.Inmuebles.FindAsync(id);

            if (inmueble == null)
            {
                return NotFound();
            }

            context.Inmuebles.Remove(inmueble);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("actualizar/{id:int}")]
        public async Task<ActionResult> Put(int id, InmuebleDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("El ID de la ruta y el ID del objeto no coinciden");
            }

            var inmueble = await context.Inmuebles.FindAsync(id);

            if (inmueble == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(dto.ImagenBase64))
            {
                dto.Imagen = await almacenadorImagenes.GuardarImagen(dto.ImagenBase64, directorioInmuebles);
            }
            else
            {
                dto.Imagen = inmueble.Imagen;
            }


            if (!string.IsNullOrEmpty(dto.QrBase64))
            {

                dto.Qr = await almacenadorImagenes.GuardarImagen(dto.QrBase64, directorioQr);
            }
            else
            {
                dto.Qr = inmueble.Qr;
            }

            mapper.Map(dto, inmueble);
            inmueble.Area = await context.Areas.SingleOrDefaultAsync(c => c.Id == dto.Area.Id);

            context.Update(inmueble);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InmuebleExiste(id))
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

        private bool InmuebleExiste(int id)
        {
            return context.Inmuebles.Any(e => e.Id == id);
        }

    }
}
