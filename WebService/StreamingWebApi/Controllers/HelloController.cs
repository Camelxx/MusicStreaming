using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StreamingWebApi
{
    public class HelloController : ApiController
    {
        // GET api/hello
        public string[] Get()
        {
            Console.WriteLine("Get Runnig");
            return new string[] { "Hello", "World" };
        }

        // GET api/hello/bob
        public string Get(string name)
        {
            return $"Hello, {name}!";
        }
    }
}
