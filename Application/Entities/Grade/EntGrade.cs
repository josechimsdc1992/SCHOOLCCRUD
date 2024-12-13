using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.Teacher;

namespace Application.Entities.Grade
{
    public class EntGrade
    {
        public int IdGrade { get; set; }
        public string Name { get; set; }
        public int IdTeacher { get; set; }
        public EntTeacher Teacher { get; set; }
    }
}
