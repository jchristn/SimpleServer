using System;
using System.Text.Json;
using System.Threading.Tasks;
using WatsonWebserver;

namespace SimpleServer
{
    class Program
    {
        static bool _DisplayRequest = false;
        static bool _DisplayPayload = false;
        static bool _DisplaySummary = true;
        static bool _PrettyJson = false;
        static string _Hostname = "localhost";
        static int _Port = 8888;
        static bool _Ssl = false;
        static Server _Server = null;

        static void Main(string[] args)
        { 
            _Server = new Server(_Hostname, _Port, _Ssl, DefaultRequest);
            _Server.Start();
            Console.WriteLine((_Ssl ? "https:" : "http:") + "//" + _Hostname + ":" + _Port);
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

        static async Task DefaultRequest(HttpContext ctx)
        {
            if (_DisplayRequest)
            {
                Console.WriteLine(SerializeJson(ctx.Request, _PrettyJson));
                if (_DisplayPayload && !String.IsNullOrEmpty(ctx.Request.DataAsString))
                {
                    Console.WriteLine(ctx.Request.DataAsString);
                }
            }

            ctx.Response.ContentType = "application/json";
            await ctx.Response.Send(SerializeJson(ctx.Request, true));

            if (_DisplaySummary)
            {
                ctx.Timestamp.End = DateTime.UtcNow;

                Console.WriteLine(
                    ctx.Request.Source.IpAddress + ":" + ctx.Request.Source.Port + " " +
                    ctx.Request.Method.ToString() + " " + ctx.Request.Url.RawWithQuery + " " +
                    ctx.Request.ContentLength + " bytes " +
                    "(" + ctx.Timestamp.TotalMs + "ms)");
            }
        }

        static string SerializeJson(object obj, bool pretty)
        {
            if (obj == null) return null;
            if (!pretty)
            {
                return JsonSerializer.Serialize(obj);
            }
            else
            {
                return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            }
        }
    }
}
