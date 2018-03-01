using System.Web.Http;

namespace SVB.Ticker.Server.Rest.Contract
{
  [AllowAnonymous]
  public class HelpController : ApiController
  {
    [HttpGet]
    [Route("help")]
    public IHttpActionResult Help()
    {
      return Ok("This is the help!");
    }
  }
}
