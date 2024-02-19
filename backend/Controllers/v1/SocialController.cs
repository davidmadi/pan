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
public class SocialController : ControllerBase
{
    [HttpPost("network/persist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<NetworkView>> NetworkPersist(NetworkView network, bool delete)
    {
        try
        {
            var db = new PostgresContext();
            var result = new Response<NetworkView>()
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
                    db.Networks.Add(new Library.Entity.Access.Network(){
                        PrimaryId = network.Primary.Id,
                        FriendId = network.Friend.Id,
                        Relationship = network.Relationship
                    });
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


    [HttpGet("network/list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Response<List<NetworkView>>> NetworkList(int userId)
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
                orderby net.network.Id
                select new NetworkView(){
                    Id = net.network.Id,
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
