using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.Teacher;

using AutoMapper;

using Domain.Entities;

namespace Application.Entities.Grade
{
    public class EntGrade
    {
        public int IdGrade { get; set; }
        public string Name { get; set; }
        public int IdTeacher { get; set; }
        public EntTeacher Teacher { get; set; }
        public ICollection<EntGradeStudent> StudentGrades { get; set; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Grade, EntGrade>();
                CreateMap<EntGrade, Domain.Entities.Grade>();
                CreateMap<EntGrade, CUGrade>();
                CreateMap<CUGrade, EntGrade>();
            }
        }
    }
}
