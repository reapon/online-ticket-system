using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }

        
        public string ComID { get; set; }
        [Required(ErrorMessage = "Company Name Required!!!")]
        public string CompanyName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
