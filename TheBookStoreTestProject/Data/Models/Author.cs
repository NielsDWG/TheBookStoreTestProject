using System.Collections.Generic;

namespace TheBookStoreTestProject.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}
