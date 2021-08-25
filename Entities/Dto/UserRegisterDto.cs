using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Dto
{
    public class UserRegisterDto: IDto
    {
        [DisplayName("Email Field")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [MaxLength(60, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} cannot be less than {1}.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [DisplayName("Password Field")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [MaxLength(30, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} cannot be less than {1}.")]
        [RegularExpression(@"^(?=.{8,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$", ErrorMessage = "{0} is required and must be properly formatted (at least one digit, lower case and one upper case characters).")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Status { get; set; }
        [DisplayName("Name Field")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [MaxLength(30, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} cannot be less than {1}.")]
        [RegularExpression("^((?!^First Name$)[a-zA-Z '])+$", ErrorMessage = "{0} is required and must be properly formatted.")]
        public string Name { get; set; }
        [DisplayName("Surname Field")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [MaxLength(30, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} cannot be less than {1}.")]
        [RegularExpression("^((?!^Surname$)[a-zA-Z '])+$", ErrorMessage = "{0} is required and must be properly formatted.")]
        public string Surname { get; set; }
        [DisplayName("Phone Number Field")]
        [Required(ErrorMessage = "{0} cannot be empty.")]
        [MaxLength(15, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(2, ErrorMessage = "{0} cannot be less than {1}.")]
        [RegularExpression("^[01]?[- .]?\\(?[2-9]\\d{2}\\)?[- .]?\\d{3}[- .]?\\d{4}$",ErrorMessage = "{0} is required and must be properly formatted.")]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }
        [DisplayName("Business Phone Number Field")]
        [MaxLength(15, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(8, ErrorMessage = "{0} cannot be less than {1}.")]
        [RegularExpression("^[01]?[- .]?\\(?[2-9]\\d{2}\\)?[- .]?\\d{3}[- .]?\\d{4}$", ErrorMessage = "{0} must be properly formatted.")]
        [DataType(DataType.PhoneNumber)]
        public string TelBusiness { get; set; }
        [DisplayName("Home Phone Number Field")]
        [MaxLength(15, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(1, ErrorMessage = "{0} cannot be less than {1}.")]
        [RegularExpression("^[01]?[- .]?\\(?[2-9]\\d{2}\\)?[- .]?\\d{3}[- .]?\\d{4}$", ErrorMessage = "{0} must be properly formatted.")]
        [DataType(DataType.PhoneNumber)]
        public string TelHome { get; set; }
        [DisplayName("Address Field")]
        [MaxLength(500, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(5, ErrorMessage = "{0} cannot be less than {1}.")]
        public string Address { get; set; }
        [DisplayName("Photo Field")]
        [MinLength(0, ErrorMessage = "{0} cannot be less than {1}.")]
        public string Photo { get; set; }
        [DisplayName("Company Name Field")]
        [MaxLength(150, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(0, ErrorMessage = "{0} cannot be less than {1}.")]
        public string Company { get; set; }
        [DisplayName("Title Field")]
        [MaxLength(150, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(0, ErrorMessage = "{0} cannot be less than {1}.")]
        public string Title { get; set; }
        [DisplayName("Birthday Information Field")]
        [MaxLength(10, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(0, ErrorMessage = "{0} cannot be less than {1}.")]
        [RegularExpression(@"^([012]\d|30|31)/(0\d|10|11|12)/\d{4}$", ErrorMessage = "{0} must be properly formatted.")]
        [DataType(DataType.Text)]
        public string BirthDate { get; set; }
        [DisplayName("Note Field")]
        [MaxLength(300, ErrorMessage = "{0} cannot be more than {1} characters.")]
        [MinLength(0, ErrorMessage = "{0} cannot be less than {1}.")]
        [DataType(DataType.Text)]
        public string Note { get; set; }
    }
}
