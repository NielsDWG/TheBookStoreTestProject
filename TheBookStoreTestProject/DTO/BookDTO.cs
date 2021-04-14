namespace TheBookStoreTestProject.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }

        public int AuthorId { get; set; }
        public AuthorDTO Author { get; set; }        
    }
}
