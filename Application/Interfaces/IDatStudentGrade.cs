﻿using Application.Cummon;
using Application.Interfaces;

using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDatStudentGrade:IDatBase<StudentGrade>
    {
        Task<ResultResponse<StudentGrade>> DGetByGradeStudentGrupo(int IdGrade, int IdStudent, string Grupo);
        Task<ResultResponse<StudentGrade>> DGetByGradeStudent(int IdGrade, int IdStudent);
        Task<ResultResponse<List<StudentGrade>>> DGetByGrade(int IdGrade);
        public Task<ResultResponse<bool>> isUsedStudent(int idStudent);
        public Task<ResultResponse<bool>> isUsedGrade(int idGrade);

    }
}
