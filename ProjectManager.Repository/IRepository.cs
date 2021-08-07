using ProjectManager.Database;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void SaveChanges();
        string FindEMail(string eMail);
    }
}
