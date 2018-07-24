using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Truextend.Test.StudentProject.Models;

namespace Truextend.Test.StudentProject.Controller
{
    public class StudentController
    {

        public List<Student> Students { get; set; }

        private string nameFilter;
        private string typeFilter;
        private string genderFilter;

        public StudentController(List<string> parameters)
        {
            this.Students = new List<Student>();
            ValidateParameters(parameters);
            ReadCSVFile(parameters[0]);
        }

        public void Search()
        {
            List<Student> result = new List<Student>();

            if (string.IsNullOrEmpty(nameFilter))
            {
                result = Students.Where(s => s.Name.Contains(nameFilter)).OrderBy(s => s.Name).ToList();
            }

            // Displaying Students
            DisplayStudents(result);
            
        }

        private void DisplayStudents(List<Student> students)
        {
            foreach (var currentStudent in students)
            {
                Console.WriteLine("/////////// STUDENT ");
                Console.WriteLine(string.Format("/////////// Type: {0}", currentStudent.Type));
                Console.WriteLine(string.Format("/////////// Name: {0}", currentStudent.Name));
                Console.WriteLine(string.Format("/////////// Gender: {0}", currentStudent.Gender));
                Console.WriteLine(string.Format("/////////// Last Update: {0}", currentStudent.LastUpdate));
            }
        }

        private void ReadCSVFile(string csvFile)
        {
            string csvContent = File.ReadAllText("test.csv");
            
            foreach (string csvRow in csvContent.Split('\n'))
            {
                if (!string.IsNullOrEmpty(csvRow))
                {
                    var csvCells = csvRow.Split(',');
                    Student newStudent = new Student();
                    int i = 0;
                    foreach (var cell in csvRow.Split(','))
                    {
                        switch (i) {
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
                                newStudent.LastUpdate = DateTime.Parse(cell);
                                break;
                        }
                        i++;
                    }

                    Students.Add(newStudent);
                }
            }
        }

        private void ValidateParameters(List<string> parameters)
        {
            var csvFileName = parameters[0];
            var firstFilter = parameters[1];
            string secondFilter = string.Empty;

            if (parameters.Count > 2)
            {
                secondFilter = parameters[2];
            }

            if (!IsCsvFileValid(csvFileName))
            {
                throw new Exception("CSV file name invalid");
            }

            if (!IsFilterValid(firstFilter) || !IsFilterValid(secondFilter))
            {
                throw new Exception("Invalid Parameters");
            }
        }

        private bool IsFilterValid(string filter)
        {
            if (string.IsNullOrEmpty(filter)) {
                return true;
            }

            var filterSplitted = filter.Split('=');

            if (filterSplitted.Length == 2)
            {
                if (filterSplitted[0].ToLower() == "name")
                {
                    nameFilter = filterSplitted[1];
                    return true;
                }
                else if (filterSplitted[0].ToLower() == "type")
                {
                    typeFilter = filterSplitted[1];
                    return true;
                }
                else if (filterSplitted[0].ToLower() == "gender")
                {
                    genderFilter = filterSplitted[1];
                    return true;
                }
            }

            return false;
        }

        private bool IsCsvFileValid(string csvFileName)
        {
            var fileNameSplitted = csvFileName.Split('.');
            if (fileNameSplitted.Length == 2 && fileNameSplitted[1] == "csv")
            {
                return true;
            }

            return false;
        }
    }
}
