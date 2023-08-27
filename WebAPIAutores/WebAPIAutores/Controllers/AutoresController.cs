﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPIAutores.Data;
using WebAPIAutores.Models;

namespace WebAPIAutores.Controllers
{
    //Permite hacer valiciones automáticas, respecto a la data recibida
    [ApiController]

    //En el mundo real podemos ver lo siguiente: Es como un placeholder o variable que pondría el nombre del controlador
    //[Route("api/[controller]")]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AutoresController(ApplicationDbContext dbContext)
        {
            _context = dbContext;

        }
        [HttpGet]             //api/autores
        [HttpGet("listado")] //api/autores/listado, el endpoint puede tener dos controladores
        [HttpGet("/listado")] // listado
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await _context.Autor.ToListAsync();
        }
        [HttpGet("first")]
        public async Task<Autor> GetFirst()
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
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await _context.Autor.FirstOrDefaultAsync(p => p.Nombre == nombre);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
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
