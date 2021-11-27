using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnityofWork.ApplicationProcess.July2021.Data.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> All();
        Task<T> GetById(int UserId);
        Task<bool> Add(T entity);
        Task<bool> Delete(int UserId);
        Task<bool> Update(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    }
}
