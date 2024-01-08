namespace Library.Logging;

internal static class TraceManager {

  private static Queue<TraceRecord> traceQueue = new Queue<TraceRecord>();
  static TraceManager() {

    Thread thread = new Thread(new ThreadStart(AsyncTraceSave));
    thread.Start();

  }

  private static void AsyncTraceSave()
  {
    while (true) {
      int count = 0;
      TraceRecord? record;
      if (count < 10 && traceQueue.TryDequeue(out record)){
        //Insert record in DB
        //Deserialize
        var sb = new System.Text.StringBuilder();
        sb.Append("[TRACE] - " + record.Created.ToString());
        Console.ForegroundColor = ConsoleColor.Green;
        if(record.Request != null){
          sb.Append("|" + record.Request + "|");
        }
        if(record.Response != null){
          sb.Append(record.Response + "|");
        }
        if(record.Error != null){
          Console.ForegroundColor = ConsoleColor.Red;
          sb.Append(record.Error + "|");
        }
        Console.WriteLine(sb.ToString());
        count++;
      }
      Thread.Sleep(5000);
    }
  }

  public static void EnqueueTrace(object? request, object? response, string? error, string? source) {
    //Table
    //Id PK
    //Request varchar
    //Response varchar
    //Error varchar
    //Created datetime
    //TODO: 
    traceQueue.Enqueue(new TraceRecord(){
      Request = request,
      Response = response,
      Error = error,
      Source = source,
      Created = DateTime.Now
    });
  }
}

internal class TraceRecord {

  public object? Request { get; set; } 
  public object? Response { get; set; }
  public object? Error { get; set; }
  public DateTime Created { get; set; }
  public string? Source { get; internal set; }
}
