using System;
using System.Linq;
using Truextend.Test.StudentProject.Controller;

namespace Truextend.Test.StudentProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Application");

            if (args.Length < 2)
            {
                Console.WriteLine("Please enter the required parameters:");
                Console.WriteLine("Usage: Program <csv file name> <Student name>");
                Console.WriteLine("Usage: Program <csv file name> <Student type>");
                Console.WriteLine("Usage: Program <csv file name> <Student type> <Student Gender>");
                return;
            }

            try
            {
                StudentController controller = new StudentController(args.ToList());
                controller.Search();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
