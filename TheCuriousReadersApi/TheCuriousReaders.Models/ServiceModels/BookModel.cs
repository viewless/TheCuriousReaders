using System;

namespace TheCuriousReaders.Models.ServiceModels
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CoverUri { get; set; }

        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }

        public DateTime CreatedAt { get; set; }

        public AuthorModel Author { get; set; }

        public GenreModel Genre { get; set; }
    }
}
