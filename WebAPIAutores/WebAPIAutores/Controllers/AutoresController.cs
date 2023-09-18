using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPIAutores.Data;
using WebAPIAutores.Filters;
using WebAPIAutores.Models;
using WebAPIAutores.Services;

namespace WebAPIAutores.Controllers
{
    //Permite hacer valiciones automáticas, respecto a la data recibida
    [ApiController]
    //Todos los endpoints están protegidos
    //[Authorize]
    //En el mundo real podemos ver lo siguiente: Es como un placeholder o variable que pondría el nombre del controlador
    //[Route("api/[controller]")]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IService _service;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSingleton servicioSingleton;

        public AutoresController(ApplicationDbContext dbContext, IService service,
            ServicioTransient servicioTransient,
            ServicioScoped servicioScoped,
            ServicioSingleton servicioSingleton)
        {
            _context = dbContext;
            _service = service;
            this.servicioTransient = servicioTransient;
            this.servicioScoped = servicioScoped;
            this.servicioSingleton = servicioSingleton;
        }
        [HttpGet]             //api/autores
        [HttpGet("listado")] //api/autores/listado, el endpoint puede tener dos controladores
        [HttpGet("/listado")] // listado
        [ServiceFilter(typeof(MyActionFilter))]
        //[ResponseCache(Duration = 10)]
        //[Authorize]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            _service.RealizarTarea();
            return await _context.Autor.ToListAsync();
        }
        [HttpGet("first")]
        public async Task<Autor> GetFirst([FromHeader] int miValor, [FromQuery] string nombre)
        {
            return await _context.Autor.FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        [HttpGet("{id:int}/{param?}")]
        //[HttpGet("{id:int}/{param)persona")]
        public async Task<ActionResult<Autor>> Get(int id, string param)
        {
            var autor = await _context.Autor.FirstOrDefaultAsync(p => p.Id == id);
            if (autor == null)
            {
                //El tipo de dato de NotFound, hereda de ActionResult entonces podemos regresar este tipo de dato
                return NotFound();
            }
            return autor;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get([FromRoute] string nombre)
        {
            var autor = await _context.Autor.FirstOrDefaultAsync(p => p.Nombre == nombre);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }
        [HttpGet("GUID")]
        //[ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult ObtenerGuids()
        {
            return Ok(new
            {
                AutoresControllerTransient = servicioTransient.Guid,
                servicioTransient = servicioTransient.Guid,

                AutoresControllerScoped = servicioScoped.Guid,
                servicioScoped = servicioScoped.Guid,

                AutoresControlleSingleton = servicioSingleton.Guid,
                servicioSingleton = servicioSingleton.Guid,
            });
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            try
            {
                var autorExist = await _context.Autor.AnyAsync(x => x.Nombre.ToUpper() == autor.Nombre.ToUpper());
                if (!autorExist)
                {
                    _context.Autor.Add(autor);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                // It automatically creates a BadRequestObjectResult with the specified error message as its content.
                return BadRequest("El autor ya existe");

                //This allows you to have more control over the response, such as setting additional HTTP headers or response status codes, if needed
                //return new BadRequestObjectResult("El autor ya existe");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(Autor autor, int id)
        {
            try
            {
                if (autor.Id != id)
                {
                    return BadRequest();
                }
                _context.Autor.Update(autor);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPut]
        //public async Task<ActionResult> Update(Autor autor)
        //{
        //    try
        //    {
        //        _context.Autor.Update(autor);
        //        await _context.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var autor = await _context.Autor.FirstOrDefaultAsync(i => i.Id == id);
                if (autor != null)
                {
                    _context.Autor.Remove(autor);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
