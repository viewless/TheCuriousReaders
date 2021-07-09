using System;

namespace TheCuriousReaders.Models.ResponseModels
{
    public class NewBookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverUri { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public AuthorResponse Author { get; set; }
        public GenreResponse Genre { get; set; }
    }
}
