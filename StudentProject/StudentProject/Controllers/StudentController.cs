using System;
using System.Collections.Generic;
using System.Linq;
using Truextend.Test.StudentProject.Adapters;
using Truextend.Test.StudentProject.BLL;
using Truextend.Test.StudentProject.Models;

namespace Truextend.Test.StudentProject.Controller
{
    public class StudentController
    {

        private string csvFileName;
        private string nameFilter;
        private string typeFilter;
        private string genderFilter;

        private static StudentBLL studentLogic;

        public StudentController(List<string> parameters)
        {
            ValidateParameters(parameters);
            studentLogic = new StudentBLL(new CSVStudentAdapter(this.csvFileName));
        }

        public void Search()
        {
            List<Student> result = new List<Student>();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                result = studentLogic.GetStudentByName(nameFilter);
            }
            else if (!string.IsNullOrEmpty(typeFilter))
            {
                if (!string.IsNullOrEmpty(genderFilter)) {
                    result = studentLogic.GetStudentsByGenderAndType(typeFilter, genderFilter);
                } else {
                    result = studentLogic.GetStudentsByStudentType(typeFilter);
                }
                
            }

            // Displaying Students
            DisplayStudents(result);
        }

        private void DisplayStudents(List<Student> students)
        {
            if (!students.Any())
            {
                Console.WriteLine("/////////// RESULTS NOT FOUND ");
            }

            foreach (var currentStudent in students)
            {
                Console.WriteLine("/////////// STUDENT ");
                Console.WriteLine(string.Format("/////////// Type: {0}", currentStudent.Type));
                Console.WriteLine(string.Format("/////////// Name: {0}", currentStudent.Name));
                Console.WriteLine(string.Format("/////////// Gender: {0}", currentStudent.Gender));
                Console.WriteLine(string.Format("/////////// Last Update: {0}", currentStudent.LastUpdate));
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
            if (string.IsNullOrEmpty(filter))
            {
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
                this.csvFileName = csvFileName;
                return true;
            }

            return false;
        }
    }
}
