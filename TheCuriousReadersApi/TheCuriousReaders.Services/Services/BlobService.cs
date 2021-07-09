using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TheCuriousReaders.Services.CustomExceptions;
using TheCuriousReaders.Services.Interfaces;

namespace TheCuriousReaders.Services.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string containerName = "bookstorage";
        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadFileAsync(IFormFile cover)
        {
            if (cover is null)
            {
                throw new ValidationException("Cover picture is null.");
            }

            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobClient = blobContainer.GetBlobClient(cover.FileName);

            using (var stream = cover.OpenReadStream())
            {
                stream.Position = 0;

                await blobClient.UploadAsync(stream, true);
            }
            
            return blobClient.Uri.ToString();
        }
    }
}
