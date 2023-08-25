using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPIAutores.Data;
using WebAPIAutores.Models;

namespace WebAPIAutores.Controllers
{
    //Permite hacer valiciones automáticas, respecto a la data recibida
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AutoresController(ApplicationDbContext dbContext)
        {
            _context = dbContext;

        }
        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await _context.Autor.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            try
            {
                _context.Autor.Add(autor);
                await _context.SaveChangesAsync();
                return Ok();
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
