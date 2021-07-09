using System;
using System.ComponentModel.DataAnnotations;

namespace TheCuriousReaders.Models.RequestModels
{
    public class PaginationParameters
    {
        const int MaxPageSize = 30;

        [Range(1, Int32.MaxValue, ErrorMessage = "Negative or 0 page numbers do not exist.")]
        public int PageNumber { get; set; }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize || value <= 0) ? MaxPageSize : value;
            }
        }
    }
}
