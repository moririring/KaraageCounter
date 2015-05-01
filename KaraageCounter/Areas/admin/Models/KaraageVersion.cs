using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KaraageCounter.Areas.admin.Models
{
    public class KaraageVersion
    {
        public KaraageVersion()
        {
        }

        public int Id { get; set; }

        [DisplayName("リリース済")]
        public bool Released { get; set; }
        public float Version { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        [DisplayName("URL")]
        [DataType(DataType.Url)]
        public string Url { get; set; } // URL
    
    }
}