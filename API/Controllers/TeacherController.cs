using Application.Cummon;
using Application.Entities.Teacher;
using Application.Entities.Validation;
using Application.Repository;

using FluentValidation.Results;

using Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/teacher")]
    public class TeacherController : ControllerBase
    {
        
        private readonly ILogger<TeacherController> _logger;
        private readonly IBusTeacher _busTeacher;

        public TeacherController(ILogger<TeacherController> logger, IBusTeacher busTeacher)
        {
            _logger = logger;
            _busTeacher = busTeacher;
        }
        [HttpPost]
        //[SwaggerOperation(Summary = "Add or update Teacher",
        //    Description = "Add or update Teache dependency on IdTeacher")]
        public async Task<ActionResult<ResultResponse<EntTeacher>>> Post(CUTeacher request)
        {
            ResultResponse<EntTeacher> response = new ResultResponse<EntTeacher>();

            try
            {
                var validator = new EntValidatorTeacher();
                ValidationResult result = validator.Validate(request);
                if (result.IsValid)
                {
                    if (request.IdTeacher == 0)
                    {
                        EntTeacher ent = new EntTeacher();
                        ent.Name = request.Name;
                        ent.SurName = request.SurName;
                        ResultResponse<EntTeacher> res = await _busTeacher.BCreate(request);
                        if (!res.HasError)
                        {
                            response.SetSucesss(res.Result);
                        }
                        else
                        {
                            response.SetError(res.Mensaje);
                        }
                    }
                    else
                    {
                        ResultResponse<EntTeacher> res = await _busTeacher.BUpdate(request);
                        if (!res.HasError)
                        {
                            response.SetSucesss(res.Result);
                        }
                        else
                        {
                            response.SetError(res.Mensaje);
                        }
                    }

                }
                else
                {
                    response.SetError(string.Join(", ", result.Errors.Select(x => x.ErrorMessage)));
                }

            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;

        }

        //[SwaggerOperation(Summary = "Lista de Teacher",
        //    Description = "Regresa una lista de Teacher")]
        [HttpGet("list")]
        public async Task<ActionResult<ResultResponse<dynamic>>> GetAll()
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busTeacher.BGetAll();
                response.Result = temp.Result;
                response.HasError = temp.HasError;
                response.StatusCode = temp.StatusCode;
                response.Mensaje = temp.Mensaje;
            }
            catch (Exception ex)
            {

            }
            return StatusCode((int)response.StatusCode, response);
        }

        //[SwaggerOperation(Summary = "Get one Teacher",
        //    Description = "Get one Teacher")]
        [HttpGet("{IdTeacher}")]
        public async Task<ActionResult<ResultResponse<dynamic>>> Get(int IdTeacher)
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busTeacher.BGet(IdTeacher);
                response.Result = temp.Result;
                response.HasError = temp.HasError;
                response.StatusCode = temp.StatusCode;
                response.Mensaje = temp.Mensaje;
            }
            catch (Exception ex)
            {

            }
            return StatusCode((int)response.StatusCode, response);
        }

        //[SwaggerOperation(Summary = "Delete Teacher",
        //   Description = "Delete Teacher by IdTeacher")]
        [HttpDelete("{IdTeacher}")]
        public async Task<ActionResult<ResultResponse<dynamic>>> Delete(int IdTeacher)
        {
            ResultResponse<object> response = new ResultResponse<object>();

            try
            {
                var temp = await _busTeacher.BDelete(IdTeacher);
                response.Result = temp.Result;
                response.HasError = temp.HasError;
                response.StatusCode = temp.StatusCode;
                response.Mensaje = temp.Mensaje;
            }
            catch (Exception ex)
            {

            }
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
