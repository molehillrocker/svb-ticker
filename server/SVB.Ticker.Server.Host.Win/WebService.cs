using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVB.Ticker.Server.Host.Win
{
  public class WebService
  {
    private const ushort Port = 8090;

    private static readonly StartOptions StartOptions = new StartOptions { Port = Port };

    private IDisposable _webApp;

    public void Start()
    {
      if (_webApp == null)
      {
        _webApp = WebApp.Start<Startup>(StartOptions);
      }
    }

    public void Stop()
    {
      if (_webApp != null)
      {
        _webApp.Dispose();
      }
    }
  }
}
