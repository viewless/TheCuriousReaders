using Moq;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Services.Interfaces;
using TheCuriousReaders.Services.Services;

namespace TheCuriousReaders.ServiceLayer.Tests.Fixtures
{
    public class CommentServiceFixture
    {
        public Mock<ICommentRepository> CommentRepository { get; private set; }
        public ICommentService CommentService { get; private set; }

        public CommentServiceFixture()
        {
            this.CommentRepository = new Mock<ICommentRepository>();

            this.CommentService = new CommentService(CommentRepository.Object);
        }
    }
}
