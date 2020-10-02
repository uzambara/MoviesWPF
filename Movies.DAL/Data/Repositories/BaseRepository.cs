using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Data.Domain;

namespace Movies.DAL.Data.Repositories
{
    public class BaseRepository<T> where T: class, IEntity
    {
        protected MoviesDbContext _context { get; }
        protected DbSet<T> _dbSet;

        public BaseRepository(MoviesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public Task<T> GetOneAsync(int id)
        {
            return _dbSet.SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task<T> GetOneWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetOneAsync(id);
            Remove(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IDisposable BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}