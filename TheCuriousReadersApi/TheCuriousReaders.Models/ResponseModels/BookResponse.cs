namespace TheCuriousReaders.Models.ResponseModels
{
    public class BookResponse
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CoverUri { get; set; }

        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }

        public AuthorResponse Author { get; set; }

        public GenreResponse Genre { get; set; }
    }
}
