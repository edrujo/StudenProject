using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Truextend.Test.StudentProject.Models;

namespace Truextend.Test.StudentProject.Adapters
{
    public class CSVStudentAdapter : IStudentAdapter
    {

        public static List<Student> Students { get; set; }
        private string csvFileName;

        public CSVStudentAdapter(string csvFile)
        {
            Students = new List<Student>();
            this.csvFileName = csvFile;
            InitializeCollection();
        }

        public Student Create()
        {
            return new Student();
        }

        public Student Create(string type, string name, string gender, string lastUpdate)
        {
            return new Student(type, name, gender, lastUpdate);
        }

        public void Store(Student newStudent)
        {
            Students.Add(newStudent);
        }

        public void Delete(Student currentStudent)
        {
            Students.Remove(currentStudent);
        }

        public List<Student> GetStudents()
        {
            return Students.OrderBy(s => s.Name).ToList();
        }
        public List<Student> GetStudentByName(string name)
        {
            return Students.Where(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Student> GetStudentsByStudentType(string type)
        {
            return Students.Where(s => s.Type.Equals(type, StringComparison.OrdinalIgnoreCase)).OrderByDescending(s => s.LastUpdate).ToList();
        }
        public List<Student> GetStudentsByGenderAndType(string type, string gender)
        {
            return Students.Where(s => s.Type.Equals(type, StringComparison.OrdinalIgnoreCase) && s.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase)).OrderByDescending(s => s.LastUpdate).ToList();
        }

        private void InitializeCollection()
        {
            string csvContent = File.ReadAllText(csvFileName);

            foreach (string csvRow in csvContent.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {
                    Student newStudent = Create();
                    int i = 0;
                    foreach (var cell in csvRow.Split(','))
                    {
                        switch (i)
                        {
                            case 0:
                                newStudent.Type = cell;
                                break;
                            case 1:
                                newStudent.Name = cell;
                                break;
                            case 2:
                                newStudent.Gender = cell;
                                break;
                            case 3:
                                newStudent.LastUpdate = DateTime.ParseExact(cell.Trim(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                                break;
                        }
                        i++;
                    }

                    Store(newStudent);
                }
            }
        }
    }
}
