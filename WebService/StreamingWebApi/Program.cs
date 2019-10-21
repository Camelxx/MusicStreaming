using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace StreamingWebApi
{
    class Program
    {
        static string host = "http://localhost:8040";

        static void Main(string[] args)
        {
            using (WebApp.Start(host))
            {
                Console.WriteLine("Web Server is running on {0} " , host);
                Console.WriteLine("Press any key to quit.");
                Console.ReadLine();
            }
        }
    }
}




//var config = new HttpSelfHostConfiguration("http://localhost:8040");

// config.Routes.MapHttpRoute(
//     "API Default", "api/{controller}/{id}",
//     new { id = RouteParameter.Optional });

// using (HttpSelfHostServer server = new HttpSelfHostServer(config))
// {
//     server.OpenAsync().Wait();
//     Console.WriteLine("Press Enter to quit.");
//     Console.ReadLine();
// }