using Moq;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Services.Configurations;
using TheCuriousReaders.Services.Interfaces;
using TheCuriousReaders.Services.Services;

namespace TheCuriousReaders.ServiceLayer.Tests.Fixtures
{
    public class SubscriptionServiceFixture
    {
        public Mock<IBookRepository> BookRepository { get; private set; }
        public Mock<IUserSubscriptionRepository> UserSubscriptionRepository { get; private set; }
        public Mock<UserSubscriptionConfiguration> UserSubscriptionConfig { get; private set; }
        public ISubscriptionsService SubscriptionsService { get; private set; }

        public SubscriptionServiceFixture()
        {
            this.BookRepository = new Mock<IBookRepository>();
            this.UserSubscriptionRepository = new Mock<IUserSubscriptionRepository>();
            this.UserSubscriptionConfig = new Mock<UserSubscriptionConfiguration>();

            this.SubscriptionsService = new SubscriptionsService(BookRepository.Object, UserSubscriptionRepository.Object,
                UserSubscriptionConfig.Object);
        }
    }
}
