using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truextend.Test.StudentProject.Models
{
    public class Student
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime LastUpdate { get; set; }

        public Student()
        {

        }

        public Student(string type, string name, string gender, string lastUpdate)
        {
            this.Type = type;
            this.Name = name;
            this.Gender = gender;
            this.LastUpdate = DateTime.ParseExact(lastUpdate.Trim(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }

    }
}
