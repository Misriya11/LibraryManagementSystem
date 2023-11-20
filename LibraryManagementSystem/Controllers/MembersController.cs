using LibraryManagementSystem.Model;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace LibraryManagementSystem.Controllers
{

    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]

    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly DbHelper _db;
        public MembersController(LmsContext lmsContext)
        {
            _db = new DbHelper(lmsContext);
        }
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Member>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public IActionResult GetMember(int id)
        {
            ResponseType type = ResponseType.success;
            try
            {
                Member? data = _db.GetMemberById(id);

                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse<Member?>(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        // POST api/<LibraryManagementApiController>
        [HttpPost]
        [Route("Add")]
        [ProducesResponseType(typeof(ApiResponse<Member>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public IActionResult Post([FromBody] Member model)
        {
            ResponseType type = ResponseType.success;
            try
            {
                model = _db.SaveMember(model);
                return Ok(ResponseHandler.GetAppResponse<Member>(type, model));
            }
            catch (CustomError ex)
            {
                return BadRequest(ResponseHandler.GetCustomExceptionResponse(ex));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        
    }
}
