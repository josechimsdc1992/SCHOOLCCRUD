using Application.Cummon;

using Domain.Entities;

using Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class DatStudent:IDatStudent
    {
        protected SchoolContext _dbContext { get; }
        private readonly ILogger<DatStudent> _logger;
        public DatStudent(SchoolContext dbContext, ILogger<DatStudent> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ResultResponse<bool>> DDelete(int iKey)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();
            try
            {

                var entity = await _dbContext.Students.SingleOrDefaultAsync(u => u.IdStudent == iKey);
                if (entity != null)
                {
                    _dbContext.Students.Remove(entity);
                    _dbContext.SaveChanges();
                    response.SetSucesss(true);
                }

            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<ResultResponse<List<Student>>> DGet()
        {
            var response = new ResultResponse<List<Student>>();
            try
            {
                var query = await _dbContext.Students.AsNoTracking().ToListAsync();
                response.SetSucesss(query);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<Student>> DGet(int iKey)
        {
            var response = new ResultResponse<Student>();
            try
            {
                var query = await _dbContext.Students.SingleOrDefaultAsync(u => u.IdStudent == iKey);
                response.SetSucesss(query);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<Student>> DSave(Student newItem)
        {
            var response = new ResultResponse<Student>();
            try
            {
                _dbContext.Students.Add(newItem);

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

        public async Task<ResultResponse<List<Student>>> DSave(List<Student> entities)
        {
            var response = new ResultResponse<List<Student>>();
            try
            {
                _dbContext.Students.AddRange(entities);
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

        public async Task<ResultResponse<bool>> DUpdate(Student entity)
        {
            var response = new ResultResponse<bool>();
            try
            {
                _dbContext.Students.Attach(entity);
                var entry = _dbContext.Entry(entity);
                entry.Property(e => e.Name).IsModified = true;
                entry.Property(e => e.SurName).IsModified = true;
                entry.Property(e => e.Date).IsModified = true;
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
