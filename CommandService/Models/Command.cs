using System.ComponentModel.DataAnnotations;

namespace CommandService.Models
{
    public class Command
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string HowTo { get; set; }
        [Required]
        public string CommandLine { get; set; }
        [Required]
        public int PlateformId { get; set; }
        public Plateform Plateform { get; set; }
    }
}
