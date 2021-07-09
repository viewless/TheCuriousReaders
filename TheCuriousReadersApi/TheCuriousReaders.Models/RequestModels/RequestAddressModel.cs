using System.ComponentModel.DataAnnotations;

namespace TheCuriousReaders.Models.RequestModels
{
    public class RequestAddressModel
    {
        [Required(ErrorMessage = "Country is required.")]
        [StringLength(35, MinimumLength = 2,
        ErrorMessage = "Country should be minimum 2 characters and maximum of 35 characters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(35, MinimumLength = 2,
        ErrorMessage = "City should be minimum 2 characters and maximum of 35 characters.")]
        [RegularExpression("^[A-Z].*[a-zA-Z]$", ErrorMessage = "Invalid city format. City should start with capital letter" +
            " and can't contain special characters at the end of it.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(35, MinimumLength = 2,
        ErrorMessage = "Street should be minimum 2 characters and maximum of 35 characters.")]
        [RegularExpression("^[A-Z].*[a-zA-Z]$", ErrorMessage = "Invalid street format. Street should start with capital letter" +
            " and can't contain special characters at the end of it.")]
        public string Street { get; set; }

        [Display(Name = "Street number")]
        [Required(ErrorMessage = "Street number is required.")]
        [StringLength(10, MinimumLength = 1,
        ErrorMessage = "Street number should be minimum 1 character and maximum of 10 characters.")]
        public string StreetNumber { get; set; }

        [Display(Name = "Building number")]
        public string BuildingNumber { get; set; }

        [Display(Name = "Apartment number")]
        public string ApartmentNumber { get; set; }

        [Display(Name = "Additional info")]
        public string AdditionalInfo { get; set; }
    }
}
