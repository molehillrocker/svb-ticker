using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Common.Logging;
using Owin;
using SVB.Ticker.Server.Core;
using SVB.Ticker.Server.Rest;

namespace SVB.Ticker.Server.Host.Win
{
  public class Startup
  {
    private const string ApplicationName = "SVB Ticker";

    private static readonly ILog Log = LogManager.GetLogger<Startup>();

    // This code configures Web API. The Startup class is specified as a type
    // parameter in the WebApp.Start method.
    public void Configuration(IAppBuilder appBuilder)
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
      HttpConfiguration config = new HttpConfiguration();
      WebApiConfiguration.Configure(config);
      builder.RegisterHttpRequestMessage(config);
      Log.Info(x => x("Successfully configured WebAPI."));

      Log.Info(x => x("Registering WebAPI filters..."));
      builder.RegisterWebApiFilterProvider(config);
      Log.Info(x => x("Successfully registered WebAPI filters."));

      IContainer container = builder.Build();
      config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

      appBuilder.UseAutofacMiddleware(container);
      appBuilder.UseAutofacWebApi(config);
      appBuilder.UseWebApi(config);

      Log.Info(x => x("Successfully initialized Autofac container."));

      Log.Info(x => x("{0} started.", ApplicationName));
    }
  }
}
