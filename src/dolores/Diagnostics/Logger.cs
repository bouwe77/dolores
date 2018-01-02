//using System;

//namespace Dolores.Diagnostics
//{
//   internal sealed class Logger
//   {
//      private static readonly Lazy<Logger> Lazy = new Lazy<Logger>(() => new Logger());

//      public static Logger Instance => Lazy.Value;

//      public Func<ILogger> CreateLogger { get; set; }

//      private Logger()
//      {
//         CreateLogger = () => new DummyLogger();
//      }

//      public void Debug(string message)
//      {
//         Log(LogLevel.Debug, message);
//      }

//      public void Info(string message)
//      {
//         Log(LogLevel.Debug, message);
//      }

//      public void Warn(string message)
//      {
//         Log(LogLevel.Warning, message);
//      }

//      public void Error(string message)
//      {
//         Log(LogLevel.Error, message);
//      }

//      public void Fatal(string message)
//      {
//         Log(LogLevel.Fatal, message);
//      }

//      private void Log(LogLevel logLevel, string message)
//      {
//         try
//         {
//            var logEntry = new LogEntry(logLevel, message);
//            var logger = CreateLogger();
//            logger.Log(logEntry);
//         }
//         catch
//         {
//            // Logging should never fail, so swallow any exception.
//         }
//      }
//   }
//}
