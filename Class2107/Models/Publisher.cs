using System.ComponentModel.DataAnnotations;

namespace Class2107.Models
{
    public class Publisher
    {
        [Key]
        public int PulisherId { get; set; }
        public string? name { get; set; }
        public string? Mobile { get; set; }


    }
}
