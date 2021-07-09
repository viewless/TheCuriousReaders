using System.Collections.Generic;
using System.Threading.Tasks;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.Services.Interfaces
{
    public interface IGenresService
    {
        Task<ICollection<GenreModel>> GetGenresAsync();
    }
}
