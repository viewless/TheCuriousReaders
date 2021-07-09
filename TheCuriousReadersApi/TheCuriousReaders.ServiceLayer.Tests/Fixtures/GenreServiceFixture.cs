using Moq;
using TheCuriousReaders.DataAccess.Interfaces;
using TheCuriousReaders.Services.Interfaces;
using TheCuriousReaders.Services.Services;

namespace TheCuriousReaders.ServiceLayer.Tests.Fixtures
{
    public class GenreServiceFixture
    {
        public Mock<IGenreRepository> GenreRepository { get; private set; }
        public IGenresService GenreService { get; private set; }

        public GenreServiceFixture()
        {
            this.GenreRepository = new Mock<IGenreRepository>();

            this.GenreService = new GenresService(GenreRepository.Object);
        }
    }
}
