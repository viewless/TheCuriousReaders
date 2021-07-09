using System.Collections.Generic;
using TheCuriousReaders.Models.ServiceModels;

namespace TheCuriousReaders.Models.ResponseModels
{
    public class SearchResponse
    {
        public ICollection<SearchBookModel> Books { get; set; }

        public int TotalCount { get; set; }
    }
}
