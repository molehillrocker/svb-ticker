using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace SVB.Ticker.Server.Rest
{
  public class CoreModule : Module
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
      base.Load(builder);
    }
  }
}