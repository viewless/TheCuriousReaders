using System;

namespace TheCuriousReaders.DataAccess.Entities
{
    public class UserSubscriptionEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public UserEntity User { get; set; }

        public int BookId { get; set; }

        public BookEntity Book { get; set; }

        public DateTime SubscriptionStart { get; set; }

        public DateTime SubscriptionEnd { get; set; }
        public int RequestedDays { get; set; }
        public bool IsAdminReviewed { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsAdditionalTimeRequested { get; set; }
    }
}
