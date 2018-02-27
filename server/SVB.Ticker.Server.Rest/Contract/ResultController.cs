using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SVB.Ticker.Server.Rest.Contract
{
  [AllowAnonymous]
  public class ResultController : ApiController
  {
    [HttpGet]
    [Route("help")]
    public IHttpActionResult Help()
    {
      return Ok("This is the help!");
    }
  }
}
