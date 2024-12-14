using Application.Cummon;
using Application.Entities.Student;
using Application.Entities.Validation;
using Application.Repository;

using FluentValidation.Results;

using Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/student")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IBusStudent _busStudent;

        public StudentController(ILogger<StudentController> logger, IBusStudent busStudent)
        {
            _logger = logger;
            _busStudent = busStudent;
        }

        [HttpPost]
        //[SwaggerOperation(Summary = "Add or update Student",
        //    Description = "Add or update Student dependency on Student")]
        public async Task<ActionResult<ResultResponse<EntStudent>>> Post(CUStudent request)
        {
            ResultResponse<EntStudent> response = new ResultResponse<EntStudent>();

            try
            {
                var validator = new EntValidadorStudent();
                ValidationResult result = validator.Validate(request);
                if (result.IsValid)
                {
                    if (request.IdStudent == 0)
                    {
                        ResultResponse<EntStudent> res = await _busStudent.BCreate(request);
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
                        ResultResponse<EntStudent> res = await _busStudent.BUpdate(request);
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

        //[SwaggerOperation(Summary = "Lista de Student",
        //    Description = "Regresa una lista de Student")]
        [HttpGet("list")]
        public async Task<ActionResult<ResultResponse<dynamic>>> GetAll()
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busStudent.BGetAll();
                response.SetSucesss(temp.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);

            }
            return StatusCode((int)response.StatusCode, response);
        }

        //[SwaggerOperation(Summary = "Get one Student",
        //    Description = "Get one Student")]
        [HttpGet("{IdStudent}")]
        public async Task<ActionResult<ResultResponse<dynamic>>> Get(int IdStudent)
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busStudent.BGet(IdStudent);
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

        //[SwaggerOperation(Summary = "Delete Student",
        //   Description = "Delete Grade by IdStudent")]
        [HttpDelete("{IdStudent}")]
        public async Task<ActionResult<ResultResponse<dynamic>>> Delete(int IdStudent)
        {
            ResultResponse<object> response = new ResultResponse<object>();

            try
            {
                var temp = await _busStudent.BDelete(IdStudent);
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
