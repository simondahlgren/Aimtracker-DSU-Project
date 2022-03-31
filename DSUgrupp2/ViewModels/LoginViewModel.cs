using System.ComponentModel.DataAnnotations;

namespace DSUgrupp2.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// ViewModel for login page // home controller
        /// </summary>
        [Required]


        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        
     

    }
}
