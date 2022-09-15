using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.CompanyDto
{
    public abstract class CompanyInputDto
    {
        [Required(ErrorMessage = "The field must not be empty")]
        [MinLength(2, ErrorMessage = "The minimum length must not be less than 2")]
        [MaxLength(30, ErrorMessage = "The string length must not be greater than 30")]
        //[RegularExpression("^[A-Z]{1}[o-z]*$", ErrorMessage = "Name must contain alphabets only, starting with upper case")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field must not be empty")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The field must not be empty")]
        [MinLength(2, ErrorMessage = "The minimum length must not be less than 2")]
        [MaxLength(30, ErrorMessage = "The string length must not be greater than 30")]
        //[RegularExpression("^[A-Z]{1}[o-z]*$", ErrorMessage = "Country must contain alphabets only, starting with only one (1) upper case")]
        public string Country { get; set; }
    }
}
