using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e10.Shared.Data.Abstraction
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();

        TEntity ById(TKey id);
        IQueryable<TEntity> ById(IEnumerable<TKey> ids);

        void Create(TEntity entity); //returns Rows Affected
        void Create(ICollection<TEntity> entity); //returns Rows Affected
        void Update(TEntity entity); //returns Rows Affected
        void Delete(TEntity entity); //returns Rows Affected
        void Delete(TKey id); //returns Rows Affected
        void Attach(TEntity entity); //returns Rows Affected
        void Delete(ICollection<TEntity> entities); //returns Rows Affected
        void Purge(TEntity entity);

        IQueryable<TEntity> All { get; }
        IEventManager EventManager { get; }
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : IEntity
    {
        
    }

    public interface IMyRepository<T> : IRepository<T> where T : Entity
    {
        T MyOne(string userId, int id);
        Task<T> MyOneAsync(string userId, int id);
        IQueryable<T> Mine(string id);
        int Count(string userId, Func<T, bool> func);
        int Count(string userId);
    }

    public interface IDictionaryRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> ByCode(IEnumerable<string> codes);
        TEntity ByCode(string code);

        IQueryable<TEntity> ByTitle(IEnumerable<string> titles);
        TEntity ByTitle(string title);
    }
}