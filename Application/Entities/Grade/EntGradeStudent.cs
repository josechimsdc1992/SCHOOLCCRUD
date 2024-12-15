using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.Student;

using AutoMapper;

namespace Application.Entities.Grade
{
    public class EntGradeStudent
    {
        public int IdStudentGrade { get; set; }
        public int IdGrade { get; set; }
        public int IdStudent { get; set; }
        public string Grupo { get; set; }

        public EntStudent Student { get; set; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.StudentGrade, EntGradeStudent>();
                CreateMap<EntGradeStudent, Domain.Entities.StudentGrade>();
            }
        }
    }
}
