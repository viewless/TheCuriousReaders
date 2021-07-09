using System.Collections.Generic;

namespace TheCuriousReaders.Models.ResponseModels
{
    public class PaginatedNewBookResponse
    {
        public ICollection<NewBookResponse> NewBooks { get; set; }
        public int TotalCount { get; set; }
    }
}
