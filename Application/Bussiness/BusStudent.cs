using Application.Cummon;
using Application.Entities.Grade;
using Application.Entities.Student;
using Application.Entities.Teacher;
using Application.Repository;

using AutoMapper;

using Domain.Entities;

using Interfaces;

using Interfacess;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness
{
    public class BusStudent : IBusStudent
    {
        private readonly ILogger<BusStudent> _logger;
        private readonly IDatStudent _datStudent;
        private readonly IDatStudentGrade _datStudentGrade;
        private readonly IMapper _mapper;
        public BusStudent(ILogger<BusStudent> logger, IDatStudent datStudent,IMapper mapper, IDatStudentGrade datStudentGrade)
        {
            this._logger = logger;
            this._datStudent = datStudent;
            this._mapper = mapper;
            _datStudentGrade = datStudentGrade;
        }
        public async Task<ResultResponse<EntStudent>> BCreate(CUStudent createModel)
        {
            ResultResponse<EntStudent> response = new ResultResponse<EntStudent>();
            string metodo = nameof(this.BCreate);

            try
            {
                Student entEntity = _mapper.Map<Student>(createModel);
                var resp = await _datStudent.DSave(entEntity);


                if (resp.HasError)
                {
                    response.SetError("No se ha creado");
                }
                else
                {
                    response.SetSucesss(_mapper.Map<EntStudent>(resp.Result));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<bool>> BDelete(int iKey)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();
            string metodo = nameof(this.BDelete);

            try
            {
                ResultResponse<bool> resValid = await _datStudentGrade.isUsedStudent(iKey);
                if (resValid.Result)
                {
                    response.SetError("The Student already is used in Grade");
                    return response;
                }
                response = await _datStudent.DDelete(iKey);
                if (!response.Result)
                {
                    response.SetError("No se ha borrado");
                    return response;
                }
                response.SetSucesss(true);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<EntStudent>> BGet(int iKey)
        {
            ResultResponse<EntStudent> response = new ResultResponse<EntStudent>();
            string metodo = nameof(this.BGet);

            try
            {
                var resData = await _datStudent.DGet(iKey);
                response.SetSucesss(_mapper.Map<EntStudent>(resData.Result));
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public async Task<ResultResponse<List<EntStudent>>> BGetAll()
        {
            ResultResponse<List<EntStudent>> response = new ResultResponse<List<EntStudent>>();
            string metodo = nameof(this.BGetAll);

            try
            {
                var resData = await _datStudent.DGet();
                response.SetSucesss(_mapper.Map<List<Student>, List<EntStudent>>(resData.Result));
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<EntStudent>> BUpdate(CUStudent updateModel)
        {
            ResultResponse<EntStudent> response = new ResultResponse<EntStudent>();
            string metodo = nameof(this.BUpdate);

            try
            {
                Student entEntity = _mapper.Map<Student>(updateModel);
                var resp = await _datStudent.DUpdate(entEntity);


                if (resp.HasError)
                {
                    response.SetError("No se ha actualizado");
                }
                else
                {
                    response.SetSucesss(_mapper.Map<EntStudent>(updateModel));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
    }
}
