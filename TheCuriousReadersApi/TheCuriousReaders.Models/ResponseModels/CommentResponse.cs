namespace TheCuriousReaders.Models.ResponseModels
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }

        public string CommentBody { get; set; }
    }
}
