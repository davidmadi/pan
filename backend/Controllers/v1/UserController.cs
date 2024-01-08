using System;
using Library.Envelope;
using Library.Back.Calculator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Entity.Access;

namespace BackApi.Controllers.v1;

[ApiController]
[Route("v1/api/tax")]

public class UserController : ControllerBase
{
    [HttpGet("user/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<User>> Get(string email, string password)
    {
        try {
            bool withCache = Request.Headers["Pragma"] != "no-cache" &&
              Request.Headers["Cache-Control"] != "no-cache";

            var db = new User();
            db.email = email;
            db.password = password;
            db.model.SaveChanges();
            return new Response<User>(){
                Result = db,
                Success = true
            };
        }
        catch (Exception e) {
            Library.Logging.LogManager.EnqueueException(e, null);
            return NotFound(new Response<IncomeTaxResult>(){
                Success = false,
                Message = e.Message
            });
        }
    }    
    
}
