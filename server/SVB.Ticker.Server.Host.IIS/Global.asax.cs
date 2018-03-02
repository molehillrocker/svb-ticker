using System;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Common.Logging;
using SVB.Ticker.Server.Core;
using SVB.Ticker.Server.Host.IIS.Properties;
using SVB.Ticker.Server.Rest;
using SVB.Ticker.Server.Web;

namespace SVB.Ticker.Server.Host.IIS
{
  public class Global : System.Web.HttpApplication
  {
    private const string ApplicationName = "SVB-Ticker";

    private static readonly ILog Log = LogManager.GetLogger<Global>();

    static Global()
    {
      AppDomain.CurrentDomain.SetData("ApplicationName", ApplicationName);
    }

    protected void Application_Start(object sender, EventArgs e)
    {
      Log.Info(x => x("{0} starting...", ApplicationName));

      Log.Info(x => x("Initializing Autofac container..."));

      ContainerBuilder builder = new ContainerBuilder();

      Log.Info(x => x("Registering Autofac modules..."));
      // Register your service implementations.
      builder.RegisterModule<CoreModule>();
      builder.RegisterModule<RestModule>();
      Log.Info(x => x("Successfully registered Autofac modules."));

      Log.Info(x => x("Configuring WebAPI..."));
      GlobalConfiguration.Configure(x => WebApiConfiguration.Configure(x, Settings.Default.IsTraceMode, Settings.Default.AllowedOrigins,
        Settings.Default.AllowedHeaders, Settings.Default.AllowedMethods));
      HttpConfiguration configuration = GlobalConfiguration.Configuration;
      Log.Info(x => x("Successfully configured WebAPI."));

      Log.Info(x => x("Registering WebAPI filters..."));
      builder.RegisterWebApiFilterProvider(configuration);
      Log.Info(x => x("Successfully registered WebAPI filters."));

      IContainer container = builder.Build();
      configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

      Log.Info(x => x("Successfully initialized Autofac container."));

      Log.Info(x => x("{0} started.", ApplicationName));
    }

    protected void Application_End(object sender, EventArgs e)
    {
      Log.Info(x => x("{0} stopping...", ApplicationName));

      Log.Info(x => x("Shutting down Autofac container..."));
      GlobalConfiguration.Configuration.DependencyResolver.Dispose();
      Log.Info(x => x("Successfully shut down Autofac container."));

      Log.Info(x => x("{0} stopped.", ApplicationName));
    }
  }
}