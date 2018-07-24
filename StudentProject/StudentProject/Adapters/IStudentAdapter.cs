using System.Collections.Generic;
using Truextend.Test.StudentProject.Models;

namespace Truextend.Test.StudentProject.Adapters
{
    public interface IStudentAdapter : IAdapter<Student>
    {
        List<Student> GetStudents();
        List<Student> GetStudentsByStudentType(string type);
        List<Student> GetStudentsByGenderAndType(string type, string gender);
    }
}