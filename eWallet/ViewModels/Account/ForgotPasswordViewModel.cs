using System.ComponentModel.DataAnnotations;

namespace eWallet.ViewModels.Account {
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}