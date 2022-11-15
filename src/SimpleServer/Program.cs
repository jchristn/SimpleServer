using System;
using System.Text.Json;
using System.Threading.Tasks;
using WatsonWebserver;

namespace SimpleServer
{
    class Program
    {
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
            Console.WriteLine(SerializeJson(ctx.Request, true));
            if (!String.IsNullOrEmpty(ctx.Request.DataAsString)) Console.WriteLine(ctx.Request.DataAsString);
            ctx.Response.ContentType = "application/json";
            await ctx.Response.Send(SerializeJson(ctx.Request, true));
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
