namespace TheCuriousReaders.Models.RequestModels
{
    public class RequestSubscriptionModel
    {   public int Id { get; set; }
        public int BookId { get; set; }
        public int Copies { get; set; }
        public bool IsAdditionalTimeRequested { get; set; }
    }
}
