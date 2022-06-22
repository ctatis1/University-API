using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Category: BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        //Relaciones con otras tablas
        public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
    }
}
