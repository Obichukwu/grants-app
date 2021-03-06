using System.ComponentModel.DataAnnotations;

namespace eWallet.ViewModels.Manage {
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}