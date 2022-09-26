using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Repository.RepositoryExtension.Utilities;

namespace Repository.RepositoryExtension
{
    public static class RepositoryCompanyExtension
    {

        /// <summary>
        /// to enable searching
        /// </summary>
        /// <param name="companies"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static IQueryable<Company> SearchCompay (this IQueryable<Company> companies, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return companies;
            }

            var lowerTermCase = searchTerm.Trim().ToLower();
            return companies.Where(x => x.Name.ToLower().Contains(lowerTermCase));
        }

        /// <summary>
        /// to enable sorting
        /// </summary>
        /// <param name="companies"></param>
        /// <param name="orderByQueryString"></param>
        /// <returns></returns>
        public static IQueryable<Company> Sort(this IQueryable<Company> companies, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return companies.OrderBy(e => e.Name);

            var orderQuery = OrderByBuilder.CreateOrderQuery<Company>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return companies.OrderBy(e => e.Name);
            return companies.OrderBy(orderQuery);
        }
    }
}
