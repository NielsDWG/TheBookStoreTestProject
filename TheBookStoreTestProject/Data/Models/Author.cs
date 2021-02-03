using System.Collections.Generic;
using System.Linq;

namespace TheBookStoreTestProject.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public virtual IEnumerable<Book> Books { get; set; }
    }
}
