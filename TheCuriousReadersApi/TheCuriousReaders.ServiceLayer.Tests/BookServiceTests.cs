using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ResponseModels;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.ServiceLayer.Tests.Fixtures;
using TheCuriousReaders.Services.CustomExceptions;
using Xunit;

namespace TheCuriousReaders.ServiceLayer.Tests
{
    public class BookServiceTests : IClassFixture<BookServiceFixture>
    {
        private readonly BookServiceFixture _bookServiceFixture;

        public BookServiceTests(BookServiceFixture bookServiceFixture)
        {
            _bookServiceFixture = bookServiceFixture;
        }

        [Fact]
        public async Task When_CreatingABookWithAnAlreadyExistingTitleAndAuthor_Expect_DuplicateResourceException()
        {
            //Arrange
            var author = new AuthorModel
            {
                Name = "George R R. Martin"
            };

            var genre = new GenreModel
            {
                Name = "Fantasy"
            };

            var bookToCreate = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            _bookServiceFixture.AuthorRepository.Setup(authorRepository => authorRepository.TitleWithAuthorExists(bookToCreate.Title, bookToCreate.Author.Name))
                .ReturnsAsync(true);

            //Act
            var exception = await Record.ExceptionAsync(async () => await _bookServiceFixture.BookService.CreateBookAsync(bookToCreate));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<DuplicateResourceException>(exception);
        }

        [Fact]
        public async Task When_CreatingABookWithAnInvalidGenre_Expect_ValidationException()
        {
            //Arrange
            var author = new AuthorModel
            {
                Name = "George R R. Martin"
            };

            var genre = new GenreModel
            {
                Name = "Non-existing"
            };

            var bookToCreate = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            _bookServiceFixture.AuthorRepository.Setup(authorRepository => authorRepository.TitleWithAuthorExists(bookToCreate.Title, bookToCreate.Author.Name))
                .ReturnsAsync(false);

            //Act
            var exception = await Record.ExceptionAsync(async () => await _bookServiceFixture.BookService.CreateBookAsync(bookToCreate));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Fact]
        public async Task When_CreatingABook_Expect_BookModel()
        {
            //Arrange
            var author = new AuthorModel
            {
                Name = "George R R. Martin"
            };

            var genre = new GenreModel
            {
                Name = "Fantasy"
            };

            var bookToCreate = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            _bookServiceFixture.AuthorRepository.Setup(authorRepository => authorRepository.TitleWithAuthorExists(bookToCreate.Title, bookToCreate.Author.Name))
                .ReturnsAsync(false);
            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.CreateBookAsync(bookToCreate))
                .ReturnsAsync(bookToCreate);

            //Act
            var model = await _bookServiceFixture.BookService.CreateBookAsync(bookToCreate);

            //Assert
            Assert.Equal(model.Title, bookToCreate.Title);
            Assert.Equal(model.Description, bookToCreate.Description);
            Assert.IsType<BookModel>(model);
        }

        [Fact]
        public async Task When_UpdatingABookWithAnAlreadyExistingTitleAndAuthor_Expect_DuplicateResourceException()
        {
            //Arrange
            var author = new AuthorModel
            {
                Name = "George R R. Martin"
            };

            var genre = new GenreModel
            {
                Name = "Fantasy"
            };

            var bookToCreate = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            var book = new BookModel
            {
                Title = "A Song of Ice and Ice",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.GetABookAsync(3))
                .ReturnsAsync(book);
            _bookServiceFixture.AuthorRepository.Setup(authorRepository => authorRepository.TitleWithAuthorExists(bookToCreate.Title, bookToCreate.Author.Name))
                .ReturnsAsync(true);

            //Act
            var exception = await Record.ExceptionAsync(async () => await _bookServiceFixture.BookService.UpdateABookByIdAsync(3, bookToCreate));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<DuplicateResourceException>(exception);
        }

