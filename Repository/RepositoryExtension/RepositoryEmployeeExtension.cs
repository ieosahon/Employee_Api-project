using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryExtension
{
    public static class RepositoryEmployeeExtension
    {
        public static IQueryable<Employee> FilterEmployee (this IQueryable<Employee> employees, uint minAge, uint maxAge)
        {
            return employees.Where(e => (e.Age >= minAge) && (e.Age <= maxAge));
        }

        public static IQueryable<Employee> SearchEmployee (this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return employees;
            }

            string lowercaseTerm = searchTerm.Trim().ToLower();

            return employees.Where(e => e.Name.ToLower().Contains(lowercaseTerm));
        }
    }
}
