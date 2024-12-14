using Application.Cummon;
using Application.Entities.Grade;
using Application.Entities.Student;
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
    public class BusGrade:IBusGrade
    {
        private readonly ILogger<BusGrade> _logger;
        private readonly IDatGrade _datGrade;
        private readonly IDatStudentGrade _datstudentGrade;
        private readonly IDatStudent _datstudent;
        private readonly IMapper _mapper;
        public BusGrade(ILogger<BusGrade> logger, IDatGrade datGrade, IDatStudentGrade datStudentGrade, IDatStudent datstudent,IMapper mapper)
        {
            this._logger = logger;
            this._datGrade = datGrade;
            this._datstudentGrade = datStudentGrade;
            this._datstudent = datstudent;
            this._mapper = mapper;
        }
        public async Task<ResultResponse<EntGrade>> BCreate(CUGrade createModel)
        {
            ResultResponse<EntGrade> response = new ResultResponse<EntGrade>();
            string metodo = nameof(this.BCreate);

            try
            {
                Grade entEntity = _mapper.Map<Grade>(createModel);
                var resp = await _datGrade.DSave(entEntity);


                if (resp.HasError)
                {
                    response.SetError("No se ha creado");
                }
                else
                {
                    response.SetSucesss(_mapper.Map<EntGrade>(resp.Result));
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
                response = await _datGrade.DDelete(iKey);
                if (!response.Result)
                {
                    response.SetError("No se ha borrado");
                }
                response.SetSucesss(response.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<EntGrade>> BGet(int iKey)
        {
            ResultResponse<EntGrade> response = new ResultResponse<EntGrade>();
            string metodo = nameof(this.BGet);

            try
            {
                var resData = await _datGrade.DGet(iKey);
                var resDetail = await _datstudentGrade.DGetByGrade(iKey);
                resData.Result.StudentGrades=resDetail.Result;

                response.SetSucesss(_mapper.Map<EntGrade>(resData.Result));
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public async Task<ResultResponse<List<EntGrade>>> BGetAll()
        {
            ResultResponse<List<EntGrade>> response = new ResultResponse<List<EntGrade>>();
            string metodo = nameof(this.BGetAll);

            try
            {
                var resData = await _datGrade.DGet();

                response.SetSucesss(_mapper.Map<List<EntGrade>>(resData.Result));
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<EntGrade>> BUpdate(CUGrade updateModel)
        {
            ResultResponse<EntGrade> response = new ResultResponse<EntGrade>();
            string metodo = nameof(this.BUpdate);

            try
            {
                Grade entEntity = _mapper.Map<Grade>(updateModel); 
                var resp = await _datGrade.DUpdate(entEntity);


                if (resp.HasError)
                {
                    response.SetError("No se ha actualizado");
                }
                else
                {
                    response.SetSucesss(_mapper.Map<EntGrade>(updateModel));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public async Task<ResultResponse<bool>> BAddStudent(CUGradeStudent gradeStudent)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();
            string metodo = nameof(this.BDelete);

            try
            {
                ResultResponse<Grade> resGrade=await _datGrade.DGet(gradeStudent.IdGrade);
                if (resGrade.Result == null)
                {
                    //Error
                }
                ResultResponse<Student> student = await _datstudent.DGet(gradeStudent.IdStudent);
                if (student == null)
                {
                    //error
                }


                ResultResponse<StudentGrade> resStudentGrade =await _datstudentGrade.DGetByGradeStudent(gradeStudent.IdGrade,   gradeStudent.IdStudent,gradeStudent.Grupo);
                if (resStudentGrade.Result != null)
                {
                    //error
                }
                StudentGrade studentGrade = _mapper.Map<StudentGrade>(gradeStudent);
                 var resCreate= _datstudentGrade.DSave(studentGrade);
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
        public async Task<ResultResponse<bool>> BRemoveStudent(CUGradeStudent gradeStudent)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();
            string metodo = nameof(this.BDelete);

            try
            {
                ResultResponse<StudentGrade> resStudentGrade =await _datstudentGrade.DGetByGradeStudent(gradeStudent.IdGrade, gradeStudent.IdStudent,gradeStudent.Grupo);
                if(!response.HasError && response.Result!=null)
                {
                    response = await _datstudentGrade.DDelete(resStudentGrade.Result.IdStudentGrade);
                }
                

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

    }
}
