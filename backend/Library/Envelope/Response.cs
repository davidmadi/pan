namespace Library.Envelope;

public class Response<T>
{
  public T? Result { get; set; }
  public bool Success { get; set; }
  public string? Message {get;set;}

}