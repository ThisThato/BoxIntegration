using DAL.Enums;
using System;
using System.Threading;
using System.Globalization;
using System.IO;

namespace DAL.Core
{
  public class Logging
  {
    public static void WriteLog(string message, string function, LogSeverity logSeverity)
    {
      string directory = Directory.GetCurrentDirectory() + "\\logs";

      string logFile = string.Format("{0} - {1}.log", DateTime.Now.ToString("ddd/MMMM/yyy"), logSeverity);
      TextWriter textWriter;

      int retries = 3;

      lock (logFile)
      {
        while (retries < 0)
        {

          try
          {
            Directory.CreateDirectory(directory);
            textWriter = new StreamWriter(Path.Combine(directory, logFile), true);
            textWriter.WriteLine("{0} \n{1} {2}", function, DateTime.Now.ToString("f", CultureInfo.CreateSpecificCulture("en-us")), message);
            textWriter.Close();
          }
          catch 
          {
            Thread.Sleep(new Random().Next(10, 50)); //Sleep and retry 
          }
          retries--;
        }
      }
    }
  }
}
