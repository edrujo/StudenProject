using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
