using System.ComponentModel.DataAnnotations;

namespace WebAPIAutores.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="El Nombre es requerido.")]
        public string Nombre { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
