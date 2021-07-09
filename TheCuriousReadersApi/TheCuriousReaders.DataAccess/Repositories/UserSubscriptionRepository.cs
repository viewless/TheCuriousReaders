using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Models.Constants;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.DataAccess.Repositories
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly CuriousReadersContext _curiousReadersContext;
        private readonly IMapper _mapper;

        public UserSubscriptionRepository(CuriousReadersContext curiousReadersContext, IMapper mapper)
        {
            _curiousReadersContext = curiousReadersContext;
            _mapper = mapper;
        }

        public async Task ApproveSubscriptionAsync(int id, int requestedDays)
        {
            var subscription = await _curiousReadersContext.UserSubscriptions
                .FirstOrDefaultAsync(subscription => subscription.Id == id);

            if (subscription.IsAdditionalTimeRequested)
            {
                subscription.IsAdditionalTimeRequested = false;
                subscription.SubscriptionEnd = subscription.SubscriptionEnd.AddDays(requestedDays);
            }
            else
            {
                subscription.SubscriptionEnd = DateTime.Now.AddDays(requestedDays);
                subscription.SubscriptionStart = DateTime.Now;
            }

            subscription.RequestedDays = requestedDays;
            subscription.IsAdminReviewed = true;
            subscription.IsApproved = true;
            subscription.IsRejected = false;

            _curiousReadersContext.UserSubscriptions.Update(subscription);
            await _curiousReadersContext.SaveChangesAsync();
            
        }

        public async Task CreateAsync(string userId, RequestSubscriptionModel request, TimeSpan duration)
        {
            var entities = new List<UserSubscriptionEntity>();
            var now = DateTime.Now;

            using var transaction = _curiousReadersContext.Database.BeginTransaction();
            try
            {
                for (int i = 0; i < request.Copies; i++)
                {
                    var entity = new UserSubscriptionEntity
                    {
                        UserId = userId,
                        BookId = request.BookId,
                        SubscriptionStart = now,
                        SubscriptionEnd = now.Add(duration),
                        RequestedDays = duration.Days,
                    };

                    entities.Add(entity);
                }

                await _curiousReadersContext.UserSubscriptions.AddRangeAsync(entities);
                await _curiousReadersContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.RollbackToSavepoint("before adding subscriptions");
            }
        }

        public async Task<bool> ExistsAsync(string userId, int bookId)
        {
            return await _curiousReadersContext.UserSubscriptions.AnyAsync(x => x.UserId == userId 
            && x.BookId == bookId && !x.IsRejected);
        }

        public async Task<ICollection<SubscriptionModel>> GetAllNotReviewedSubscriptionsAsync(PaginationParameters paginationParameters)
        {
            var subscriptions = await _curiousReadersContext.UserSubscriptions
                .Where(subscriptions => !subscriptions.IsAdminReviewed)
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .Include(user => user.User)
                .Include(book => book.Book)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<ICollection<SubscriptionModel>>(subscriptions);
        }

        public async Task<ICollection<ApprovedSubscriptionModel>> GetApprovedSubscriptionsForAnUserAsync(PaginationParameters paginationParameters, string userId)
        {
            var subscriptions = await _curiousReadersContext.UserSubscriptions
                .Where(subscriptions => subscriptions.IsApproved
                && subscriptions.UserId == userId || (subscriptions.IsAdditionalTimeRequested && subscriptions.UserId == userId && 
                 subscriptions.IsApproved))
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .Include(user => user.User)
                .Include(book => book.Book)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<ICollection<ApprovedSubscriptionModel>>(subscriptions);
        }

        public async Task<SubscriptionModel> GetSubscriptionAsync(int id)
        {
            var subscription = await _curiousReadersContext.UserSubscriptions
                .FirstOrDefaultAsync(subscription => subscription.Id == id);

            return _mapper.Map<SubscriptionModel>(subscription);
        }

        public async Task<int> GetTotalCountOfApprovedSubscriptionsForAnUserAsync(string userId)
        {
            return await _curiousReadersContext.UserSubscriptions
                .Where(subscriptions => subscriptions.IsAdminReviewed && subscriptions.IsApproved
                && subscriptions.UserId == userId)
                .CountAsync();
        }

        public async Task<int> GetTotalCountOfNonReviewedSubscriptionsAsync()
        {
            return await _curiousReadersContext.UserSubscriptions
                .Where(subscriptions => !subscriptions.IsAdminReviewed)
                .CountAsync();
        }

        public async Task<int> GetTotalSubscribersOfABookAsync(int bookId)
        {
            return await _curiousReadersContext.UserSubscriptions
                .Where(subscription => subscription.BookId == bookId 
                && !subscription.IsRejected )
                .GroupBy(x => x.UserId)
                .CountAsync();
        }

        public async Task RejectSubscriptionAsync(int id)
        {
            var subscription = await _curiousReadersContext.UserSubscriptions
            .Include(book => book.Book)
            .FirstOrDefaultAsync(subscription => subscription.Id == id);

            if (subscription.IsAdditionalTimeRequested)
            {
                subscription.IsAdditionalTimeRequested = false;
            }
            else
            {
                subscription.IsRejected = true;
                subscription.IsApproved = false;
                subscription.Book.Quantity += 1;
            }

            subscription.IsAdminReviewed = true;

            _curiousReadersContext.UserSubscriptions.Update(subscription);
            await _curiousReadersContext.SaveChangesAsync();
        }

        public async Task ExtendSubscriptionAsync(int id, int requestedDays)
        {
            var subscription = await _curiousReadersContext.UserSubscriptions
            .FirstOrDefaultAsync(subscription => subscription.Id == id);

            subscription.IsAdditionalTimeRequested = true;
            subscription.IsAdminReviewed = false;

            subscription.RequestedDays = requestedDays;

            _curiousReadersContext.UserSubscriptions.Update(subscription);
            await _curiousReadersContext.SaveChangesAsync();
        }
    }
}
