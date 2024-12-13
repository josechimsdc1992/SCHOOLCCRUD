using Application.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Validation
{
    public class EntValidadorStudent:AbstractValidator<CUStudent>
    {
        EntValidadorStudent() {
            RuleFor(entity => entity.IdStudent)
                   .NotEmpty()
                   .WithMessage("IdStudent es requerido");

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
                   .WithMessage("SurName es requerido");
        }
    }
}
