using Application.Cummon;
using Application.Entities.Student;
using Application.Entities.Teacher;
using Application.Repository;
using Interfaces;
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
        public BusStudent(ILogger<BusStudent> logger, IDatStudent datStudent)
        {
            this._logger = logger;
            this._datStudent = datStudent;
        }
        public async Task<ResultResponse<EntStudent>> BCreate(CUStudent createModel)
        {
            ResultResponse<EntStudent> response = new ResultResponse<EntStudent>();
            string metodo = nameof(this.BCreate);

            try
            {
                Student entEntity = new Student();
                entEntity.Name = createModel.Name;
                entEntity.IdTeacher = createModel.IdTeacher;
                var resp = await _datStudent.DSave(entEntity);


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
                response = await _datStudent.DDelete(iKey);
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

        public async Task<ResultResponse<EntStudent>> BGet(Guid iKey)
        {
            ResultResponse<EntStudent> response = new ResultResponse<EntStudent>();
            string metodo = nameof(this.BGet);

            try
            {
                var resData = await _datStudent.DGet(iKey);
                response.SetSuccess(resData.Result);
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
                response.SetSuccess(resData.Result);
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
                Student entEntity = new Student();
                entEntity.IdGrade = createModel.IdGrade;
                entEntity.Name = createModel.Name;
                entEntity.IdTeacher = createModel.IdTeacher;
                var resp = await _datStudent.DUpdate(entEntity);


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
    }
}
