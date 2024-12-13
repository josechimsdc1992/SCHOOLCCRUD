using Application.Cummon;

using Domain.Entities;

using Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class DatStudentGrade:IDatStudentGrade
    {
        protected SchoolContext _dbContext { get; }
        private readonly ILogger<DatStudentGrade> _logger;
        public DatStudentGrade(SchoolContext dbContext, ILogger<DatStudentGrade> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ResultResponse<bool>> DDelete(int iKey)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();
            try
            {

                var entity = await _dbContext.StudentGrades.SingleOrDefaultAsync(u => u.IdStudentGrade == iKey);
                if (entity != null)
                {
                    _dbContext.StudentGrades.Remove(entity);
                    response.SetSucesss(true);
                }

            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<ResultResponse<List<StudentGrade>>> DGet()
        {
            var response = new ResultResponse<List<StudentGrade>>();
            try
            {
                var query = await _dbContext.StudentGrades.AsNoTracking().ToListAsync();
                response.SetSucesss(query);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<StudentGrade>> DGet(int iKey)
        {
            var response = new ResultResponse<StudentGrade>();
            try
            {
                var query = await _dbContext.StudentGrades.SingleOrDefaultAsync(u => u.IdStudentGrade == iKey);
                response.SetSucesss(query);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public async Task<ResultResponse<StudentGrade>> DGetByGradeStudent(int IdGrade,int IdStudent)
        {
            var response = new ResultResponse<StudentGrade>();
            try
            {
                var query = await _dbContext.StudentGrades.SingleOrDefaultAsync(u => u.IdStudentGrade == iKey && u.IdGrade== IdGrade);
                response.SetSucesss(query);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<List<StudentGrade>>> DGetByGrade(int IdGrade)
        {
            var response = new ResultResponse<StudentGrade>();
            try
            {
                var query = await _dbContext.StudentGrades.Where(u => u.IdGrade == IdGrade).ToList();
                response.SetSucesss(query);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<StudentGrade>> DSave(StudentGrade newItem)
        {
            var response = new ResultResponse<StudentGrade>();
            try
            {
                _dbContext.StudentGrades.Add(newItem);

                int i = await _dbContext.SaveChangesAsync();

                if (i == 0)
                {
                    return default;
                }
                response.SetSucesss(newItem);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<List<StudentGrade>>> DSave(List<StudentGrade> entities)
        {
            var response = new ResultResponse<List<StudentGrade>>();
            try
            {
                _dbContext.StudentGrades.AddRange(entities);
                int i = await _dbContext.SaveChangesAsync();

                if (i != 0)
                    response.SetSucesss(entities);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<bool>> DUpdate(StudentGrade entity)
        {
            var response = new ResultResponse<bool>();
            try
            {
                _dbContext.StudentGrades.Attach(entity);
                var entry = _dbContext.Entry(entity);
                bool IsModified = entry.Properties.Where(e => e.IsModified).Count() > 0;
                if (IsModified)
                {
                    int i = await _dbContext.SaveChangesAsync();
                    if (i == 1)
                        response.SetSucesss(true);
                }
                else
                {
                    response.SetError("No guardado");
                }
            }
            catch (Exception ex)
            {
                return response;
            }

            return response;
        }

    }
}
