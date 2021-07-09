using System.Collections.Generic;

namespace TheCuriousReaders.Models.ServiceModels
{
    public class PaginatedNewBookModel
    {
        public ICollection<BookModel> NewBooks { get; set; }
        public int TotalCount { get; set; }
    }
}
