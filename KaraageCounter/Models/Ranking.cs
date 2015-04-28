using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KaraageCounter.Models
{
    public class Ranking
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int KaraageCount { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
    }
}