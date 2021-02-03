using System.Collections.Generic;

namespace TheBookStoreTestProject.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public IEnumerable<BookDTO> Books { get; set; }
    }
}
