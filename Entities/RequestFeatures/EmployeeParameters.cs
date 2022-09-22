using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class EmployeeParameters : RequestParameters
    {
        // implementing filtering

        public uint MinAge { get; set; } // default value = 0
        public uint MaxAge { get; set; } = int.MaxValue;

        public bool IsValidRange => MaxAge > MinAge;
    }
}
