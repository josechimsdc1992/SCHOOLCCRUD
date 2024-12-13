using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StudentGrade
    {
        public int IdStudentGrade { get; set; }
        public int IdGrade { get; set; }
        public int IdStudent { get; set; }
        public string Grupo { get; set; }
    }
}
