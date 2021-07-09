using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.Models.RequestModels;

namespace TheCuriousReaders.Services.Interfaces
{
    public interface ISubscriptionsService
    {
        Task<bool> SubscribeAsync(string userId, RequestSubscriptionModel request);
        Task<int> GetTotalSubscribersOfABookAsync(int bookId);
        Task<int> GetTotalCountOfNonReviewedSubscriptionsAsync();
        Task<int> GetTotalCountOfApprovedSubscriptionsForAnUserAsync(string userId);
        Task<ICollection<SubscriptionModel>> GetNonReviewedSubscriptionsAsync(PaginationParameters paginationParameters);
        Task<SubscriptionModel> GetSubscriptionAsync(int id);
        Task<ICollection<ApprovedSubscriptionModel>> GetApprovedSubscriptionsForAnUserAsync(PaginationParameters paginationParameters, string userId);
        Task ApproveSubscriptionAsync(int id, int requestedDays);
        Task RejectSubscriptionAsync(int id);
        Task ExtendSubscriptionAsync(int id, int requestedDays);
    }
}
