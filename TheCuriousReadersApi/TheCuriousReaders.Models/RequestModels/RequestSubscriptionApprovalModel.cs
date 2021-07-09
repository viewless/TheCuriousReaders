using System.ComponentModel.DataAnnotations;

namespace TheCuriousReaders.Models.RequestModels
{
    public class RequestSubscriptionApprovalModel
    {
        [Required]
        public bool IsApproved { get; set; }
        public int SubscriptionDaysAccepted { get; set; }
        public bool IsAdditionalTimeRequested { get; set; }
    }
}
