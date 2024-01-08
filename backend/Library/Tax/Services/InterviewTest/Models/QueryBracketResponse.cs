public class QueryBracketResponse {
  public List<Bracket>? tax_brackets {get;set;}

  public List<QueryError>? errors {get;set;}
}

public class QueryError {
  public string? field { get; set; }
  public string? code { get; set; }
  public string? message { get; set; }
}

public class Bracket {
  public int? max { get; set; }
  public int? min { get; set; }
  public decimal rate {get;set;}

}
