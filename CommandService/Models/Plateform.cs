using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommandService.Models
{
    public class Plateform
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int PlateformId { get; set; }
        public ICollection<Command> Commands { get; set; } = new List<Command>();   
    }
}
