namespace LearnMVC.Models
{
    public class AuthorBookView
    {
        public Book? Book { get; set; }
        public List<Author>? Authors { get; set; }
        public int AuthorId { get; set; }
    }
}
