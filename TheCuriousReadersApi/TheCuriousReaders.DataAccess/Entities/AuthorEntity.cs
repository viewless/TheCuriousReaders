using System.Collections.Generic;

namespace TheCuriousReaders.DataAccess.Entities
{
    public class AuthorEntity
    {
        public AuthorEntity()
        {
            Books = new List<BookEntity>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BookEntity> Books { get; set; }
    }
}