        [Fact]
        public async Task When_UpdatingABookWithAnInvalidGenre_Expect_ValidationException()
        {
            //Arrange
            var author = new AuthorModel
            {
                Name = "George R R. Martin"
            };

            var genre = new GenreModel
            {
                Name = "Non-existing"
            };

            var bookToCreate = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.GetABookAsync(3))
                .ReturnsAsync(bookToCreate);
            _bookServiceFixture.AuthorRepository.Setup(authorRepository => authorRepository.TitleWithAuthorExists(bookToCreate.Title, bookToCreate.Author.Name))
                .ReturnsAsync(false);

            //Act
            var exception = await Record.ExceptionAsync(async () => await _bookServiceFixture.BookService.UpdateABookByIdAsync(3, bookToCreate));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Fact]
        public async Task When_UpdatingABook_Expect_BookModel()
        {
            //Arrange
            var author = new AuthorModel
            {
                Name = "George R R. Martin"
            };

            var genre = new GenreModel
            {
                Name = "Fantasy"
            };

            var bookToCreate = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.GetABookAsync(3))
                .ReturnsAsync(bookToCreate);
            _bookServiceFixture.AuthorRepository.Setup(authorRepository => authorRepository.TitleWithAuthorExists(bookToCreate.Title, bookToCreate.Author.Name))
                .ReturnsAsync(false);
            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.UpdateABookAsync(3, bookToCreate))
                .ReturnsAsync(bookToCreate);

            //Act
            var model = await _bookServiceFixture.BookService.UpdateABookByIdAsync(3, bookToCreate);

            //Assert
            Assert.Equal(model.Title, bookToCreate.Title);
            Assert.Equal(model.Description, bookToCreate.Description);
            Assert.IsType<BookModel>(model);
        }

        [Fact]
        public async Task When_UpdatingAFieldDifferentThanTitleOrAuthorWithAnInvalidGenre_Expect_ValidationException()
        {
            //Arrange
            var author = new AuthorModel
            {
                Name = "George R R. Martin"
            };

            var genre = new GenreModel
            {
                Name = "Documentary"
            };

            var bookToCreate = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            var book = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.GetABookAsync(3))
                .ReturnsAsync(book);
            _bookServiceFixture.AuthorRepository.Setup(authorRepository => authorRepository.TitleWithAuthorExists(bookToCreate.Title, bookToCreate.Author.Name))
                .ReturnsAsync(true);

            //Act
            var exception = await Record.ExceptionAsync(async () => await _bookServiceFixture.BookService.UpdateABookByIdAsync(3, bookToCreate));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Fact]
        public async Task When_UpdatingABookFieldDifferentThanAuthorOrTitle_Expect_BookModel()
        {
            //Arrange
            var author = new AuthorModel
            {
                Name = "George R R. Martin"
            };

            var genre = new GenreModel
            {
                Name = "Fantasy"
            };

            var bookToCreate = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            var book = new BookModel
            {
                Title = "A Song of Ice and Fire",
                Description = "The adventures of a few friends",
                Quantity = 44,
                Genre = genre,
                IsAvailable = true,
                Author = author
            };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.GetABookAsync(3))
                .ReturnsAsync(book);
            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.UpdateABookAsync(3, bookToCreate))
                .ReturnsAsync(bookToCreate);

            //Act
            var model = await _bookServiceFixture.BookService.UpdateABookByIdAsync(3, bookToCreate);

            //Assert
            Assert.Equal(model.Title, bookToCreate.Title);
            Assert.Equal(model.Description, bookToCreate.Description);
            Assert.IsType<BookModel>(model);
        }

        [Fact]
        public async Task When_RequestingCountOfNewBooks_Expect_CountOfNewBooks()
        {
            //Arrange
            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.CountOfNewBooksAsync())
                .ReturnsAsync(13);

            //Act
            var count = await _bookServiceFixture.BookService.CountOfNewBooksAsync();

            //Assert
            Assert.Equal(13, count);
        }

        [Fact]
        public async Task When_RequestingCountOfAllBooks_Expect_CountOfAllBooks()
        {
            //Arrange
            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.CountOfBooksAsync())
                .ReturnsAsync(15);

            //Act
            var count = await _bookServiceFixture.BookService.CountOfBooksAsync();

            //Assert
            Assert.Equal(15, count);
        }

        [Fact]
        public async Task When_RetrievingABookById_Expect_BookModel()
        {
            //Arrange
            var book = new BookModel
            {
                Title = "Harry Potter",
                Description = "A story about a young man with glasses and magic wand"
            };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.GetABookAsync(3))
                .ReturnsAsync(book);

            //Act
            var bookResult = await _bookServiceFixture.BookService.GetABookByIdAsync(3);

            //Assert
            Assert.Equal(bookResult, book);         
        }

        [Fact]
        public async Task When_RetrievingBooksWithPagination_Expect_CollectionOfBooks()
        {
            //Arrange
            var paginationParameters = new PaginationParameters();
            paginationParameters.PageNumber = 1;
            paginationParameters.PageSize = 10;

            var books = new BookModel[]{
                new BookModel{Title = "Harry Potter", Description = "A story about a young man with glasses and magic wand" },
                new BookModel{Title = "A song of Ice and Fire", Description = "A song of ice and fire"}
            };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.GetBooksWithPaginationAsync(paginationParameters, true))
                .ReturnsAsync(books);

            //Act
            var result = await _bookServiceFixture.BookService.GetBooksWithPaginationAsync(paginationParameters, true);

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task When_RetrievingPaginatedBooks_Expect_CollectionOfBooksAndTheirCount()
        {
            //Arrange
            var paginationParameters = new PaginationParameters();
            paginationParameters.PageNumber = 1;
            paginationParameters.PageSize = 10;

            var books = new BookModel[]{
                new BookModel{Title = "Harry Potter", Description = "A story about a young man with glasses and magic wand" },
                new BookModel{Title = "A song of Ice and Fire", Description = "A song of ice and fire"}
            };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.CountOfNewBooksAsync())
                .ReturnsAsync(2);

            //Act
            var result = await _bookServiceFixture.BookService.GetPaginatedBooksAndTheirTotalCountAsync(books);

            //Assert
            Assert.Equal(2, result.NewBooks.Count);
        }

        [Fact]
        public  async Task When_SearchingForBooks_Expect_CountOfFoundBooks()
        {
            //Arrange
            var paginationParameters = new PaginationParameters();
            paginationParameters.PageNumber = 1;
            paginationParameters.PageSize = 10;
            var searchParameters = new SearchParameters();

            var books = new SearchBookModel[]{
                new SearchBookModel{Title = "Harry Potter" },
                new SearchBookModel{Title = "A song of Ice and Fire"}
            };

            var searchRes = new SearchResponse
            {
                Books = books,
                TotalCount = 2
            };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.SearchAsync(paginationParameters, searchParameters))
                .ReturnsAsync(searchRes);
            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.CountOfNewBooksAsync())
                .ReturnsAsync(2);
            //Act
            var result =  await _bookServiceFixture.BookService.SearchAsync(paginationParameters, searchParameters);

            //Assert
            Assert.Equal(2, result.Books.Count);
        }

        [Fact]
        public async Task When_DeletingABook_Expect_DeletedBookModel()
        {
            //Arrange
            var book = new BookModel{
                Title = "Harry Potter" };

            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.DeleteABookAsync(3))
                .ReturnsAsync(book);

            //Act
            var result = await _bookServiceFixture.BookService.DeleteABookByIdAsync(3);

            //Assert
            Assert.Equal(result, book);
        }

        [Fact]
        public async Task When_AddingACover_Expect_BookModel()
        {
            //Arrange
            var book = new BookModel
            {
                Title = "Harry Potter"
            };

            string cover = "image.png";

            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a test file")), 0, 0, "Data", "test.txt");

            _bookServiceFixture.BlobService.Setup(blobService => blobService.UploadFileAsync(file))
                .ReturnsAsync(cover);
            _bookServiceFixture.BookRepository.Setup(bookRepository => bookRepository.AddCoverAsync(3, cover))
                .ReturnsAsync(book);

            //Act
            var result = await _bookServiceFixture.BookService.AddCoverAsync(3, file);

            //Assert
            Assert.Equal(result, book);
        }
    }
}
