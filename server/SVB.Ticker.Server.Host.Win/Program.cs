using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Common.Logging;
using Microsoft.Owin.Hosting;

namespace SVB.Ticker.Server.Host.Win
{
  class Program
  {
    private static readonly ILog Log = LogManager.GetLogger<Program>();

    static void Main(string[] args)
    {
      const string baseAddress = "http://localhost:8090/";

      // This starts the OWIN host using the application startup
      // logic in the Startup class. See Startup for the example of
      // how to set up OWIN Web API.
      WebApp.Start<Startup>(baseAddress);

      if (Debugger.IsAttached)
      {
        Console.WriteLine();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
      }
    }

    private static FileInfo GetLoggingConfig()
    {
      string loggingConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logging.config");

      return File.Exists(loggingConfigFilePath) ? new FileInfo(loggingConfigFilePath): null;
    }
  }
}
