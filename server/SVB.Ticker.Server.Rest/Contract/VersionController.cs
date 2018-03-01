using System.Reflection;
using System.Web.Http;

namespace SVB.Ticker.Server.Rest.Contract
{
  [AllowAnonymous]
  public class VersionController : ApiController
  {
    [HttpGet]
    [Route("version")]
    public IHttpActionResult Help()
    {
      return Ok($"v{Assembly.GetExecutingAssembly().GetName().Version}");
    }
  }
}
