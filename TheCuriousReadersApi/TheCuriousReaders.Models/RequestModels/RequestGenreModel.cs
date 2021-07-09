using System.ComponentModel.DataAnnotations;

namespace TheCuriousReaders.Models.RequestModels
{
    public class RequestGenreModel
    {
        [Required(ErrorMessage = "Genre is required.")]
        public string Name { get; set; }
    }
}
