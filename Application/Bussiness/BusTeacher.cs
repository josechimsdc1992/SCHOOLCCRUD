using Application.Cummon;
using Application.Entities.Grade;
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
    public class BusTeacher:IBusTeacher
    {
        private readonly ILogger<BusTeacher> _logger;
        private readonly IDatTeacher _datTeacher;
        private readonly IMapper _mapper;
        private readonly IDatGrade _datGrade;
        public BusTeacher(ILogger<BusTeacher> logger, IDatTeacher datTeacher,IMapper mapper, IDatGrade datGrade)
        {
            this._logger = logger;
            this._datTeacher = datTeacher;
            this._mapper = mapper;
            _datGrade = datGrade;
        }
        public async Task<ResultResponse<EntTeacher>> BCreate(CUTeacher createModel)
        {
            ResultResponse<EntTeacher> response = new ResultResponse<EntTeacher>();
            string metodo = nameof(this.BCreate);

            try
            {
                Teacher entEntity = _mapper.Map<Teacher>(createModel);
                var resp = await _datTeacher.DSave(entEntity);


                if (resp.HasError)
                {
                    response.SetError("No se ha creado");
                }
                else
                {
                    response.SetSucesss(_mapper.Map<EntTeacher>(resp.Result));
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
                ResultResponse<bool> resValid = await _datGrade.isUsedTeacher(iKey);
                if (resValid.Result)
                {
                    response.SetError("The techear already is used in Grade");
                    return response;
                }
                response = await _datTeacher.DDelete(iKey);
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

        public async Task<ResultResponse<EntTeacher>> BGet(int iKey)
        {
            ResultResponse<EntTeacher> response = new ResultResponse<EntTeacher>();
            string metodo = nameof(this.BGet);

            try
            {
                var resData = await _datTeacher.DGet(iKey);
                response.SetSucesss(_mapper.Map<EntTeacher>(resData.Result));
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
                response.SetSucesss(_mapper.Map<List<EntTeacher>>(resData.Result));
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
                Teacher entEntity = _mapper.Map<Teacher>(updateModel);
                var resp = await _datTeacher.DUpdate(entEntity);


                if (resp.HasError)
                {
                    response.SetError("No se ha actualizado");
                }
                else
                {
                    response.SetSucesss(_mapper.Map<EntTeacher>(updateModel));
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
