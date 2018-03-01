using System;
using System.IO;
using Common.Logging;
using Topshelf;

namespace SVB.Ticker.Server.Host.Win
{
  class Program
  {
    private static readonly ILog Log = LogManager.GetLogger<Program>();

    static void Main()
    {
      TopshelfExitCode returnCode = HostFactory.Run(x =>
      {
        x.Service<WebService>(s =>
        {
          s.ConstructUsing(name => new WebService());
          s.WhenStarted(ws => ws.Start());
          s.WhenStopped(ws => ws.Stop());
        });

        x.RunAsLocalService();

        x.SetDescription("SVB-Ticker service for live results.");
        x.SetDisplayName("SVB-Ticker");
        x.SetServiceName("SVB-Ticker");
      });

      Environment.ExitCode = (int)Convert.ChangeType(returnCode, returnCode.GetTypeCode());
    }

    private static FileInfo GetLoggingConfig()
    {
      string loggingConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logging.config");

      return File.Exists(loggingConfigFilePath) ? new FileInfo(loggingConfigFilePath) : null;
    }
  }
}
