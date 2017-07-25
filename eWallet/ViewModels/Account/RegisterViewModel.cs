using System;
using System.ComponentModel.DataAnnotations;
using eWallet.Models;

namespace eWallet.ViewModels.Account {
    public class RegisterAgentViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required]
        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }

        [Required]
        [Display(Name = "State")]
        public State State { get; set; }

        [Required]
        [Display(Name = "Region")]
        public Region Region { get; set; }
    }

    public class RegisterFarmerViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Farm Name")]
        public string FarmName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Other name")]
        public string OtherName { get; set; }

        [Required]
        [Display(Name = "Type of Farmer")]
        public FarmerType Type { get; set; }

        [Required]
        [Display(Name = "State")]
        public State State { get; set; }

        [Required]
        [Display(Name = "Region")]
        public Region Region { get; set; }
    }
}