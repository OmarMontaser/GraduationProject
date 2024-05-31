using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeRepository.Interface
{      
        public interface IGenericRepository<T> : IDisposable
        {
            IEnumerable<T> GetAll();
            IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
            T GetById(T id);
            T GetById(string id);
            T GetById(int id);
            Task<T> GetByIdAsync(T id);

            void Add(T entity);
            Task<T> AddAsync(T entity);

            void update(T entity);
            Task<T> UpdateAsync(T entity);

            void Delete(T entity);
            Task<T> DeleteAsync(T entity);

            IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int? skip,
                Expression<Func<T, object>> orderBy = null, string orderByDirection = "Asc");
      

        /*IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IQueryable<T>> orderBy = null,
            string includeProperties = "");
        */
    }
}