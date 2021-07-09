using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentModel> CreateCommentAsync(CommentModel commentModel);
        Task<ICollection<PaginatedCommentModel>> GetCommentsWithPaginationAsync(PaginationParameters paginationParameters, int bookId);
        Task<int> GetTotalApprovedCommentsForABookAsync(int bookId);
        Task<int> GetTotalCommentsForABookAsync(int bookId);
        Task<CommentModel> DeleteCommentByIdAsync(int id);
        Task<CommentModel> GetCommentAsync(int id);
        Task ReviewCommentAsync(int id, bool isApproved);
        Task<ICollection<PaginatedCommentModel>> GetApprovedCommentsWithPaginationASync(PaginationParameters paginationParameters, int bookId);
    }
}
