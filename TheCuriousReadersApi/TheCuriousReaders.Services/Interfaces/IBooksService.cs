using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ResponseModels;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.Services.Interfaces
{
    public interface IBooksService
    {
        Task<BookModel> CreateBookAsync(BookModel bookModel);
        Task<BookModel> GetABookByIdAsync(int id);
        Task<int> CountOfBooksAsync();
        Task<ICollection<BookModel>> GetBooksWithPaginationAsync(PaginationParameters paginationParameters, bool shouldBeNewlyCreated = false);
        Task<int> CountOfNewBooksAsync();
        Task<PaginatedNewBookModel> GetPaginatedBooksAndTheirTotalCountAsync(ICollection<BookModel> newBooks);
        Task<SearchResponse> SearchAsync(PaginationParameters paginationParameters, SearchParameters model);
        Task<BookModel> DeleteABookByIdAsync(int id);
        Task<BookModel> AddCoverAsync(int id, IFormFile cover);
        Task PatchABookAsync(int id, BookModel bookModel);
        Task<BookModel> UpdateABookByIdAsync(int id,BookModel bookModel);
    }
}
