using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsByCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();
    }
}
