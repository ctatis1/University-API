using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.DataAccess
{
    public class UniversityDBContext: DbContext
    {  
        //constructor
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options):base(options)
        {

        }

        //Add DBsets (tables of DB)
        public DbSet<User>? Users { get; set; }
        public DbSet<Curso>? Cursos { get; set; }
    }
}
