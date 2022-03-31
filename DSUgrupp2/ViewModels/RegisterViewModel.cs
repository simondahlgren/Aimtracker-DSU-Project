
using System.ComponentModel.DataAnnotations;

namespace DSUgrupp2.Models
{
    public class RegisterViewModel
    {
        /// <summary>
        /// Input from an interface, props for setting input values.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords didnt match!")]
        public string ConfirmPassword { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool Trainer { get; set; }
        public bool Athlete { get; set; }

    }
}
