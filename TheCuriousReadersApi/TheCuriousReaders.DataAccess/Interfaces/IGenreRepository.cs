using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.DataAccess.Interfaces
{
    public interface IGenreRepository
    {
        Task<GenreEntity> GetGenreAsync(GenreEntity genreEntity);

        Task<ICollection<GenreModel>> GetGenresAsync();
    }
}
