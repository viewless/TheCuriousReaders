namespace TheCuriousReaders.Models.ServiceModels
{
    public class SearchParameters
    {
        public string BookTitle { get; set; }

        public string AuthorName { get; set; }

        public string GenreName { get; set; }

        public string BookDescription { get; set; }

        public string CommentBody { get; set; }
    }
}
