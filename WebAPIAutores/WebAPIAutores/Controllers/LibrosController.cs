using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Data;
using WebAPIAutores.Models;

namespace WebAPIAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await _context.Libro.Include(p => p.Autor).FirstOrDefaultAsync(p => p.Id == id);
        }
        [HttpGet]
        public async Task<ActionResult<List<Libro>>> GetAll()
        {
            return await _context.Libro.Include(p => p.Autor).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            //Comment
            var existeAutor = await _context.Autor.FirstOrDefaultAsync(x => x.Id == libro.AutorId);
            if (existeAutor != null)
            {
                libro.Autor = existeAutor;
                _context.Libro.Add(libro);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}
