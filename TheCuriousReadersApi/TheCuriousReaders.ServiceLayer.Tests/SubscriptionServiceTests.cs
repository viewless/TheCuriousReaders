using Moq;
using System.Threading.Tasks;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.ServiceLayer.Tests.Fixtures;
using TheCuriousReaders.Services.CustomExceptions;
using Xunit;

namespace TheCuriousReaders.ServiceLayer.Tests
{
    public class SubscriptionServiceTests : IClassFixture<SubscriptionServiceFixture>
    {
        private readonly SubscriptionServiceFixture _subscriptionsServiceFixture;

        public SubscriptionServiceTests(SubscriptionServiceFixture subscriptionServiceFixture)
        {
            _subscriptionsServiceFixture = subscriptionServiceFixture;
        }

        [Fact]
        public async Task When_InvalidRequestedDaysNumber_Expect_ValidationException()
        {
            //Arrange
            //Act
            var exception = await Record.ExceptionAsync(async () => await _subscriptionsServiceFixture.SubscriptionsService.ApproveSubscriptionAsync(2, -1));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Fact]
        public async Task When_BookDoesNotExist_Expect_False()
        {
            //Arrange
            BookModel bookModel = null;
            RequestSubscriptionModel requestSubscriptionModel = new RequestSubscriptionModel();
            requestSubscriptionModel.BookId = 23;
            requestSubscriptionModel.Copies = 24;

            _subscriptionsServiceFixture.BookRepository
                .Setup(bookRepository => bookRepository.GetABookAsync(23)).ReturnsAsync(bookModel);

            //Act
            var result = await _subscriptionsServiceFixture.SubscriptionsService
                 .SubscribeAsync("993dsao", requestSubscriptionModel);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task When_UserAlreadySubscribed_Expect_DuplicateResourceException()
        {
            //Arrange
            BookModel bookModel = new BookModel
            {
                Title = "Game of Thrones",
                Description = "A book about fire and ice",
                Quantity = 22
            };

            RequestSubscriptionModel requestSubscriptionModel = new RequestSubscriptionModel();
            requestSubscriptionModel.BookId = 23;
            requestSubscriptionModel.Copies = 24;

            _subscriptionsServiceFixture.BookRepository
                .Setup(bookRepository => bookRepository.GetABookAsync(23)).ReturnsAsync(bookModel);
            _subscriptionsServiceFixture.UserSubscriptionRepository
                .Setup(userSubscriptionRepository => userSubscriptionRepository.ExistsAsync("993dsao", 23)).ReturnsAsync(true);

            //Act
            var exception = await Record.ExceptionAsync(async () => await _subscriptionsServiceFixture.SubscriptionsService.SubscribeAsync("993dsao", requestSubscriptionModel));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<DuplicateResourceException>(exception);
        }

        [Fact]
        public async Task When_BookQuantityIsZero_Expect_ValidationException()
        {
            //Arrange
            BookModel bookModel = new BookModel
            {
                Title = "Game of Thrones",
                Description = "A book about fire and ice",
                Quantity = 0
            };

            RequestSubscriptionModel requestSubscriptionModel = new RequestSubscriptionModel();
            requestSubscriptionModel.BookId = 23;
            requestSubscriptionModel.Copies = 24;

            _subscriptionsServiceFixture.BookRepository
                .Setup(bookRepository => bookRepository.GetABookAsync(23)).ReturnsAsync(bookModel);
            _subscriptionsServiceFixture.UserSubscriptionRepository
                .Setup(userSubscriptionRepository => userSubscriptionRepository.ExistsAsync("993dsao", 23)).ReturnsAsync(false);

            //Act
            var exception = await Record.ExceptionAsync(async () => await _subscriptionsServiceFixture.SubscriptionsService.SubscribeAsync("993dsao", requestSubscriptionModel));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Fact]
        public async Task When_NotEnoughQuantityForRequestedCopies_Expect_ValidationException()
        {
            //Arrange
            BookModel bookModel = new BookModel
            {
                Title = "Game of Thrones",
                Description = "A book about fire and ice",
                Quantity = 3
            };

            RequestSubscriptionModel requestSubscriptionModel = new RequestSubscriptionModel();
            requestSubscriptionModel.BookId = 23;
            requestSubscriptionModel.Copies = 23;

            _subscriptionsServiceFixture.BookRepository
                .Setup(bookRepository => bookRepository.GetABookAsync(23)).ReturnsAsync(bookModel);
            _subscriptionsServiceFixture.UserSubscriptionRepository
                .Setup(userSubscriptionRepository => userSubscriptionRepository.ExistsAsync("993dsao", 23)).ReturnsAsync(false);

            //Act
            var exception = await Record.ExceptionAsync(async () => await _subscriptionsServiceFixture.SubscriptionsService.SubscribeAsync("993dsao", requestSubscriptionModel));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Fact]
        public async Task When_SubscribingIsSuccessful_Expect_True()
        {
            //Arrange
            BookModel bookModel = new BookModel
            {
                Title = "Game of Thrones",
                Description = "A book about fire and ice",
                Quantity = 555
            };

            RequestSubscriptionModel requestSubscriptionModel = new RequestSubscriptionModel();
            requestSubscriptionModel.BookId = 23;
            requestSubscriptionModel.Copies = 23;

            _subscriptionsServiceFixture.BookRepository
                .Setup(bookRepository => bookRepository.GetABookAsync(23)).ReturnsAsync(bookModel);
            _subscriptionsServiceFixture.UserSubscriptionRepository
                .Setup(userSubscriptionRepository => userSubscriptionRepository.ExistsAsync("993dsao", 23)).ReturnsAsync(false);

            //Act
            var result = await _subscriptionsServiceFixture.SubscriptionsService.SubscribeAsync("993dsao", requestSubscriptionModel);

            //Assert
            Assert.True(result);
        }


        [Fact]
        public async Task When_RetrievingApprovedSubscriptionsByUserId_Expect_CollectionOfApprovedSubscriptions()
        {
            //Arrange
            var paginationParameters = new PaginationParameters();
            paginationParameters.PageNumber = 1;
            paginationParameters.PageSize = 10;

            var subscriptions = new ApprovedSubscriptionModel[]{
                new ApprovedSubscriptionModel{BookTitle = "Harry Potter"},
                new ApprovedSubscriptionModel{BookTitle = "A song of ice and fire"}
            };

            _subscriptionsServiceFixture.UserSubscriptionRepository.Setup(userSubscription 
                => userSubscription.GetApprovedSubscriptionsForAnUserAsync(paginationParameters, "034024"))
                .ReturnsAsync(subscriptions);

            //Act
            var result = await _subscriptionsServiceFixture.SubscriptionsService.GetApprovedSubscriptionsForAnUserAsync(paginationParameters, "034024");

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task When_RetrievingNonReviewedSubscriptions_Expect_CollectionOfNonReviewedSubscriptions()
        {
            //Arrange
            var paginationParameters = new PaginationParameters();
            paginationParameters.PageNumber = 1;
            paginationParameters.PageSize = 10;

            var subscriptions = new SubscriptionModel[]{
                new SubscriptionModel{BookTitle = "Harry Potter"},
                new SubscriptionModel{BookTitle = "A song of ice and fire"},
                new SubscriptionModel{BookTitle = "Yan Bibiyan on the moon"}
            };

            _subscriptionsServiceFixture.UserSubscriptionRepository.Setup(userSubscription
                => userSubscription.GetAllNotReviewedSubscriptionsAsync(paginationParameters))
                .ReturnsAsync(subscriptions);

            //Act
            var result = await _subscriptionsServiceFixture.SubscriptionsService.GetNonReviewedSubscriptionsAsync(paginationParameters);

            //Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task When_RetrievingASubscriptionById_Expect_SubscriptionModel()
        {
            //Arrange
            var subscription = new SubscriptionModel{
                BookTitle = "Harry Potter"
            };

            _subscriptionsServiceFixture.UserSubscriptionRepository.Setup(userSubscription
                => userSubscription.GetSubscriptionAsync(3))
                .ReturnsAsync(subscription);

            //Act
            var result = await _subscriptionsServiceFixture.SubscriptionsService.GetSubscriptionAsync(3);

            //Assert
            Assert.Equal(result, subscription);
        }

        [Fact]
        public async Task When_RetrievingTotalSubscribersForABook_Expect_Count()
        {
            //Arrange
            _subscriptionsServiceFixture.UserSubscriptionRepository.Setup(userSubscription
                => userSubscription.GetTotalSubscribersOfABookAsync(3))
                .ReturnsAsync(55);

            //Act
            var result = await _subscriptionsServiceFixture.SubscriptionsService.GetTotalSubscribersOfABookAsync(3);

            //Assert
            Assert.Equal(55, result);
        }
        
        [Fact]
        public async Task When_RetrievingTotalCountOfApprovedSubscriptionsForAnUser_Expect_TotalCount()
        {
            //Arrange
            _subscriptionsServiceFixture.UserSubscriptionRepository.Setup(userSubscription
                => userSubscription.GetTotalCountOfApprovedSubscriptionsForAnUserAsync("9ad9sda9k"))
                .ReturnsAsync(63);

            //Act
            var result = await _subscriptionsServiceFixture.SubscriptionsService.GetTotalCountOfApprovedSubscriptionsForAnUserAsync("9ad9sda9k");

            //Assert
            Assert.Equal(63, result);
        }

        [Fact]
        public async Task When_RetrievingTotalCountOfNonReviewedSubscriptions_Expect_TotalCount()
        {
            //Arrange
            _subscriptionsServiceFixture.UserSubscriptionRepository.Setup(userSubscription
                => userSubscription.GetTotalCountOfNonReviewedSubscriptionsAsync())
                .ReturnsAsync(3223);

            //Act
            var result = await _subscriptionsServiceFixture.SubscriptionsService.GetTotalCountOfNonReviewedSubscriptionsAsync();

            //Assert
            Assert.Equal(3223, result);
        }
    }
}
