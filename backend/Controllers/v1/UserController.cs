using Library.Envelope;
using Library.Back.Calculator;
using Microsoft.AspNetCore.Mvc;
using Library.Entity.Access;
using Library.Entity.S3Bucket;
using System.Net;

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
            }
            db.SaveChanges();
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


    [HttpPost("user/uploadPicture")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<User>> Update(int userId, IFormFile file)
     {
        try
        {
            var db = new PostgresContext();
            var dbUser = db.Users.First(u => u.Id == userId);
            if (dbUser != null) {
                var s3File = S3File.Upload(file);
                dbUser.ProfilePicture = s3File.Url;
                db.SaveChanges();
                return new Response<User>()
                {
                    Result = dbUser,
                    Success = true
                };
            } 
            else {
                return new Response<User>()
                {
                    Message = "No user found"
                };
            }
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

    [HttpPost("user/network")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<Network>> Network(Network network, bool delete)
    {
        try
        {
            var db = new PostgresContext();
            var result = new Response<Network>()
            {
                Success = false
            };
            if (delete){
                var dbNet = db.Networks.FirstOrDefault(n => n.Id == network.Id);
                if (dbNet != null) {
                    db.Remove(dbNet);
                    db.SaveChanges();
                    result.Success = true;
                }
            } else {
                if (network.Id > 0) {
                    var dbNet = db.Networks.FirstOrDefault(n => n.Id == network.Id);
                    if (dbNet != null) {
                        dbNet.Relationship = network.Relationship;
                        db.SaveChanges();
                        result.Success = true;
                    }
                } else {
                    db.Networks.Add(network);
                    db.SaveChanges();
                    result.Success = true;
                }
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


    [HttpGet("user/network")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<List<NetworkView>>> Network(int userId)
    {
        try
        {
            var db = new PostgresContext();
            var listNetwork = from user in db.Set<User>()
                join network in db.Set<Network>() 
                    on user.Id equals network.PrimaryId
                where user.Id == userId
                select new { user, network };
            var friendQuery = from friend in db.Set<User>()
                join net in listNetwork
                    on friend.Id equals net.network.FriendId
                select new NetworkView(){
                    Primary = net.user,
                    Friend = friend,
                    Relationship = net.network.Relationship
                };

            return new Response<List<NetworkView>>()
            {
                Result = friendQuery.ToList(),
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
    }}
