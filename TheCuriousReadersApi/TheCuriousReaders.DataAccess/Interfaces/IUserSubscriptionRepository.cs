using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.DataAccess.Interfaces
{
    public interface IUserSubscriptionRepository
    {
        Task CreateAsync(string userId, RequestSubscriptionModel request, TimeSpan duration);

        Task<bool> ExistsAsync(string userId, int bookId);
        Task<int> GetTotalSubscribersOfABookAsync(int bookId);
        Task<int> GetTotalCountOfNonReviewedSubscriptionsAsync();
        Task<int> GetTotalCountOfApprovedSubscriptionsForAnUserAsync(string userId);
        Task<ICollection<SubscriptionModel>> GetAllNotReviewedSubscriptionsAsync(PaginationParameters paginationParameters);
        Task<ICollection<ApprovedSubscriptionModel>> GetApprovedSubscriptionsForAnUserAsync(PaginationParameters paginationParameters, string userId);
        Task<SubscriptionModel> GetSubscriptionAsync(int id);
        Task ApproveSubscriptionAsync(int id, int requestedDays);
        Task RejectSubscriptionAsync(int id);
        Task ExtendSubscriptionAsync(int id, int requestedDays);
    }
}
