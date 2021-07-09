using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Models.RequestModels;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMapper _mapper;
        private readonly CuriousReadersContext _curiousReadersContext;

        public CommentRepository(IMapper mapper, CuriousReadersContext curiousReadersContext)
        {
            _mapper = mapper;
            _curiousReadersContext = curiousReadersContext;
        }

        public async Task<CommentModel> CreateCommentAsync(CommentModel commentModel)
        {
            var commentEntity = _mapper.Map<CommentEntity>(commentModel);

            await _curiousReadersContext.AddAsync(commentEntity);
            await _curiousReadersContext.SaveChangesAsync();

            return _mapper.Map<CommentModel>(commentEntity);
        }

        public async Task<ICollection<PaginatedCommentModel>> GetApprovedCommentsWithPaginationASync(PaginationParameters paginationParameters, int bookId)
        {
            var comments = await _curiousReadersContext.Comments
            .Where(c => c.BookId == bookId && c.IsApproved)
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .AsNoTracking()
            .Include(user => user.User)
            .ToListAsync();

            return _mapper.Map<ICollection<PaginatedCommentModel>>(comments);
        }

        public async Task<ICollection<PaginatedCommentModel>> GetCommentsWithPaginationAsync(PaginationParameters paginationParameters, int bookId)
        {
            var comments = await _curiousReadersContext.Comments
            .Where(book => book.BookId == bookId)
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .AsNoTracking()
            .Include(user => user.User)
            .ToListAsync();

            return _mapper.Map<ICollection<PaginatedCommentModel>>(comments);
        }

        public async Task<int> GetTotalCommentsForABookAsync(int bookId)
        {
            return await _curiousReadersContext.Comments
                .Where(book => book.BookId == bookId)
                .CountAsync();
        }

        public async Task<int> GetTotalApprovedCommentsForABookAsync(int bookId)
        {
            return await _curiousReadersContext.Comments
                .Where(c => c.BookId == bookId && c.IsApproved)
                .CountAsync();
        }
        public async Task<CommentModel> DeleteCommentByIdAsync(int id)
        {
            var commentEntity = await _curiousReadersContext.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (commentEntity == null)
            {
                return null;
            }

            _curiousReadersContext.Remove(commentEntity);
            await _curiousReadersContext.SaveChangesAsync();

            return _mapper.Map<CommentModel>(commentEntity);
        }

        public async Task<CommentModel> GetCommentAsync(int id)
        {
            var commentEntity = await _curiousReadersContext.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<CommentModel>(commentEntity);
        }

        public async Task ReviewCommentAsync(int id, bool isApproved)
        {
            var commentEntity = await _curiousReadersContext.Comments
                .FirstOrDefaultAsync(x => x.Id == id);

            commentEntity.IsApproved = isApproved;

            _curiousReadersContext.Comments.Update(commentEntity);

            await _curiousReadersContext.SaveChangesAsync();
        }
    }
}
