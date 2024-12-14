﻿using Application.Entities.Grade;
using AutoMapper;

using System;
using System.Collections.Generic;
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
        public DateTime Date { get; set; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Student, EntStudent>();
                CreateMap<EntStudent, Domain.Entities.Student>();
                CreateMap<EntStudent, CUStudent>();
                CreateMap<CUStudent, EntStudent>();
            }
        }
    }
}
