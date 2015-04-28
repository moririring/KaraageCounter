using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

namespace KaraageCounter.Models
{
    public class Karaage
    {
        [Required]
        public int KaraageID { get; set; }

        [Required]
        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
        public string IpAddress { get; set; }
        public string SessionId { get; set; }
        public int RandNumber { get; set; }
    }



}