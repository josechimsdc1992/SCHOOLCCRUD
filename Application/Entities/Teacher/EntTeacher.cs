using Application.Entities.Grade;

using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Teacher
{
    public class EntTeacher
    {
        public int IdTeacher { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public char Genero { get; set; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Teacher, EntTeacher>();
                CreateMap<EntTeacher, Domain.Entities.Teacher>();

                CreateMap<EntTeacher, CUTeacher>();
                CreateMap<CUTeacher, EntTeacher>();
            }
        }
    }
}
