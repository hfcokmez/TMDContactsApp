using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Dto
{
    public class UserLoginDto: IDto
    {
        [DisplayName("Email Field")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [DisplayName("Password Field")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
