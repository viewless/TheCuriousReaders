using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.DataAccess.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly CuriousReadersContext _curiousReadersContext;
        public GenreRepository(CuriousReadersContext curiousReadersContext)
        {
            _curiousReadersContext = curiousReadersContext;
        }

        public async Task<GenreEntity> GetGenreAsync(GenreEntity genreEntity)
        {
            return await _curiousReadersContext.Genres
                .Where(genre => genre.Name.ToLower() == genreEntity.Name.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<GenreModel>> GetGenresAsync()
        {
            var genresWithBookCount = await _curiousReadersContext.Genres
                .Select(x => new GenreModel
                {
                    Name = x.Name,
                    BookCount = x.Books.Count
                })
                .ToListAsync();

            return genresWithBookCount;
        }
    }
}
