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
using TheCuriousReaders.Models.ResponseModels;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {

        private readonly IMapper _mapper;
        private readonly CuriousReadersContext _curiousReadersContext;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly string[] delimiters = new string[] { ",", ";", " ", "!", "?", ".", "\n" };

        public BookRepository(IMapper mapper,
            CuriousReadersContext curiousReadersContext,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository)
        {
            _mapper = mapper;
            _curiousReadersContext = curiousReadersContext;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<BookModel> CreateBookAsync(BookModel model)
        {
            var entity = _mapper.Map<BookEntity>(model);

            var author = await _authorRepository.GetAuthorAsync(entity.Author);
            var genre = await _genreRepository.GetGenreAsync(entity.Genre);

            if (!(author is null))
            {
                entity.Author = author;
            }

            if (!(genre is null))
            {
                entity.Genre = genre;
            }

            entity.CreatedAt = DateTime.Now;
            entity.IsAvailable = true;
            await _curiousReadersContext.AddAsync(entity);
            await _curiousReadersContext.SaveChangesAsync();

            return _mapper.Map<BookModel>(entity);
        }

        public async Task<BookModel> GetABookAsync(int id)
        {
            var bookEntity = await _curiousReadersContext.Books
                .AsNoTracking()
                .Include(author => author.Author)
                .Include(genre => genre.Genre)
                .FirstOrDefaultAsync(book => book.Id == id);

            return _mapper.Map<BookModel>(bookEntity);
        }

        public async Task<ICollection<BookModel>> GetBooksWithPaginationAsync(PaginationParameters paginationParameters, bool shouldBeNewlyCreated = false)
        {
            List<BookEntity> books;

            books = await _curiousReadersContext.Books
            .Where(book => !shouldBeNewlyCreated
            || EF.Functions.DateDiffDay(book.CreatedAt, DateTime.Now) <= Constants.NewBookDuration)
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .AsNoTracking()
            .Include(author => author.Author)
            .Include(genre => genre.Genre)
            .ToListAsync();

            return _mapper.Map<ICollection<BookModel>>(books);
        }

        public async Task<int> CountOfNewBooksAsync()
        {
            return await _curiousReadersContext.Books
                .Where(book => EF.Functions.DateDiffDay(EF.Property<DateTime>(book, "CreatedAt"), DateTime.Now) <= Constants.NewBookDuration)
                .CountAsync();
        }

        public async Task ReduceQuantity(int id, int copies)
        {
            using var transaction = _curiousReadersContext.Database.BeginTransaction();
            try
            {
                var bookEntity = await _curiousReadersContext.Books.FindAsync(id);

                if (bookEntity.Quantity >= copies)
                {
                    bookEntity.Quantity -= copies;
                }

                await _curiousReadersContext.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.RollbackToSavepoint("before reduced quantity");
            }
        }

        public async Task<SearchResponse> SearchAsync(PaginationParameters paginationParameters, SearchParameters model)
        {
            var searchResult = new List<SearchBookModel>();
            var totalCount = 0;

            if (!string.IsNullOrEmpty(model.BookTitle))
            {
                var filteredBooks = await _curiousReadersContext.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .Where(b =>
                           b.Title.Contains(model.BookTitle))
                    .ToListAsync();
 
                foreach (var book in filteredBooks)
                {
                    if (!searchResult.Any(x => x.Id == book.Id))
                    {
                        searchResult.Add(_mapper.Map<SearchBookModel>(book));
                        totalCount += 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(model.AuthorName))
            {
                var filteredBooks = await _curiousReadersContext.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .Where(b =>
                           b.Author.Name.Contains(model.AuthorName))
                    .ToListAsync();

                foreach (var book in filteredBooks)
                {
                    if (!searchResult.Any(x => x.Id == book.Id))
                    {
                        searchResult.Add(_mapper.Map<SearchBookModel>(book));
                        totalCount += 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(model.GenreName))
            {
                var filteredBooks = await _curiousReadersContext.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .Where(b =>
                           b.Genre.Name.Contains(model.GenreName))
                    .ToListAsync();

                foreach (var book in filteredBooks)
                {
                    if (!searchResult.Any(x => x.Id == book.Id))
                    {
                        searchResult.Add(_mapper.Map<SearchBookModel>(book));
                        totalCount += 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(model.BookDescription))
            {
                var entities = await _curiousReadersContext.Books
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .ToListAsync();

                var filteredBooks = FilterDescription(model.BookDescription, entities);

                foreach (var book in filteredBooks)
                {
                    if (!searchResult.Any(x => x.Id == book.Id))
                    {
                        searchResult.Add(_mapper.Map<SearchBookModel>(book));
                        totalCount += 1;
                    }
                }
            }
            if (!string.IsNullOrEmpty(model.CommentBody))
            {
                var entities = await _curiousReadersContext.Books
                   .Include(b => b.Author)
                   .Include(b => b.Genre)
                   .Include(b => b.UserComments)
                   .ToListAsync();

                var filteredBooks = FilterComments(model.CommentBody, entities);
                
                foreach (var book in filteredBooks)
                {
                    if (!searchResult.Any(x => x.Id == book.Id))
                    {
                        searchResult.Add(_mapper.Map<SearchBookModel>(book));
                        totalCount += 1;
                    }
                }
            }

            var paginatedSearchResult = searchResult
                   .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                   .Take(paginationParameters.PageSize)
                   .ToList();

            return new SearchResponse
            {
                Books = paginatedSearchResult,
                TotalCount = totalCount
            };
        }

        public async Task<BookModel> DeleteABookAsync(int id)
        {
            var bookEntity = await _curiousReadersContext.Books
                .FirstOrDefaultAsync(book => book.Id == id);

            if (bookEntity is null)
            {
                return null;
            }

            _curiousReadersContext.Remove(bookEntity);
            await _curiousReadersContext.SaveChangesAsync();

            return _mapper.Map<BookModel>(bookEntity);
        }

        public async Task<BookModel> AddCoverAsync(int id, string coverUri)
        {
            var entity = await _curiousReadersContext.Books
               .Include(author => author.Author)
               .Include(genre => genre.Genre)
               .FirstOrDefaultAsync(book => book.Id == id);

            entity.CoverUri = coverUri;

            _curiousReadersContext.Books.Update(entity);

            await _curiousReadersContext.SaveChangesAsync();

            return _mapper.Map<BookModel>(entity);
        }

        public async Task<BookModel> UpdateABookAsync(int id, BookModel model)
        {
            var modelToUpdate = _mapper.Map<UpdateBookModel>(model);

            var bookEntity = await _curiousReadersContext.Books
                .Include(author => author.Author)
                .Include(genre => genre.Genre)
                .FirstOrDefaultAsync(book => book.Id == id);

            modelToUpdate.CoverUri = bookEntity.CoverUri;
            modelToUpdate.CreatedAt = bookEntity.CreatedAt;
            modelToUpdate.IsAvailable = bookEntity.IsAvailable;

            var genre = await _genreRepository.GetGenreAsync(_mapper.Map<GenreEntity>(model.Genre));
            bookEntity.Genre = genre;

            var author = await _authorRepository.GetAuthorAsync(_mapper.Map<AuthorEntity>(model.Author));
            if(author is null)
            {
                _curiousReadersContext.Entry(bookEntity.Author).CurrentValues.SetValues(modelToUpdate.Author);
                _curiousReadersContext.Entry(bookEntity).CurrentValues.SetValues(modelToUpdate);
                await _curiousReadersContext.SaveChangesAsync();

                return _mapper.Map<BookModel>(model);
            }

            bookEntity.Author = author;
            _curiousReadersContext.Entry(bookEntity).CurrentValues.SetValues(modelToUpdate);
            await _curiousReadersContext.SaveChangesAsync();

            return _mapper.Map<BookModel>(model);
        }

        public async Task<bool> BookWithIdExists(int id)
        {
            return await _curiousReadersContext.Books.AnyAsync(b => b.Id == id);
        }
              
        public async Task PatchABookAsync(int id, BookModel model)
        {
            var bookEntity = await _curiousReadersContext.Books
                .Include(author => author.Author)
                .Include(genre => genre.Genre)
                .FirstOrDefaultAsync(book => book.Id == id);

            bookEntity.IsAvailable = model.IsAvailable;

            _curiousReadersContext.Update(bookEntity);
            await _curiousReadersContext.SaveChangesAsync();
        }

         private IEnumerable<BookEntity> FilterDescription(string bookDescription, List<BookEntity> entities)
        {
            var keywords = new List<string>();

            keywords = bookDescription
                .ToLower()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var filteredBooks = entities
             .Where(b =>
                keywords.Any(kw => b.Description
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                .ToArray()
                .Any(w => w.ToLower() == kw)));

            return filteredBooks;
        }

        private IEnumerable<BookEntity> FilterComments(string commentBody, List<BookEntity> entities)
        {
            var keywords = new List<string>();

            keywords = commentBody
                .ToLower()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var filteredBooks = entities
                .Where(b =>
                 b.UserComments.Any(c =>
                 keywords.Any(kw => c.CommentBody
                 .Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
                 .ToArray()
                 .Any(w => w.ToLower() == kw))));

            return filteredBooks;
        }

        public async Task<int> CountOfBooksAsync()
        {
            return await _curiousReadersContext.Books
                .CountAsync();
        }
    }
}