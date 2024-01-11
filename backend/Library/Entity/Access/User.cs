using System.Text.Json.Serialization;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Npgsql;

namespace Library.Entity.Access
{
  public class UserContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=postgres");
    }
  }

  public class User
  {
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
  }

}
