using System;
using System.Web.Http;

namespace SVB.Ticker.Server.Rest.Contract
{
  [AllowAnonymous]
  public class ResultController : ApiController
  {
    [HttpGet]
    [Route("result")]
    public ResultSetRow[] GetResult()
    {
      return new[]
      {
        new ResultSetRow{Position=1, FirstName="John", LastName="Doe", Points=222.2f},
        new ResultSetRow{Position=2, FirstName="Jane", LastName="Doe", Points=217.9f},
        new ResultSetRow{Position=3, FirstName="Jim", LastName="Doe", Points=194.5f}
      };
    }

    public class ResultSetRow
    {
      public int Position { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public float? Points { get; set; }

      public TimeSpan? Runtime { get; set; }
    }
  }
}
