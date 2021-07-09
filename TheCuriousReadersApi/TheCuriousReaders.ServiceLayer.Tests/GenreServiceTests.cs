using Moq;
using System.Threading.Tasks;
using TheCuriousReaders.Models.ServiceModels;
using TheCuriousReaders.ServiceLayer.Tests.Fixtures;
using Xunit;

namespace TheCuriousReaders.ServiceLayer.Tests
{
    public class GenreServiceTests : IClassFixture<GenreServiceFixture>
    {
        private readonly GenreServiceFixture _genreServiceFixture;

        public GenreServiceTests(GenreServiceFixture genreServiceFixture)
        {
            _genreServiceFixture = genreServiceFixture; 
        }

        [Fact]
        public async Task When_RetrievingGenres_Expect_CollectionOfGenreModels()
        {
            //Arrange
            var genres = new GenreModel[]
            {
                new GenreModel{Name = "Thriller"},
                new GenreModel{Name = "Romance"},
                new GenreModel{Name = "Horror"}
            };

            _genreServiceFixture.GenreRepository.Setup(genreRepository => genreRepository.GetGenresAsync())
                .ReturnsAsync(genres);

            //Act
            var result = await _genreServiceFixture.GenreService.GetGenresAsync();

            //Assert
            Assert.Equal(3, result.Count);
        }
    }
}
