using Application.Cummon;
using Application.Entities.Grade;
using Application.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IBusGrade : IBusBase<EntGrade, CUGrade, CUGrade>
    {
        Task<ResultResponse<bool>> BAddStudent(CUGradeStudent gradeStudent);
        Task<ResultResponse<bool>> BRemoveStudent(CUGradeStudent gradeStudent);
    }
}
