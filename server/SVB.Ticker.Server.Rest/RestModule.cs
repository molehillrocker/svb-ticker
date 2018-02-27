using Autofac;
using Autofac.Integration.WebApi;

namespace SVB.Ticker.Server.Core
{
  public class RestModule : Module
  {
    /// <summary>
    /// Override to add registrations to the container.
    /// </summary>
    /// <remarks>
    /// Note that the ContainerBuilder parameter is unique to this module.
    /// </remarks>
    /// <param name="builder">The builder through which components can be
    ///             registered.</param>
    protected override void Load(ContainerBuilder builder)
    {
      // Contract
      RegisterTypesFromPackageContract(builder);
      // Operation
      RegisterTypesFromPackageOperation(builder);
    }

    private static void RegisterTypesFromPackageContract(ContainerBuilder builder)
    {
      // Register all ApiControllers
      builder.RegisterApiControllers(System.Reflection.Assembly.GetExecutingAssembly());

      // Register our filter dispatcher
      //builder.RegisterType<FilterDispatcher>()
      //  .AsWebApiAuthenticationFilterFor<ApiController>()
      //  .AsWebApiAuthorizationFilterFor<ApiController>()
      //  .AsWebApiActionFilterFor<ApiController>()
      //  .AsWebApiExceptionFilterFor<ApiController>();

      // Register our custom filters
      //builder.RegisterType<AuthenticationFilterImpl>().As<ICustomAuthenticationFilter>();
      //builder.RegisterType<ExceptionFilterImpl>().As<ICustomExceptionFilter>();
    }

    private static void RegisterTypesFromPackageOperation(ContainerBuilder builder)
    {
    }
  }
}