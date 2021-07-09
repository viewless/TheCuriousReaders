using System;

namespace TheCuriousReaders.Models.ServiceModels
{
    public class SubscriptionModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BookTitle { get; set; }
        public int RequestedSubscriptionDays { get; set; }
        public bool IsAdditionalTimeRequested { get; set; }

    }
}
