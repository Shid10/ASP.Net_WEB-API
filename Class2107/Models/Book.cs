using System.ComponentModel.DataAnnotations;

namespace Class2107.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [MaxLength(25)]
        [MinLength(6, ErrorMessage = "title must be at least 6 characters long")]
        public string? Title { get; set; }

        public int AuthorId { get; set; }

        [Required(ErrorMessage = "ISBN Required")]
        public string? Isbn { get; set; }

    }
}
