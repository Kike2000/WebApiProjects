using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Models;

namespace WebAPIAutores.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Creación de tabla.
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Libro> Libro { get; set; }

    }
}
