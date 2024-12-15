using Application.Cummon;
using Application.Entities.Grade;
using Application.Entities.Validation;
using Application.Repository;

using Domain.Entities;

using FluentValidation.Results;

using Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/grade")]
    public class GradeController : ControllerBase
    {
        private readonly ILogger<GradeController> _logger;
        private readonly IBusGrade _busGrade;

        public GradeController(ILogger<GradeController> logger, IBusGrade busGrade)
        {
            _logger = logger;
            _busGrade = busGrade;
        }
        [HttpPost]
        //[SwaggerOperation(Summary = "Add or update Grade",
        //    Description = "Add or update Grade dependency on IdGrade")]
        public async Task<ActionResult<ResultResponse<EntGrade>>> Post(CUGrade request)
        {
            ResultResponse<EntGrade> response = new ResultResponse<EntGrade>();

            try
            {
                var validator = new EntValidatorGrade();
                ValidationResult result = validator.Validate(request);
                if (result.IsValid)
                {
                    if (request.IdGrade == 0)
                    {
                        ResultResponse<EntGrade> res = await _busGrade.BCreate(request);
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
                        ResultResponse<EntGrade> res = await _busGrade.BUpdate(request);
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

        //[SwaggerOperation(Summary = "Lista de Grade",
        //    Description = "Regresa una lista de Grade")]
        [HttpGet("list")]
        public async Task<ActionResult<ResultResponse<dynamic>>> GetAll()
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busGrade.BGetAll();
                response.Result = temp.Result;
                response.HasError = temp.HasError;
                response.Mensaje = temp.Mensaje;
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);

            }
            return StatusCode((int)response.StatusCode, response);
        }

        //[SwaggerOperation(Summary = "Get one Grade",
        //    Description = "Get one Grade")]
        [HttpGet("{IdGrade}")]
        public async Task<ActionResult<ResultResponse<dynamic>>> Get(int IdGrade)
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busGrade.BGet(IdGrade);
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

        //[SwaggerOperation(Summary = "Delete Grade",
        //    Description = "Delete Grade by IdGrade")]
        [HttpDelete("{IdGrade}")]
        public async Task<ActionResult<ResultResponse<dynamic>>> Delete(int IdGrade)
        {
            ResultResponse<object> response = new ResultResponse<object>();

            try
            {
                var temp = await _busGrade.BDelete(IdGrade);
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

        [HttpPost("AddStudent")]
        //[SwaggerOperation(Summary = "Add Student to Grade",
        //    Description = "Add Student to grade")]

        public async Task<ActionResult<ResultResponse<bool>>> AddStudentGrade(CUGradeStudent request)
        {
            ResultResponse<bool> response = new ResultResponse<bool>();

            try
            {
                var validator = new EntValidadorGradeStudent();
                ValidationResult result = validator.Validate(request);
                if (result.IsValid)
                {
                    ResultResponse<bool> res = await _busGrade.BAddStudent(request);
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
                    response.SetError(string.Join(", ", result.Errors.Select(x => x.ErrorMessage)));
                }

            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;

        }

        [HttpPost("RemoveStudent")]
        //[SwaggerOperation(Summary = "Delete Student to Grade",
        //     Description = "Delete Student to grade")]
        public async Task<ActionResult<ResultResponse<EntGrade>>> DeleteStudentGrade(CUGradeStudent request)
        {
            ResultResponse<EntGrade> response = new ResultResponse<EntGrade>();

            try
            {
                var validator = new EntValidadorGradeStudent();
                ValidationResult result = validator.Validate(request);
                if (result.IsValid)
                {
                    CUGradeStudent ent = new CUGradeStudent();
                    ResultResponse<bool> res = await _busGrade.BRemoveStudent(request);
                    if (!res.HasError)
                    {
                        response.SetSucesss(new EntGrade());
                    }
                    else
                    {
                        response.SetError(res.Mensaje);
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
        //[SwaggerOperation(Summary = "Get one Grade",
        //    Description = "Get one Grade")]
        [HttpGet("{IdGrade}/{IdStudent}")]
        public async Task<ActionResult<ResultResponse<dynamic>>> Get(int IdGrade,int IdStudent)
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busGrade.BGetStudent(IdGrade,IdStudent);
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
