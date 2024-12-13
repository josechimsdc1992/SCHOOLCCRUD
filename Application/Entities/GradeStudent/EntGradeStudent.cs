using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.Grade;
using Application.Entities.Student;

namespace Application.Entities.GradeStudent
{
    public class EntGradeStudent
    {
        public int IdStudentGrade { get; set; }
        public int IdGrade { get; set; }
        public int IdStudent { get; set; }
        public string Grupo { get; set; }

        public EntStudent Student { get; set; }
        public EntGrade Grade { get; set; }
    }
}
