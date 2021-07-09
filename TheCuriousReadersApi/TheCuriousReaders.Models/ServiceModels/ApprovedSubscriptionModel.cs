using System;

namespace TheCuriousReaders.Models.ServiceModels
{
    public class ApprovedSubscriptionModel
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public DateTime ReturnBookDate { get; set; }
        public int RemainingDays { get; set; }
        public bool IsAdditionalTimeRequested { get; set; }
    }
}
