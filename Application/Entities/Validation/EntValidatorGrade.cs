using Application.Entities.Grade;
using Application.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Validation
{
    public class EntValidatorGrade : AbstractValidator<CUGrade>
    {
        public EntValidatorGrade() {
            RuleFor(entity => entity.IdGrade)
                       .NotEmpty()
                       .WithMessage("IdStudent es requerido");

            RuleFor(entity => entity.Name)
                   .NotEmpty()
                   .WithMessage("Name es requerido");

            RuleFor(entity => entity.IdTeacher)
                   .NotEmpty()
                   .WithMessage("IdTeacher es requerido");
        }
    }
}
