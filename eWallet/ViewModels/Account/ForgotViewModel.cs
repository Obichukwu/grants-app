using System.ComponentModel.DataAnnotations;

namespace eWallet.ViewModels.Account {
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}