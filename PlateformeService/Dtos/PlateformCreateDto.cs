using System.ComponentModel.DataAnnotations;

namespace PlateformeService.Dtos
{
    public class PlateformCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Cost { get; set; }
    }
}
