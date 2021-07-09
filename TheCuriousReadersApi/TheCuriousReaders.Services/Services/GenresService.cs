using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.Services.Interfaces;

namespace TheCuriousReaders.Services.Services
{
    public class GenresService : IGenresService
    {
        private readonly IGenreRepository _genreRepository;

        public GenresService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<ICollection<GenreModel>> GetGenresAsync()
        {
            return await _genreRepository.GetGenresAsync();
        }
    }
}
