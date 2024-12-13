using Application.Cummon;

using Domain.Entities;

using Infrastructure;
using Infrastructure.Repository;

using Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolService.Infrastructure.Repository
{
    public class DatTeacher:IDatTeacher
    {
        protected SchoolContext _dbContext { get; }
        private readonly ILogger<DatTeacher> _logger;
        public DatTeacher(SchoolContext dbContext, ILogger<DatTeacher> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ResultResponse<bool>> DDelete(int iKey)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();
            try
            {

                var entity = await _dbContext.Teachers.SingleOrDefaultAsync(u => u.IdTeacher == iKey);
                if (entity != null)
                {
                    _dbContext.Teachers.Remove(entity);
                    response.SetSucesss(true);
                }

            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<ResultResponse<List<Teacher>>> DGet()
        {
            var response = new ResultResponse<List<Teacher>>();
            try
            {
                var query = await _dbContext.Teachers.AsNoTracking().ToListAsync();
                response.SetSucesss(query);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<Teacher>> DGet(int iKey)
        {
            var response = new ResultResponse<Teacher>();
            try
            {
                var query = await _dbContext.Teachers.SingleOrDefaultAsync(u => u.IdTeacher == iKey);
                response.SetSucesss(query);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<Teacher>> DSave(Teacher newItem)
        {
            var response = new ResultResponse<Teacher>();
            try
            {
                _dbContext.Teachers.Add(newItem);

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

        public async Task<ResultResponse<List<Teacher>>> DSave(List<Teacher> entities)
        {
            var response = new ResultResponse<List<Teacher>>();
            try
            {
                _dbContext.Teachers.AddRange(entities);
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

        public async Task<ResultResponse<bool>> DUpdate(Teacher entity)
        {
            var response = new ResultResponse<bool>();
            try
            {
                _dbContext.Teachers.Attach(entity);
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
