using ProjectManager.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly PmDbContext _dbContext;

        public Repository(PmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public void Add(T entity)
        {
            _dbContext.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }
    }
}
