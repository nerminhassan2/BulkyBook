using System.Linq.Expressions;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DtaAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{   
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //dbSet == _db.Categories
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);  
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            //IQueryable<T> query = dbSet;
            IQueryable<T> query = dbSet.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            //IQueryable<T> query = dbSet;
            return dbSet.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
