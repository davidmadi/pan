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
      var variable = Variable.GetInstance();
      optionsBuilder.UseNpgsql($"Host={variable.PostgresHost};Username={variable.PostgresUser};Password={variable.PostgresPassword};Database={variable.PostgresDB}");
    }
  }

  public class User
  {
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }

    [JsonIgnore]
    public string? Password { get; set; }
  }

}
