using System;
using WatsonWebserver;
using Newtonsoft.Json;

namespace SimpleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server s = new Server("localhost", 8888, false, DefaultRequest);
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

        static HttpResponse DefaultRequest(HttpRequest req)
        {
            Console.WriteLine(req.ToString());
            return new HttpResponse(req, 200, null, "application/json", SerializeJson(req, true));
        }

        static string SerializeJson(object obj, bool pretty)
        {
            if (obj == null) return null;
            string json;
            if (pretty)
            {
                json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented,
                  new JsonSerializerSettings
                  {
                      NullValueHandling = NullValueHandling.Ignore,
                      DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                  });
            }
            else
            {
                json = JsonConvert.SerializeObject(obj,
                  new JsonSerializerSettings
                  {
                      NullValueHandling = NullValueHandling.Ignore,
                      DateTimeZoneHandling = DateTimeZoneHandling.Utc
                  });
            }
            return json;
        }
    }
}
