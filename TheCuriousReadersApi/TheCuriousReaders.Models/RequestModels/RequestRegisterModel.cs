using System.ComponentModel.DataAnnotations;

namespace TheCuriousReaders.Models.RequestModels
{
    public class RequestRegisterModel
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(35, MinimumLength = 2,
        ErrorMessage = "First Name should be minimum 2 characters and maximum of 35 characters.")]
        [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Invalid first name format.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(35, MinimumLength = 2,
        ErrorMessage = "Last Name should be minimum 2 characters and maximum of 35 characters.")]
        [RegularExpression("^[A-Z][a-zA-Z]*$", ErrorMessage = "Invalid last name format.")]
        public string LastName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"(08)[7-9 ][0-9 ]{7}", ErrorMessage = "Please enter valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public RequestAddressModel Address { get; set; }
    }
}
