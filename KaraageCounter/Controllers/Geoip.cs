using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaraageCounter.Controllers
{
    //都道府県

    //            string url = "http://express.heartrails.com/api/json?method=getPrefectures";
    //            using (var res = WebRequest.Create(url).GetResponse())
    //            {
    //                using (var resStream = res.GetResponseStream())
    //                {
    //                    var serializer = new DataContractJsonSerializer(typeof(Prefectures));
    //                    var prefectures = serializer.ReadObject(resStream) as Prefectures;
    //                    ViewBag.Prefecture = prefectures.response.prefecture;
    //                }
    //            }



//            string url = "http://www.telize.com/geoip/" + Request.UserHostAddress;
//            ViewBag.IpAddress = Request.UserHostAddress;
//            WebClient wc = new WebClient();
//            string html = wc.DownloadString(url);
//            if (html.Contains("\"city\""))
//            {
//                var city = html.Substring(html.IndexOf("\"city\"") + "\"city\"".Length + 2);
//                ViewBag.City = city.Substring(0, city.IndexOf(",") - 1);
//            }
//            if (html.Contains("\"region\""))
//            {
//                var region = html.Substring(html.IndexOf("\"region\"") + "\"region\"".Length + 2);
//                ViewBag.Region = region.Substring(0, region.IndexOf(",") - 1);
//            }
//            if (html.Contains("\"country\""))
//            {
//                var country = html.Substring(html.IndexOf("\"country\"") + "\"country\"".Length + 2);
//                ViewBag.Country = country.Substring(0, country.IndexOf(",") - 1);
//            }



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