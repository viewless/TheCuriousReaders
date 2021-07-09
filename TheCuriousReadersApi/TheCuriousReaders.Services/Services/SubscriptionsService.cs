using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.Services.Configurations;
using TheCuriousReaders.Services.CustomExceptions;
using TheCuriousReaders.Services.Interfaces;

namespace TheCuriousReaders.Services.Services
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly UserSubscriptionConfiguration _userSubscriptionConfig;

        public SubscriptionsService(IBookRepository bookRepository,
            IUserSubscriptionRepository userSubscriptionRepository,
            UserSubscriptionConfiguration userSubscriptionConfig)
        {
            _bookRepository = bookRepository;
            _userSubscriptionRepository = userSubscriptionRepository;
            _userSubscriptionConfig = userSubscriptionConfig;
        }

        public async Task ApproveSubscriptionAsync(int id, int requestedDays)
        {
            if(requestedDays < 1)
            {
                throw new ValidationException("Accepted days can't be negative number or 0.");
            }

            await _userSubscriptionRepository.ApproveSubscriptionAsync(id, requestedDays);
        }

        public async Task<ICollection<ApprovedSubscriptionModel>> GetApprovedSubscriptionsForAnUserAsync(PaginationParameters paginationParameters, string userId)
        {
            return await _userSubscriptionRepository.GetApprovedSubscriptionsForAnUserAsync(paginationParameters, userId);
        }

        public async Task<ICollection<SubscriptionModel>> GetNonReviewedSubscriptionsAsync(PaginationParameters paginationParameters)
        {
            return await _userSubscriptionRepository.GetAllNotReviewedSubscriptionsAsync(paginationParameters);
        }

        public async Task<SubscriptionModel> GetSubscriptionAsync(int id)
        {
            return await _userSubscriptionRepository.GetSubscriptionAsync(id);
        }

        public async Task<int> GetTotalCountOfApprovedSubscriptionsForAnUserAsync(string userId)
        {
            return await _userSubscriptionRepository.GetTotalCountOfApprovedSubscriptionsForAnUserAsync(userId);
        }

        public async Task<int> GetTotalCountOfNonReviewedSubscriptionsAsync()
        {
            return await _userSubscriptionRepository.GetTotalCountOfNonReviewedSubscriptionsAsync();
        }

        public async Task<int> GetTotalSubscribersOfABookAsync(int bookId)
        {
            return await _userSubscriptionRepository.GetTotalSubscribersOfABookAsync(bookId);
        }

        public async Task RejectSubscriptionAsync(int id)
        {
            await _userSubscriptionRepository.RejectSubscriptionAsync(id);
        }

        public async Task<bool> SubscribeAsync(string userId, RequestSubscriptionModel request)
        {
            var bookModel = await _bookRepository.GetABookAsync(request.BookId);

            if (bookModel == null)
            {
                return false;
            }

            if (await _userSubscriptionRepository.ExistsAsync(userId, request.BookId))
            {
                throw new DuplicateResourceException("User already subscribed for that book.");
            }

            if (bookModel.Quantity == 0 || request.Copies > bookModel.Quantity)
            {
                throw new ValidationException("Not enough quantity available.");
            }

            await _userSubscriptionRepository.CreateAsync(userId, request, _userSubscriptionConfig.Duration);
            await _bookRepository.ReduceQuantity(request.BookId, request.Copies);

            return true;
        }

        public async Task ExtendSubscriptionAsync(int id, int requestedDays)
        {
            await _userSubscriptionRepository.ExtendSubscriptionAsync(id , requestedDays);
        }
    }
}
