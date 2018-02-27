using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;
using Common.Logging;
using SVB.Ticker.Server.Common.System.Extension;
using SVB.Ticker.Server.Host.Win.Properties;

namespace SVB.Ticker.Server.Host.Win
{
  public class CustomCorsPolicyProvider : ICorsPolicyProvider
  {
    private static readonly ILog Log = LogManager.GetLogger<CustomCorsPolicyProvider>();

    #region Implementation of ICorsPolicyProvider

    /// <summary>
    /// Gets the <see cref="T:System.Web.Cors.CorsPolicy"/>.
    /// </summary>
    /// <returns>
    /// The <see cref="T:System.Web.Cors.CorsPolicy"/>.
    /// </returns>
    /// <param name="request">The request.</param><param name="cancellationToken">The cancellation token.</param>
    public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      CorsPolicy corsPolicy = new CorsPolicy
      {
        SupportsCredentials = true,
        PreflightMaxAge = 3600,
      };

      IEnumerable<string> allowedOrigins = CreateAllowedOrigins();
      corsPolicy.AllowAnyOrigin = !allowedOrigins.Any();
      if (!corsPolicy.AllowAnyOrigin)
      {
        corsPolicy.Origins.AddAll(allowedOrigins);
      }

      IEnumerable<string> allowedHeaders = CreateAllowedHeaders();
      corsPolicy.AllowAnyHeader = !allowedHeaders.Any();
      if (!corsPolicy.AllowAnyHeader)
      {
        corsPolicy.Headers.AddAll(allowedHeaders);
      }

      IEnumerable<string> allowedMethods = CreateAllowedMethods();
      corsPolicy.AllowAnyMethod = !allowedMethods.Any();
      if (!corsPolicy.AllowAnyMethod)
      {
        corsPolicy.Methods.AddAll(allowedMethods);
      }

      Log.Trace(x => x("CORS policy: {0}", corsPolicy));

      return Task.FromResult(corsPolicy);
    }

    #endregion

    private static string[] CreateAllowedOrigins()
    {
      if (string.IsNullOrEmpty(Settings.Default.AllowedOrigins))
      {
        return new string[0];
      }

      string[] origins = Settings.Default.AllowedOrigins.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
      string[] allowedOrigins = origins
        .Select(x => string.Format("http://{0}", x))
        .Concat(origins.Select(x => string.Format("https://{0}", x)))
        .ToArray();

      Log.Trace(x => x("Allowed HTTP origins: ", string.Join(", ", allowedOrigins)));

      return allowedOrigins;
    }

    private static string[] CreateAllowedHeaders()
    {
      if (string.IsNullOrEmpty(Settings.Default.AllowedHeaders))
      {
        return new string[0];
      }

      string[] allowedHeaders = Settings.Default.AllowedHeaders.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

      Log.Trace(x => x("Allowed HTTP headers: ", string.Join(", ", allowedHeaders)));

      return allowedHeaders;
    }

    private static string[] CreateAllowedMethods()
    {
      if (string.IsNullOrEmpty(Settings.Default.AllowedMethods))
      {
        return new string[0];
      }

      string[] allowedMethods = Settings.Default.AllowedMethods.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

      Log.Trace(x => x("Allowed HTTP methods: ", string.Join(", ", allowedMethods)));

      return allowedMethods;
    }
  }
}