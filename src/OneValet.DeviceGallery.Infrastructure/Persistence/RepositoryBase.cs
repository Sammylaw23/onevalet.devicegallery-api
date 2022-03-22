using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Infrastructure.Persistence
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly DeviceDbContext _dbContext;

        public RepositoryBase(IApplicationDbContext context)
        {
            _dbContext = (DeviceDbContext)context;
        }

        protected virtual async Task AddAsync(T entity) => await _dbContext.Set<T>().AddAsync(entity);

        protected virtual void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        protected virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
                query.Where(filter).AsNoTracking();

            foreach (string includedProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includedProperty);
            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        protected virtual async Task<T> GetByIdAsync(object id) => await _dbContext.Set<T>().FindAsync(id);

        protected virtual void Update(T entity) => _dbContext.Entry(entity).State = EntityState.Modified;

    }
}
