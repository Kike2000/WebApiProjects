using WebAPIAutores.Validations;

namespace WebAPIAutores.Models
{
    public class Libro
    {
        public int Id { get; set; }
        [FirtsLetterCapitalAttribute]
        public string Nombre { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
