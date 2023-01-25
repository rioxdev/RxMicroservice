﻿using System.ComponentModel.DataAnnotations;

namespace PlateformeService.Models
{
    public class Plateform
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Cost { get; set; }
    }
}
