using System.ComponentModel.DataAnnotations;

namespace DojoActivity.Models
{
    public class RegisterViewModels : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [Display(Name="First Name")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name="Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string LastName { get; set; }
 
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email { get; set; }
 
        [Required]
        [MinLength(8)]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        public string Confirm{ get; set; }
    }


    public class LoginViewModels : BaseEntity
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Email")]
        [EmailAddress(ErrorMessage="Invalid Email")]
        public string LogEmail {get;set;}

        [Required]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
            
        public string LogPassword{get;set;}
    }


}
