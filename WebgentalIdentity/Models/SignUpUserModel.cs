using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebgentalIdentity.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage ="Please Enter Your First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }



        [Required(ErrorMessage="Please Enter Your Email")]
        [Display(Name ="Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter a strong Password")]
        [Compare("ConfirmPassword",ErrorMessage="Password Does not Match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
