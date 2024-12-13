using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Grade
    {
        public int IdGrade { get; set; }
        public string Name { get; set; }
        public int IdTeacher { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<StudentGrade> StudentGrades { get; set; }
    }
}
