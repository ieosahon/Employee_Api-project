using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => CurrentPage > 1; // true if the current page is greater than 1. i.e the current page is 2 and above
        public bool HasNextPage => CurrentPage < TotalPages; // true if the current page is less that the total number of pages
    }
}
