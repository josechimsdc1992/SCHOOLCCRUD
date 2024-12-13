using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[teacher]")]
    public class TeacherController : ControllerBase
    {
        
        private readonly ILogger<TeacherController> _logger;
        private readonly IDatTeacher _datTeacher;

        public TeacherController(ILogger<TeacherController> logger, IDatTeacher datTeacher)
        {
            _logger = logger;
            _datTeacher = datTeacher;
        }
        [HttpPost]
        [SwaggerOperation(Summary = "Add or update Teacher",
            Description = "Add or update Teache dependency on IdTeacher")]
        [IMDMetodo(20231026100640, 20231026100652)]
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
                        ResultResponse<EntTeacher> res = await _busTeacher.BCreate(ent);
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
                        EntTeacher ent = new EntTeacher();
                        ent.Name = request.Name;
                        ent.SurName = request.SurName;
                        ResultResponse<EntTeacher> res = await _busTeacher.BUpdate(ent);
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

        [SwaggerOperation(Summary = "Lista de Teacher",
            Description = "Regresa una lista de Teacher")]
        [HttpGet("list")]
        [IMDMetodo(67823462368441, 67823462367664)]
        public async Task<ActionResult<ResultResponse<dynamic>>> GetAll()
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busTeacher.BGetAll();
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

        [SwaggerOperation(Summary = "Get one Teacher",
            Description = "Get one Teacher")]
        [HttpGet("{IdTeacher}")]
        [IMDMetodo(67823462259630, 67823462258830)]
        public async Task<ActionResult<ResultResponse<dynamic>>> Get(int IdTeacher)
        {
            ResultResponse<object> response = new ResultResponse<object>();


            try
            {
                var temp = await _busTeacher.BGet(IdTeacher);
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

        [SwaggerOperation(Summary = "Delete Teacher",
           Description = "Delete Teacher by IdTeacher")]
        [HttpDelete("{IdTeacher}")]
        public async Task<ActionResult<ResultResponse<dynamic>>> Delete(int IdTeacher)
        {
            ResultResponse<object> response = new ResultResponse<object>();

            try
            {
                var temp = await _busTeacher.BDelete(IdTeacher);
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
