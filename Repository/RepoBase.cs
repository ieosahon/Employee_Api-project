using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepoBase<T> : IRepoBase<T> where T : class
    {
        protected RepoContext _repoContext;

        public RepoBase(RepoContext repoContext)
        {
            _repoContext = repoContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ?
            _repoContext.Set<T>().AsNoTracking() : _repoContext.Set<T>();

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ? _repoContext.Set<T>().Where(expression).AsNoTracking()
            : _repoContext.Set<T>()
            .Where(expression);


        public void Create(T entity) => _repoContext.Set<T>().Add(entity);

        public void Delete(T entity) => _repoContext.Set<T>().Remove(entity);

        public void Update(T entity) => _repoContext.Set<T>().Update(entity);

    }
}
