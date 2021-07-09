using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;

namespace TheCuriousReaders.DataAccess.Interfaces
{
    public interface IAuthorRepository
    {
        Task<AuthorEntity> GetAuthorAsync(AuthorEntity authorEntity);
        Task<bool> TitleWithAuthorExists(string bookTitle, string authorName);
    }
}
