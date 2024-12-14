using Application.Entities.Student;
using Application.Entities.Teacher;

using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Validation
{
    public class EntValidatorTeacher : AbstractValidator<CUTeacher>
    {
        public EntValidatorTeacher()
        {

            RuleFor(entity => entity.Name)
                    .NotEmpty()
                    .WithMessage("Name es requerido");

            RuleFor(entity => entity.SurName)
                   .NotEmpty()
                   .WithMessage("SurName es requerido");
        }

    }
}
