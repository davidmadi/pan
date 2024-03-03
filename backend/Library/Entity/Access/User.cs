using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Npgsql;

namespace Library.Entity.Access
{
  public class PostgresContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Network> Networks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var variable = Variable.GetInstance();
      // optionsBuilder.UseNpgsql($"Host={variable.PostgresHost};Username={variable.PostgresUser};Password={variable.PostgresPassword};Database={variable.PostgresDB}");
      optionsBuilder.UseNpgsql($"Host=localhost;Username=postgres;Password=postgres;Database=postgres");
    }
  }

  public class User
  {
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? ProfilePicture { get; set; }

    [NotMapped]
    public List<string>? Settings { 
      get
      {
        if (DBSettings != null) {
          return JsonSerializer.Deserialize<List<string>>(this.DBSettings);
        }
        return new List<string>();
      } 
      set
      {
        this.DBSettings = JsonSerializer.Serialize(value);
      }
    }
    
    [JsonIgnore]
    [Column("Settings")]
    public string? DBSettings { get; set; }

    [JsonIgnore]
    public string? Password { get; set; }

    public int Save(PostgresContext db){
      this.DBSettings = JsonSerializer.Serialize(this.Settings);
      return db.SaveChanges();
    }
  }

}
