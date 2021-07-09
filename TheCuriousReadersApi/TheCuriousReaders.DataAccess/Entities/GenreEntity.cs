using System.Collections.Generic;

namespace TheCuriousReaders.DataAccess.Entities
{
    public class GenreEntity
    {
        public GenreEntity()
        {
            Books = new List<BookEntity>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BookEntity> Books { get; set; }
    }
}
