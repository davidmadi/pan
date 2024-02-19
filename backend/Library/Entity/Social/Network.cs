using System.Text.Json.Serialization;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Npgsql;

namespace Library.Entity.Access
{
  public class Network
  {
    public int Id { get; set; }
    public int PrimaryId { get; set; }
    public int FriendId { get; set; }
    public string? Relationship { get; set; }
  }

  public class NetworkView 
  {
    public int Id { get; set; }
    public User? Primary { get; set; }
    public User? Friend { get; set; }
    public string? Relationship { get; set; }
  }

}
