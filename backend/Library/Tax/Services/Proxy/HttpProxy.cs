using System.Text.Json;
using System.Text.Json.Serialization;
using Library.Logging;

public static class HttpProxy {

  public static T? HttpJsonCall<T>(object? request, string url, string source) {

    try
    {
      HttpClient client = new HttpClient();
      var stringTask = client.GetStringAsync(url);
      stringTask.Wait();

      var options = new JsonSerializerOptions()
      {
          NumberHandling = JsonNumberHandling.AllowReadingFromString |
          JsonNumberHandling.WriteAsString
      };

      TraceManager.EnqueueTrace(request, stringTask.Result, null, source);
      if (stringTask.Result != null) {
        return JsonSerializer.Deserialize<T>(stringTask.Result, options);
      }
      return default(T);
    }
    catch(Exception e) {
      TraceManager.EnqueueTrace(request, null, e.ToString(), source);
      LogManager.EnqueueException(e, source);
      return default(T);
    }
  }

}