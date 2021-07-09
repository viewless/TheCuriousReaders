using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.DataAccess.Interfaces;

namespace TheCuriousReaders.DataAccess.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly CuriousReadersContext _curiousReadersContext;

        public AuthorRepository(CuriousReadersContext curiousReadersContext)
        {
            _curiousReadersContext = curiousReadersContext;
        }

        public async Task<AuthorEntity> GetAuthorAsync(AuthorEntity authorEntity)
        {
            return await _curiousReadersContext.Authors
                .Where(author => author.Name.ToLower() == authorEntity.Name.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<bool> TitleWithAuthorExists(string bookTitle, string authorName)
        {
            return await _curiousReadersContext.Authors.AnyAsync(a => a.Books.Any(b => b.Title == bookTitle && b.Author.Name == authorName));
        }
    }
}
