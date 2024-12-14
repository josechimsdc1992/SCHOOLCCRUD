using Application.Entities.Grade;

using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Validation
{
    public class EntValidadorGradeStudent : AbstractValidator<CUGradeStudent>
    {
        public EntValidadorGradeStudent()
        {
            RuleFor(entity => entity.IdGrade)
                   .NotEmpty()
                   .WithMessage("IdGrade es requerido");

            RuleFor(entity => entity.IdStudent)
                   .NotEmpty()
                   .WithMessage("IdStudent es requerido");
            RuleFor(entity => entity.Grupo)
                   .NotEmpty()
                   .WithMessage("Grupo es requerido");




        }
    }
}