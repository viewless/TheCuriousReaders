namespace TheCuriousReaders.Models.ServiceModels
{
    public class PaginatedCommentModel
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string CommentBody { get; set; }
        public bool isApproved { get; set; }
    }
}
