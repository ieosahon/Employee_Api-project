using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public abstract class RequestParameters
    {
        const int maxPageSize = 50; // max rows in a page is 50
        public int PageNumber { get; set; } = 1; // if not set by the caller, the default page number will be 1
        private int _pageSize = 5; // if not set by the caller, the default page number of rows in the page will be 10
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
