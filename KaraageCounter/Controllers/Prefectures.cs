using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaraageCounter.Controllers
{
    public class Prefectures
    {
        public Response response { get; set; }

        public class Response
        {
            public IEnumerable<string> prefecture { get; set; }
        }
    }
}