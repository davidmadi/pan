namespace Library.Logging;

internal static class LogManager {

  private static Queue<LogRecord> logQueue = new Queue<LogRecord>();

  static LogManager() {
    Thread thread = new Thread(new ThreadStart(AsyncLogSave));
    thread.Start();
  }

  private static void AsyncLogSave()
  {
    while (true) {
      int count = 0;
      LogRecord? record;
      if (count < 10 && logQueue.TryDequeue(out record)){
        //Insert record in DB
        //Deserialize
        var sb = new System.Text.StringBuilder();
        sb.Append("[LOG] - " + record.Created.ToString());
        if(record.Source != null){
          sb.Append("|" + record.Source);
        }
        if(record.LogType != null){
          sb.Append("|" + record.LogType);
        }
        if(record.Message != null){
          sb.Append("|" + record.Message);
        }
        if(record.Context != null){
          sb.Append("|" + record.Context);
        }
        Console.WriteLine(sb.ToString());
        count++;
      }
      Thread.Sleep(5000);
    }
  }  

  public static void EnqueueException(Exception e, string? source) {
    EnqueueLog("Exception", e.Message, e.StackTrace?.ToString(), source);
  }

  public static void EnqueueLog(string? logType, object? message, string? context, string? source) {
    //Table
    //Id PK
    //Request varchar
    //Response varchar
    //Error varchar
    //Created datetime
    //TODO: 
    logQueue.Enqueue(new LogRecord(){
      LogType = logType,
      Message = message,
      Context = context,
      Source = source,
      Created = DateTime.Now
    });
  }  
}

internal class LogRecord {

  public string? LogType { get;set;}
  public object? Message { get; set; } 
  public object? Context { get; set; }
  public DateTime Created { get; set; }
  public string? Source { get; set; }
}