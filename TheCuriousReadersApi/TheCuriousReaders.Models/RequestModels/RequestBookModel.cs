using System;
using System.ComponentModel.DataAnnotations;

namespace TheCuriousReaders.Models.RequestModels
{
    public class RequestBookModel
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, MinimumLength = 2,
        ErrorMessage = "Title should be minimum 2 characters and maximum of 50 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, MinimumLength = 10,
        ErrorMessage = "Description should be minimum 10 characters and maximum of 500 characters.")]
        public string Description { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Quantity must be more than 0.")]
        public int Quantity { get; set; }

        public RequestAuthorModel Author { get; set; }

        public RequestGenreModel Genre { get; set; }
    }
}
