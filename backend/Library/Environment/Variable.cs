public class Variable 
{
  static Variable? _variable = null;
  public static Variable GetInstance(){
    if (_variable == null) {
      _variable = new Variable();
    }
    return _variable;
  }
  public string? PostgresHost { get{ return Get("POSTGRES_HOST"); }  }
  public string? PostgresDB { get{ return Get("POSTGRES_DB"); }  }
  public string? PostgresUser { get{ return Get("POSTGRES_USER"); }  }
  public string? PostgresPassword { get{ return Get("POSTGRES_PASSWORD"); }  }
  public static string? Get(string name) {
    return Environment.GetEnvironmentVariable(name);
  }

}