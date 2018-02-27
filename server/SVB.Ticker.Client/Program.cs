using System;
using System.Net.Http;

namespace SVB.Ticker.Client
{
  class Program
  {
    const string baseAddress = "http://localhost:8090/";

    static void Main(string[] args)
    {
      HttpClient client = new HttpClient();
      var response = client.GetAsync(baseAddress + "help").Result;

      Console.WriteLine(response);
      Console.WriteLine(response.Content.ReadAsStringAsync().Result);

      Console.WriteLine();
      Console.WriteLine("Press any key to exit.");
      Console.ReadKey();
    }
  }
}
