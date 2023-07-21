namespace Class2107.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Book>? Books { get; set; }

    }
}
