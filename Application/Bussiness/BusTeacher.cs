using Application.Cummon;
using Application.Entities.Grade;
using Application.Entities.Teacher;
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
    public class BusTeacher:IBusTeacher
    {
        private readonly ILogger<BusTeacher> _logger;
        private readonly IDatTeacher _datTeacher;
        public BusTeacher(ILogger<BusTeacher> logger, IDatTeacher datTeacher)
        {
            this._logger = logger;
            this._datTeacher = datTeacher;
        }
        public async Task<ResultResponse<EntTeacher>> BCreate(CUTeacher createModel)
        {
            ResultResponse<EntTeacher> response = new ResultResponse<EntTeacher>();
            string metodo = nameof(this.BCreate);

            try
            {
                Teacher entEntity = new Teacher();
                entEntity.Name = createModel.Name;
                entEntity.IdTeacher = createModel.IdTeacher;
                var resp = await _datTeacher.DSave(entEntity);


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
                response = await _datTeacher.DDelete(iKey);
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

        public async Task<ResultResponse<EntTeacher>> BGet(Guid iKey)
        {
            ResultResponse<EntTeacher> response = new ResultResponse<EntTeacher>();
            string metodo = nameof(this.BGet);

            try
            {
                var resData = await _datTeacher.DGet(iKey);
                response.SetSuccess(resData.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }
        public async Task<ResultResponse<List<EntTeacher>>> BGetAll()
        {
            ResultResponse<List<EntTeacher>> response = new ResultResponse<List<EntTeacher>>();
            string metodo = nameof(this.BGetAll);

            try
            {
                var resData = await _datTeacher.DGet();
                response.SetSuccess(resData.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }

            return response;
        }

        public async Task<ResultResponse<EntTeacher>> BUpdate(CUTeacher updateModel)
        {
            ResultResponse<EntTeacher> response = new ResultResponse<EntTeacher>();
            string metodo = nameof(this.BUpdate);

            try
            {
                Teacher entEntity = new Teacher();
                entEntity.IdGrade = createModel.IdGrade;
                entEntity.Name = createModel.Name;
                entEntity.IdTeacher = createModel.IdTeacher;
                var resp = await _datTeacher.DUpdate(entEntity);


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
