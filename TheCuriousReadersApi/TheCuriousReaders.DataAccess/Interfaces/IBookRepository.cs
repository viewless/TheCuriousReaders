using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ResponseModels;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.DataAccess.Interfaces
{
    public interface IBookRepository
    {
        Task<BookModel> CreateBookAsync(BookModel model);
        Task<BookModel> GetABookAsync(int id);
        Task<int> CountOfBooksAsync();
        Task<ICollection<BookModel>> GetBooksWithPaginationAsync(PaginationParameters paginationParameters, bool shouldBeNewlyCreated = false);
        Task<int> CountOfNewBooksAsync();
        Task ReduceQuantity(int id, int copies);
        Task<SearchResponse> SearchAsync(PaginationParameters paginationParameters, SearchParameters model);
        Task<BookModel> DeleteABookAsync(int id);
        Task<BookModel> AddCoverAsync(int id, string coverUri);
        Task PatchABookAsync(int id, BookModel model);
        Task<BookModel> UpdateABookAsync(int id, BookModel model);
        Task<bool> BookWithIdExists(int id);
    }
}
