using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIAutores.Models
{
    public class Autor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es requerido.")]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo {0} no debe de tener más de {1} caracteres")]
        public string Nombre { get; set; }
        [Range(18, 120)]
        [NotMapped]
        public int Edad { get; set; }
        [CreditCard]
        [NotMapped]
        public string TarjetaCredito { get; set; }
        [Url]
        [NotMapped]
        public string Url { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
