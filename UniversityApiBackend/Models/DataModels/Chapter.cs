using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Chapter: BaseEntity
    {
        [Required]
        public string List = string.Empty;

        public int CursoID { get; set; }    
        public virtual Curso Curso { get; set; } = new Curso();
    }
}
