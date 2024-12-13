using Application.Cummon;
using Application.Entities.Grade;
using Application.Entities.Student;
using Application.Repository;
using Interfaces;
using Interfacess;
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
        public BusGrade(ILogger<BusGrade> logger, IDatGrade datGrade, IDatStudentGrade datStudentGrade, IDatStudent student)
        {
            this._logger = logger;
            this._datGrade = datGrade;
            this._datstudentGrade = datStudentGrade;
            this._datstudent = _datstudent;
        }
        public async Task<ResultResponse<EntGrade>> BCreate(CUGrade createModel)
        {
            ResultResponse<EntGrade> response = new ResultResponse<EntGrade>();
            string metodo = nameof(this.BCreate);

            try
            {
                Grade entEntity = new Grade();
                entEntity.Name =createModel.Name;
                entEntity.IdTeacher = createModel.IdTeacher;
                var resp = await _datGrade.DSave(entEntity);


                if (resp.HasError)
                {
                    response.SetError("No se ha creado");
                }
                else
                {
                    response.SetSucesss(entEntity);
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<bool>> BDelete(Guid iKey)
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
                response.SetSuccess(response.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<EntGrade>> BGet(Guid iKey)
        {
            ResultResponse<EntGrade> response = new ResultResponse<EntPaquete>();
            string metodo = nameof(this.BGet);

            try
            {
                var resData = await _datGrade.DGet(iKey);
                response.SetSuccess(resData.Result);
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
                response.SetSuccess(resData.Result);
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
                Grade entEntity = new Grade();
                entEntity.IdGrade= createModel.IdGrade;
                entEntity.Name = createModel.Name;
                entEntity.IdTeacher = createModel.IdTeacher;
                var resp = await _datGrade.DUpdate(entEntity);


                if (resp.HasError)
                {
                    response.SetError("No se ha actualizado");
                }
                else
                {
                    response.SetSuccess(entEntity);
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public async Task<ResultResponse<bool>> BAddStudent(int IdGrade, int IdStudent)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();
            string metodo = nameof(this.BDelete);

            try
            {
                Grade grade= _datGrade.DGet(IdGrade);
                if (grade == null)
                {
                    //Error
                }
                Student student = await _datstudent.DGet(IdStudent);
                if (student == null)
                {
                    //error
                }


                StudentGrade studentGrade = _datstudentGrade.DGetByGradeStudent(grade.IdGrade, student.IdStudent);
                if (studentGrade != null)
                {
                    //error
                }
                StudentGrade studentGrade = new StudentGrade();
                studentGrade.IdGrade= grade.IdGrade;
                studentGrade.IdStudent = student.IdStudent;
                 var resCreate= _datstudentGrade.DSave(studentGrade);
                if (!response.Result)
                {
                    response.SetError("No se ha borrado");
                }
                response.SetSuccess(response.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public async Task<ResultResponse<bool>> BRemoveStudent(int IdGrade, int IdStudent)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();
            string metodo = nameof(this.BDelete);

            try
            {
                StudentGrade studentGrade = _datstudentGrade.DGetByGradeStudent(grade.IdGrade, student.IdStudent);
                response = await _datstudentGrade.DDelete(studentGrade.idstudentGrade);

                if (!response.Result)
                {
                    response.SetError("No se ha borrado");
                }
                response.SetSuccess(response.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

    }
}
