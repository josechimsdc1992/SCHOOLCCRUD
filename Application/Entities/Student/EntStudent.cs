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
    public class EntStudent
    {
        public int IdStudent { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public char Genero { get; set; }
        public String Date { get; set; }
        public string FullName { get { return Name + " " + SurName; } }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Student, EntStudent>().ForMember(dest => dest.Date,
               opts => opts.MapFrom(src => src.Date.ToString("dd/MM/yyyy")));
                CreateMap<EntStudent, Domain.Entities.Student>().ForMember(dest => dest.Date,
               opts => opts.MapFrom(src => DateTime.ParseExact(src.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
                CreateMap<EntStudent, CUStudent>();
                CreateMap<CUStudent, EntStudent>();
            }
        }
    }
}
