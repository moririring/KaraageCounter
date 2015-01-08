using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaraageCounter.Controllers
{



    public class Geoip
    {
        public float longitude { get; set; }
        public int latitude { get; set; }
        public string asn { get; set; }
        public string offset { get; set; }
        public string ip { get; set; }
        public string area_code { get; set; }
        public string continent_code { get; set; }
        public string dma_code { get; set; }
        public string city { get; set; }
        public string timezone { get; set; }
        public string region { get; set; }
        public string country_code { get; set; }
        public string isp { get; set; }
        public string country { get; set; }
        public string country_code3 { get; set; }
        public string region_code { get; set; }
    }
}