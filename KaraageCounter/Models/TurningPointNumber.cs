﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KaraageCounter.Models
{
    public class TurningPointNumber
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public DateTime GetTime { get; set; }
    }
}