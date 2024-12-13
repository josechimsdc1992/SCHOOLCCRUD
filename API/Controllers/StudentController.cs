using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[student]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IDatStudent _datStudent;

        public StudentController(ILogger<StudentController> logger, IDatStudent datStudent)
        {
            _logger = logger;
            _datStudent = datStudent;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add or update Student",
            Description = "Add or update Student dependency on Student")]
        public async Task<ActionResult<ResultResponse<EntStudent>>> Post(CUStudent request)
        {
            ResultResponse<EntStudent> response = new ResultResponse<EntStudent>();

            try
            {
                var validator = new EntValidatorStudent();
                ValidationResult result = validator.Validate(request);
                if (result.IsValid)
                {
                    if (request.IdStudent == 0)
                    {
                        EntStudent ent = new EntStudent();
                        ent.Name = request.Name;
                        ent.IdTeacher = request.IdTeacher;
                        ResultResponse<EntStudent> res = await _busStudent.BCreate(ent);
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
                        EntStudent ent = new EntStudent();
                        ent.IdStudent = request.IdStudent;
                        ent.Name = request.Name;
                        ent.SurName = request.SurName;
                        ResultResponse<EntStudent> res = await _busStudent.BUpdate(ent);
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

        [SwaggerOperation(Summary = "Lista de Student",
            Description = "Regresa una lista de Student")]
        [HttpGet("list")]
        public async Task<ActionResult<ResultResponse<dynamic>>> GetAll()
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busStudent.BGetAll();
                response.Result = temp.Result;
                response.HasError = temp.HasError;
                response.HttpCode = temp.HttpCode;
                response.Message = temp.Message;
            }
            catch (Exception ex)
            {
                response.ErrorCode = metodo.iCodigoError;
                response.SetError(ex);

            }
            return StatusCode((int)response.HttpCode, response);
        }

        [SwaggerOperation(Summary = "Get one Student",
            Description = "Get one Student")]
        [HttpGet("{IdStudent}")]
        public async Task<ActionResult<ResultResponse<dynamic>>> Get(int IdStudent)
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busStudent.BGet(IdStudent);
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

        [SwaggerOperation(Summary = "Delete Student",
           Description = "Delete Grade by IdStudent")]
        [HttpDelete("{IdStudent}")]
        public async Task<ActionResult<IMDResponse<dynamic>>> Delete(int IdStudent)
        {
            ResultResponse<object> response = new ResultResponse<object>();

            try
            {
                var temp = await _busStudent.BDelete(IdStudent);
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

    }
}
