using Entities.DTO.EmployeeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.CompanyEmployeeDto
{
    public class CompanyEmployeeCreationDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public IEnumerable<CreateEmployeeDto> Employees { get; set; }
    }
}
