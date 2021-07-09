using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TheCuriousReaders.Services.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadFileAsync(IFormFile cover);
    }
}
