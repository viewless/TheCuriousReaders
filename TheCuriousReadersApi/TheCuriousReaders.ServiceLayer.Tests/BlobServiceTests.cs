using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TheCuriousReaders.ServiceLayer.Tests.Fixtures;
using TheCuriousReaders.Services.CustomExceptions;
using Xunit;

namespace TheCuriousReaders.ServiceLayer.Tests
{
    public class BlobServiceTests : IClassFixture<BlobServiceFixture>
    {
        private readonly BlobServiceFixture _blobServiceFixture;

        public BlobServiceTests(BlobServiceFixture blobServiceFixture)
        {
            _blobServiceFixture = blobServiceFixture;
        }

        [Fact]
        public async Task When_CoverisNull_Expect_ValidationException()
        {
            //Arrange
            //Act
            var exception = await Record.ExceptionAsync(async () => await _blobServiceFixture.BlobService.UploadFileAsync(null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<ValidationException>(exception);
        }

        [Fact]
        public async Task When_CoverisProvided_Expect_NotNullResult()
        {
            //Arrange
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a file")), 0, 0, "Data", "test.txt");
            var _blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=sfddakotastorage;AccountKey=mj1a3dA3vTWljsxkygkqrINxjduVfrnXG88MtyXmMIo2/WNf1bjiEh+li1BUh7V8ehdfcS85Oen4Hl7X1vDygw==;EndpointSuffix=core.windows.net");
            var containerClient = _blobServiceClient.GetBlobContainerClient("bookstorage");

            _blobServiceFixture.BlobClient.Setup(blobClient => blobClient.GetBlobContainerClient("bookstorage")).Returns(containerClient);
            //Act
            var result = await _blobServiceFixture.BlobService.UploadFileAsync(file);

            //Assert
            Assert.NotNull(result);
        }
    }
}
