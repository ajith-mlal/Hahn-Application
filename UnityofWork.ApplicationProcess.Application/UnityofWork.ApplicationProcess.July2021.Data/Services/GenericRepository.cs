using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnityofWork.ApplicationProcess.July2021.Data.Data;

namespace UnityofWork.ApplicationProcess.July2021.Data.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected DBContext context;
        internal DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(DBContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this._logger = logger;

        }

        public virtual Task<List<T>> All()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(int UserId)
        {
            return await dbSet.FindAsync(UserId);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Delete(int UserId)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }
    }
}
