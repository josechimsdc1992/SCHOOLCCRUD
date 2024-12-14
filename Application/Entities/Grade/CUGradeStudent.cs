using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Grade
{
    public class CUGradeStudent
    {
        public int IdGrade { get; set; }
        public int IdStudent { get; set; }
        public string Grupo { get; set; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.StudentGrade, CUGradeStudent>();
                CreateMap<CUGradeStudent, Domain.Entities.StudentGrade>();
            }
        }
    }
}
