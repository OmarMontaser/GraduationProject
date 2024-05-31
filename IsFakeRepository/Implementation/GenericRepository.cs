using IsFakeModels;
using IsFakeRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeRepository.Implementation
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbset;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbset = _context.Set<T>();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return dbset.Where(predicate).ToList();
        }


        public void Add(T entity)
        {
            dbset.Add(entity);
        }
        public async Task<T> AddAsync(T entity)
        {
            dbset.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }

            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);

            }
            dbset.Remove(entity);

        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);

            }
            dbset.Remove(entity);
            return entity;
        }

        public void Dispose()
        {
            _context.Dispose();
        }


/*        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in
                includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }
*/
        public IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public T GetById(T id)
        {
            return dbset.Find(id);
        }
        public T GetById(int id)
        {
            return dbset.Find(id);
        }
        public T GetById(string id)
        {
            return dbset.Find(id);
        }

        public T GetByUserName(string UserName)
        {
            return dbset.Find(UserName);
        }

        public async Task<T> GetByIdAsync(T id)
        {
            return await dbset.FindAsync(id);
        }

        public void update(T entity)
        {
            dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int? skip, Expression<Func<T, object>> orderBy = null, string orderByDirection = "Asc")
        {
            IQueryable<T> query = dbset.Where(match);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == "ASC")
                    query = query.OrderBy(orderBy);

                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }


    }
}
