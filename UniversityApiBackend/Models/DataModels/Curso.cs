using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Curso: BaseEntity
    {
        [Required, StringLength(280)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string PublicoObjetivo { get; set; } = string.Empty;
        [Required]
        public string Objetivos { get; set; } = string.Empty;
        [Required]
        public string Requisitos { get; set; } = string.Empty;
        [Required]
        public string Nivel
        {
            get
            {
                return Nivel;
            }
            set
            {
                if ((value == "Basico") || (value == "Medio") || (value == "Avanzado"))
                {
                    Nivel = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}
