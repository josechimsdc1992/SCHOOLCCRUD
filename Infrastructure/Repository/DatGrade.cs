using Application.Cummon;

using Domain.Entities;

using Interfacess;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class DatGrade:IDatGrade
    {
        protected SchoolContext _dbContext { get; }
        private readonly ILogger<DatGrade> _logger;
        public DatGrade(SchoolContext dbContext, ILogger<DatGrade> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ResultResponse<bool>> DDelete(int iKey)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();
            try
            {

                var entity = await _dbContext.Grades.SingleOrDefaultAsync(u => u.IdGrade == iKey);
                if (entity != null)
                {
                    _dbContext.Grades.Remove(entity);
                    response.SetSucesss(true);
                }

            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<ResultResponse<List<Grade>>> DGet()
        {
            var response = new ResultResponse<List<Grade>>();
            try
            {
                var query = await _dbContext.Grades.AsNoTracking().ToListAsync();
                response.SetSucesss(query); 
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<Grade>> DGet(int iKey)
        {
            var response = new ResultResponse<Grade>();
            try
            {
                var query = await _dbContext.Grades.SingleOrDefaultAsync(u => u.IdGrade == iKey);
                response.SetSucesss(query);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<Grade>> DSave(Grade newItem)
        {
            var response = new ResultResponse<Grade>();
            try
            {
                _dbContext.Grades.Add(newItem);

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

        public async Task<ResultResponse<List<Grade>>> DSave(List<Grade> entities)
        {
            var response = new ResultResponse<List<Grade>>();
            try
            {
                _dbContext.Grades.AddRange(entities);
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

        public async Task<ResultResponse<bool>> DUpdate(Grade entity)
        {
            var response = new ResultResponse<bool>();
            try
            {
                _dbContext.Grades.Attach(entity);
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
