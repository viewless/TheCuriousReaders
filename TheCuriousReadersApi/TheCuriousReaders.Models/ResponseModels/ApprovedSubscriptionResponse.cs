using System;

namespace TheCuriousReaders.Models.ResponseModels
{
    public class ApprovedSubscriptionResponse
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public DateTime ReturnBookDate { get; set; }
        public int RemainingDays { get; set; }
        public bool IsAdditionalTimeRequested { get; set; }
    }
}
