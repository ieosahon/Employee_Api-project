using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.EmployeeDto
{
    public abstract class EmployeeInputDto
    {
        [Required(ErrorMessage = "The field must not be empty")]
        [MinLength(2, ErrorMessage = "The minimum length must not be less than 2")]
        [MaxLength(30, ErrorMessage = "The string length must not be greater than 30")]
        //[RegularExpression("^[A-Z]{1}[o-z]*$", ErrorMessage = "Name must contain alphabets only, starting with upper case")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field must not be empty")]
        [Range(18, int.MaxValue, ErrorMessage = "Age must not be empty and less than 18")]
        [DataType("Number")]
        public int Age { get; set; }

        [Required(ErrorMessage = "The field must not be empty")]
        [MinLength(2, ErrorMessage = "The minimum length must not be less than 2")]
        [MaxLength(30, ErrorMessage = "The string length must not be greater than 30")]
        //[RegularExpression("^[A-Z]{1}[o-z]*$", ErrorMessage = "Position must contain alphabets only, starting with only one (1) upper case")]

        public string Position { get; set; }
    }
}
