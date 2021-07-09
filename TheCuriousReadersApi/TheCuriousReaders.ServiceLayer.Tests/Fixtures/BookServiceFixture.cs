using AutoMapper;
using Moq;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Models.Helpers;
using TheCuriousReaders.Services.Interfaces;
using TheCuriousReaders.Services.Services;

namespace TheCuriousReaders.ServiceLayer.Tests.Fixtures
{
    public class BookServiceFixture
    {
        public Mock<IBookRepository> BookRepository { get; private set; }
        public Mock<IMapper> Mapper { get; private set; }
        public Mock<IAuthorRepository> AuthorRepository { get; private set; }
        public Mock<IBlobService> BlobService { get; private set; }
        public IBooksService BookService { get; private set; }

        public BookServiceFixture()
        {
            this.BookRepository = new Mock<IBookRepository>();
            this.Mapper = new Mock<IMapper>();
            this.AuthorRepository = new Mock<IAuthorRepository>();
            this.BlobService = new Mock<IBlobService>();

            this.BookService = new BooksService(BookRepository.Object, Mapper.Object, AuthorRepository.Object,
                BlobService.Object);
        }
    }
}
