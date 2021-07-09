using System.ComponentModel.DataAnnotations;

namespace TheCuriousReaders.Models.RequestModels
{
    public class RequestAuthorModel
    {
        [Required(ErrorMessage = "Author is required.")]
        [StringLength(50, MinimumLength = 2,
        ErrorMessage = "Author should be minimum 2 characters and maximum of 50 characters.")]
        public string Name { get; set; }
    }
}
