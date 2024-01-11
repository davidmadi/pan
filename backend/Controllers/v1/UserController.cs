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
        try
        {
            bool withCache = Request.Headers["Pragma"] != "no-cache" &&
              Request.Headers["Cache-Control"] != "no-cache";

            var db = new UserContext();
            db.Users.Add(new User() { Email = email, Password = password });
            db.SaveChanges();
            return new Response<User>()
            {
                Result = db.Users.First(),
                Success = true
            };
        }
        catch (Exception e)
        {
            Library.Logging.LogManager.EnqueueException(e, null);
            return NotFound(new Response<IncomeTaxResult>()
            {
                Success = false,
                Message = e.Message
            });
        }
    }

}
