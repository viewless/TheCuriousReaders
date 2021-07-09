using TheCuriousReaders.Models.ResponseModels;

namespace TheCuriousReaders.Models.ServiceModels
{
    public class SearchBookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverUri { get; set; }
        public AuthorModel Author { get; set; }
        public GenreModel Genre { get; set; }
    }
}
