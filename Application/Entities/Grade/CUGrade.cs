using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Entities.Grade
{
    public class CUGrade
    {
        public int IdGrade { get; set; }
        public string Name { get; set; }
        public int IdTeacher { get; set; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Grade, CUGrade>();
                CreateMap<CUGrade, Domain.Entities.Grade>();
            }
        }
    }

    
}
