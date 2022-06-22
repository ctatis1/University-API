using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Student : BaseEntity
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime DoB { get; set; }

        //Relaciones con otras tablas
        [Required]
        public ICollection<Curso> Cursos { get; set; } = new List<Curso>(); 
    }
}
