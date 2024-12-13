using Domain.Entities;
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
        [SwaggerOperation(Summary = "Add or update Grade",
            Description = "Add or update Grade dependency on IdGrade")]
        [IMDMetodo(20231026100640, 20231026100652)]
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
                        EntGrade ent = new EntGrade();
                        ent.Name = request.Name;
                        ent.IdTeacher = request.IdTeacher;
                        ResultResponse<EntGrade> res = await _busGrade.BCreate(ent);
                        if (!res.HasError)
                        {
                            response.SetSuccess(res.Result);
                        }
                        else
                        {
                            response.SetError(res.Message);
                        }
                    }
                    else
                    {
                        EntGrade ent = new EntGrade();
                        ent.IdGrade = request.IdGrade;
                        ent.Name = request.Name;
                        ent.IdTeacher = request.IdTeacher;
                        ResultResponse<EntGrade> res = await _busGrade.BUpdate(ent);
                        if (!res.HasError)
                        {
                            response.SetSuccess(res.Result);
                        }
                        else
                        {
                            response.SetError(res.Message);
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

        [SwaggerOperation(Summary = "Lista de Grade",
            Description = "Regresa una lista de Grade")]
        [HttpGet("list")]
        [IMDMetodo(67823462368441, 67823462367664)]
        public async Task<ActionResult<ResultResponse<dynamic>>> GetAll()
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busGrade.BGetAll();
                response.Result = temp.Result;
                response.HasError = temp.HasError;
                response.HttpCode = temp.HttpCode;
                response.Message = temp.Message;
            }
            catch (Exception ex)
            {
                response.ErrorCode = metodo.iCodigoError;
                response.SetError(ex);

                _logger.LogError(IMDSerializer.Serialize(metodo.iCodigoError, $"Error en {metodo.sNombre}({metodo.sParametros}): {ex.Message}", ex, response));
            }
            return StatusCode((int)response.HttpCode, response);
        }

        [SwaggerOperation(Summary = "Get one Grade",
            Description = "Get one Grade")]
        [HttpGet("{IdGrade}")]
        [IMDMetodo(67823462259630, 67823462258830)]
        public async Task<ActionResult<ResultResponse<dynamic>>> Get(int IdGrade)
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busGrade.BGet(IdGrade);
                response.Result = temp.Result;
                response.HasError = temp.HasError;
                response.HttpCode = temp.HttpCode;
                response.Message = temp.Message;
            }
            catch (Exception ex)
            {
                
            }
            return StatusCode((int)response.HttpCode, response);
        }

        [SwaggerOperation(Summary = "Delete Grade",
            Description = "Delete Grade by IdGrade")]
        [HttpDelete("{IdGrade}")]
        [IMDMetodo(67823462262730, 67823462261930)]
        public async Task<ActionResult<IMDResponse<dynamic>>> Delete(int IdGrade)
        {
            ResultResponse<object> response = new ResultResponse<object>();

            try
            {
                var temp = await _busGrade.BDelete(IdChofer);
                response.Result = temp.Result;
                response.HasError = temp.HasError;
                response.HttpCode = temp.HttpCode;
                response.Message = temp.Message;
            }
            catch (Exception ex)
            {

            }
            return StatusCode((int)response.HttpCode, response);
        }

        [HttpPost("AddStudent")]
        [SwaggerOperation(Summary = "Add Student to Grade",
            Description = "Add Student to grade")]

        public async Task<ActionResult<ResultResponse<dynamic>>> Post(CUGradeStudent request)
        {
            ResultResponse<EntGrade> response = new ResultResponse<EntGrade>();

            try
            {
                var validator = new EntValidatorGradeStudent();
                ValidationResult result = validator.Validate(request);
                if (result.IsValid)
                {
                    CUGradeStudent ent = new CUGradeStudent();
                    ResultResponse<bool> res = await _busGrade.BAddStudent(request.IdGrade, request.IdStudent);
                    if (!res.HasError)
                    {
                        response.SetSuccess(res.Result);
                    }
                    else
                    {
                        response.SetError(res.Message);
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

        [HttpPost("AddStudent")]
        [SwaggerOperation(Summary = "Delete Student to Grade",
             Description = "Delete Student to grade")]
        [IMDMetodo(20231026100640, 20231026100652)]
        public async Task<ActionResult<ResultResponse<dynamic>>> Post(CUGradeStudent request)
        {
            ResultResponse<EntGrade> response = new ResultResponse<EntGrade>();

            try
            {
                var validator = new EntValidatorGradeStudent();
                ValidationResult result = validator.Validate(request);
                if (result.IsValid)
                {
                    CUGradeStudent ent = new CUGradeStudent();
                    ResultResponse<bool> res = await _busGrade.BRemoveStudent(request.IdGrade, request.IdStudent);
                    if (!res.HasError)
                    {
                        response.SetSuccess(res.Result);
                    }
                    else
                    {
                        response.SetError(res.Message);
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
    }
}
