using System.Text.Json.Serialization;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Library.Entity.Access
{
  public class User
  {
    [JsonIgnore]
    public Model model { get; set; }
    public User(){
      model = new Model();
    }
    public int id{get{return model.id;}set{model.id = value;}}
    public string? name{get{return model.name;}set{model.name = value;}}
    public string? email{get{return model.email;}set{model.email = value;}}
    public string? password{get{return model.password;}set{model.password = value;}}

    public async void Save(){
      var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
      await using var dataSource = NpgsqlDataSource.Create(connectionString);
      var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=localhost;Username=test;Password=test");

    }

    public class Model : DbContext {
      public int id{get;set;}
      public string? name{get;set;}
      public string? email{get;set;}
      public string? password {get;set;}
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=postgres");
      }
    }

  }
}