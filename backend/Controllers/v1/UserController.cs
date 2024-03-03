using Library.Envelope;
using Library.Back.Calculator;
using Microsoft.AspNetCore.Mvc;
using Library.Entity.Access;
using Library.Entity.S3Bucket;
using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace BackApi.Controllers.v1;

[ApiController]
[Route("v1/")]
public class UserController : ControllerBase
{
    [HttpPost("user/create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<User>> Create(string email, string password)
    {
        try
        {
            var db = new PostgresContext();
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
    
    [HttpPost("user/update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<User>> Update(User user)
    {
        try
        {
            var db = new PostgresContext();
            var dbUser = db.Users.First(u => u.Id == user.Id);
            if (dbUser != null) {
                dbUser.Email = user.Email;
                dbUser.FullName = user.FullName;
                dbUser.ProfilePicture = user.ProfilePicture;
                dbUser.Settings = user.Settings;
                dbUser.Save(db);
            }
            return new Response<User>()
            {
                Result = dbUser,
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

            var db = new PostgresContext();
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

    [HttpGet("user/get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<User>> Find(int id)
    {
        try
        {
            var db = new PostgresContext();
            var usr = db.Users.FirstOrDefault(u => u.Id == id);
            return new Response<User>()
            {
                Result = usr,
                Success = usr != null
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

    [HttpGet("user/list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<List<User>>> List(int pageNumber, int pageSize)
    {
        try
        {
            int itemsToSkip = pageNumber * pageSize;
            var db = new PostgresContext();
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

    [HttpGet("user/find")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<List<User>>> Find(string name)
    {
        try
        {
            var db = new PostgresContext();
            var query = from usr in db.Set<User>()
                where EF.Functions.ILike(usr.FullName, $"%{name}%") ||
                    EF.Functions.ILike(usr.Email, $"%{name}%")
                select usr;
            return new Response<List<User>>()
            {
                Result = query.ToList(),
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
