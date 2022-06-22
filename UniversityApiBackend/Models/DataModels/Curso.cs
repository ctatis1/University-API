using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }
    public class Curso: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public Level Level { get; set; } = Level.Basic;

        //Relaciones con otras tablas
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();
        [Required]
        public Chapter Chapter { get; set; } = new Chapter();
    }
}
