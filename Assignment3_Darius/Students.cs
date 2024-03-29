using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_Darius
{
    internal class Students
    {
        public int ID { get; set; } 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age {  get; set; }

        public string Gender { get; set; }

        public string ClassName { get; set; }

        public double Grade {  get; set; }


        public override string ToString()
        {
            return ID + FirstName + LastName + Age + Gender + ClassName + Grade;
        }

    }
}
