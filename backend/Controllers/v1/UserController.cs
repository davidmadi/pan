using System;
using Library.Envelope;
using Library.Back.Calculator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Entity.Access;
using Microsoft.AspNetCore.Http.Features;

namespace BackApi.Controllers.v1;

[ApiController]
[Route("v1/api/tax")]

public class UserController : ControllerBase
{
    [HttpPost("user/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<User>> Create(string email, string password)
    {
        try
        {
            var db = new UserContext();
            var newUser = new User() { Email = email, Password = password };
            db.Users.Add(newUser);
            db.SaveChanges();
            return new Response<User>()
            {
                Result = newUser,
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

    [HttpPost("user/login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<User>> Login(string email, string password)
    {
        try
        {
            var result = new Response<User>()
            {
                Success = true
            };

            var db = new UserContext();
            var listUsers = db.Users.Where(u => u.Email == email && u.Password == password);
            if (listUsers.Count() > 0) {
                result.Result = listUsers.First();
                result.Success = true;
            }
            return result;
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

    [HttpGet("user/list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<List<User>>> List(int pageNumber, int pageSize)
    {
        try
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
            var db = new UserContext();
            var listUsers = db.Users.OrderBy(item => item.Id) // Assuming you want to order by some property like Id
            .Skip(itemsToSkip)
            .Take(pageSize);
            return new Response<List<User>>()
            {
                Result = listUsers.ToList(),
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
