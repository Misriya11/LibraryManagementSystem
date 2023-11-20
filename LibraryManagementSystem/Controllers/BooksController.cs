using LibraryManagementSystem.Model;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]

    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DbHelper _db;
        public BooksController(LmsContext lmsContext)
        {
            _db = new DbHelper(lmsContext);
        }

        // GET: api/<LibraryManagementApiController>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(ApiResponse<List<Book>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.success;
            try
            {
                List<Book> data = _db.GetBooks().ToList();
               
                if (!data.Any()) {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse<List<Book>>(type, data));
            }
            catch (Exception ex) { 
                return BadRequest(ResponseHandler.GetExceptionResponse(ex)); }
        }

        // GET api/<LibraryManagementApiController>/5
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Book>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.success;
            try
            {
                Book? data = _db.GetBooksById(id);

                if (data==null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse<Book?>(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        [HttpPost]
        [Route("Request")]
        [ProducesResponseType(typeof(ApiResponse<BookLendingResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public IActionResult BookRequest([FromBody] BookLendingRequest model)
        {
            ResponseType type = ResponseType.success;
            try
            {
                var returnmodel = _db.BooklendingRequest(model);
                if (returnmodel is BorrowedBook)
                {
                    return Ok(ResponseHandler.GetAppResponse<BookLendingResponse>(type, BookLendingResponse.fromBorrowBook(returnmodel)));
                }
                else
                {
                    return Ok(ResponseHandler.GetAppResponse<BookLendingResponse>(type, BookLendingResponse.fromWaitList(returnmodel)));
                }
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
        [HttpPost]
        [Route("Return")]
        [ProducesResponseType(typeof(ApiResponse<BorrowedBook>),200)]
        [ProducesResponseType(typeof(ApiResponse<string>),400)]
        public IActionResult ReturnBook([FromBody] BookReturn model)
        {
            ResponseType type = ResponseType.success;
            try
            {
                BorrowedBook returnmodel = _db.BookReturn(model);
                return Ok(ResponseHandler.GetAppResponse<BorrowedBook>(type, returnmodel));

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
