using Application.Entities.Student;

using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Validation
{
    public class EntValidadorStudent:AbstractValidator<CUStudent>
    {
       public EntValidadorStudent() {

            RuleFor(entity => entity.Name)
                   .NotEmpty()
                   .WithMessage("Name es requerido");

            RuleFor(entity => entity.SurName)
                   .NotEmpty()
                   .WithMessage("SurName es requerido");

            RuleFor(entity => entity.Genero)
                   .NotEmpty()
                   .WithMessage("Genero es requerido");

            RuleFor(entity => entity.Date)
                   .NotEmpty()
                   .WithMessage("Date es requerido");
        }
    }
}
