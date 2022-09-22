using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class EmployeeParameters : RequestParameters
    {

        public EmployeeParameters()
        {

            // to sort by a default parameter if null is passed
            OrderBy = "name";
        }

        // implementing filtering

        public uint MinAge { get; set; } // default value = 0
        public uint MaxAge { get; set; } = int.MaxValue;

        public bool IsValidRange => MaxAge > MinAge;
    }
}
