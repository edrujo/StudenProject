using System.Collections.Generic;
using Truextend.Test.StudentProject.Adapters;
using Truextend.Test.StudentProject.Models;

namespace Truextend.Test.StudentProject.BLL
{
    public class StudentBLL
    {
        private IStudentAdapter StudentAdapter { get; set; }

        public StudentBLL(IStudentAdapter adapter)
        {
            this.StudentAdapter = adapter;
        }

        public List<Student> GetStudents()
        {
            return StudentAdapter.GetStudents();
        }

        public List<Student> GetStudentByName(string name)
        {
            return StudentAdapter.GetStudentByName(name);
        }

        public List<Student> GetStudentsByStudentType(string type)
        {
            return StudentAdapter.GetStudentsByStudentType(type);
        }

        public List<Student> GetStudentsByGenderAndType(string type, string gender)
        {
            if (gender.ToLower().StartsWith("f"))
            {
                gender = "F";
            }
            else if (gender.ToLower().StartsWith("m"))
            {
                gender = "M";
            }

            return StudentAdapter.GetStudentsByGenderAndType(type, gender);
        }

    }
}
