using System.ComponentModel.DataAnnotations;

namespace MakeIt.WebUI.ViewModel.Account
{
    public class RegisterViewModel
    {
        public RegisterViewModel() { }

        public RegisterViewModel(string recaptchaPublicKey)
        {
            RecaptchaPublicKey = recaptchaPublicKey;
        }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Minimum Length = 8", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password confirmation")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match")]
        public string ConfirmPassword { get; set; }

        public string RecaptchaPublicKey { get; }

    }
}