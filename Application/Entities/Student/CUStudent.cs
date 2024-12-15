using Application.Entities.Grade;
using AutoMapper;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Student
{
    public class CUStudent
    {
        public int IdStudent { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public char Genero { get; set; }
        public string Date { get; set; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Student, CUStudent>().ForMember(dest => dest.Date,
               opts => opts.MapFrom(src => src.Date.ToString("dd/MM/yyyy")));
                CreateMap<CUStudent, Domain.Entities.Student>().ForMember(dest => dest.Date,
               opts => opts.MapFrom(src => DateTime.ParseExact(src.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
            }
        }
    }
}
