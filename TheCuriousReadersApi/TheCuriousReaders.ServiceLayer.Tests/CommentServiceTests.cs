using Moq;
using System.Threading.Tasks;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.ServiceLayer.Tests.Fixtures;
using Xunit;

namespace TheCuriousReaders.ServiceLayer.Tests
{
    public class CommentServiceTests : IClassFixture<CommentServiceFixture>
    {
        private readonly CommentServiceFixture _commentServiceFixture;

        public CommentServiceTests(CommentServiceFixture commentServiceFixture)
        {
            _commentServiceFixture = commentServiceFixture;
        }

        [Fact]
        public async Task When_CreatingAComment_Expect_CommentModel()
        {
            //Arrange
            var comment = new CommentModel
            {
                Rating = 3,
                CommentBody = "Very nice book",
                BookId = 1,
                UserId = "Test"
            };

            _commentServiceFixture.CommentRepository.Setup(commentRepository => commentRepository.CreateCommentAsync(comment))
                .ReturnsAsync(comment);
            //Act
            var result = await _commentServiceFixture.CommentService.CreateCommentAsync(comment);
            //Assert
            Assert.Equal(result, comment);
        }

        [Fact]
        public async Task When_RetrievingCommentsWithPagination_Expect_CollectionOfComments()
        {
            //Arrange
            var paginationParameters = new PaginationParameters();
            paginationParameters.PageNumber = 1;
            paginationParameters.PageSize = 10;

            var comments = new PaginatedCommentModel[]{
                new PaginatedCommentModel{Rating = 3, CommentBody = "A story about a young man with glasses and magic wand" },
                new PaginatedCommentModel{Rating = 4, CommentBody = "A song of ice and fire"}
            };

            _commentServiceFixture.CommentRepository.Setup(commentRepository => commentRepository.GetCommentsWithPaginationAsync(paginationParameters, 2))
                .ReturnsAsync(comments);

            //Act
            var result = await _commentServiceFixture.CommentService.GetCommentsWithPaginationAsync(paginationParameters, 2);

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task When_GettingTotalCountOfComments_Expect_CountOfComments()
        {
            //Arrange
            _commentServiceFixture.CommentRepository.Setup(commentRepository => commentRepository.GetTotalCommentsForABookAsync(2))
                .ReturnsAsync(3);

            //Act
            var result = await _commentServiceFixture.CommentService.GetTotalCommentsForABookAsync(2);

            //Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task When_DeletingAComment_Expect_CommentModel()
        {
            //Arrange
            var comment = new CommentModel
            {
                Rating = 5,
                CommentBody = "testing"
            };

            _commentServiceFixture.CommentRepository.Setup(commentRepo => commentRepo.DeleteCommentByIdAsync(1))
                .ReturnsAsync(comment);

            //Act
            var result = await _commentServiceFixture.CommentService.DeleteCommentByIdAsync(1);

            //Assert
            Assert.Equal(result, comment);
        }

        [Fact]
        public async Task When_GettingTotalCountOfApprovedComments_Expect_CountOfApprovedComments()
        {
            //Arrange
            _commentServiceFixture.CommentRepository.Setup(commentRepository => commentRepository.GetTotalApprovedCommentsForABookAsync(3))
                .ReturnsAsync(5);

            //Act
            var result = await _commentServiceFixture.CommentService.GetTotalApprovedCommentsForABookAsync(3);

            //Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public async Task When_RetrievingApprovedCommentsWithPagination_Expect_ApprovedCollectionOfComments()
        {
            //Arrange
            var paginationParameters = new PaginationParameters();
            paginationParameters.PageNumber = 1;
            paginationParameters.PageSize = 10;

            var comments = new PaginatedCommentModel[]{
                new PaginatedCommentModel{Rating = 3, CommentBody = "A story about a young man with glasses and magic wand" , isApproved = true},
                new PaginatedCommentModel{Rating = 4, CommentBody = "A song of ice and fire", isApproved = true},
            };

            _commentServiceFixture.CommentRepository.Setup(commentRepository => commentRepository.GetApprovedCommentsWithPaginationASync(paginationParameters, 2))
                .ReturnsAsync(comments);

            //Act
            var result = await _commentServiceFixture.CommentService.GetApprovedCommentsWithPaginationASync(paginationParameters, 2);

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task When_RetrievingNonApprovedCommentsWithPagination_Expect_EmptyCollection()
        {
            //Arrange
            var paginationParameters = new PaginationParameters();
            paginationParameters.PageNumber = 1;
            paginationParameters.PageSize = 10;

            var comments = new PaginatedCommentModel[]{};

            _commentServiceFixture.CommentRepository.Setup(commentRepository => commentRepository.GetApprovedCommentsWithPaginationASync(paginationParameters, 2))
                .ReturnsAsync(comments);

            //Act
            var result = await _commentServiceFixture.CommentService.GetApprovedCommentsWithPaginationASync(paginationParameters, 2);

            //Assert
            Assert.Equal(0, result.Count);
        }
    }
}
