using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryExtension
{
    public static class RepositoryCompanyExtension
    {
        public static IQueryable<Company> SearchCompay (this IQueryable<Company> companies, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return companies;
            }

            var lowerTermCase = searchTerm.Trim().ToLower();
            return companies.Where(x => x.Name.ToLower().Contains(lowerTermCase));
        }
    }
}
