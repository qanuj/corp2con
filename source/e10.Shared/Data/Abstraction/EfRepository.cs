using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using e10.Shared.Util;

namespace e10.Shared.Data.Abstraction
{
    public abstract class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class,IEntity
    {
        protected EfRepository(DbContext context, IEventManager eventManager) {
            Guard.ArgumentNotNull(context, "EF Data Context");
            Guard.ArgumentNotNull(eventManager, "ef event manager");
            Context = context;
            EventManager = eventManager;
        }

        protected DbContext Context { get; private set; }

        public abstract IQueryable<TEntity> ById(IEnumerable<int> ids);

        public virtual IQueryable<TEntity> All
        {
            get { return Context.Set<TEntity>(); }
        }

        protected void Attach<T>(T entity,EntityState state) where T : class
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = state;
        }


        protected int UpdateEntity<T>(T entity) where T : class
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChanges();
        }

        protected int CreateEntity<T>(T entity) where T : class
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Added;
            return Context.SaveChanges();
        }

        public virtual void Create(TEntity entity) {
            Guard.ArgumentNotNull(entity, "Create Entity");
            Context.Entry(entity).State = EntityState.Added;
        }

        public virtual TEntity ById(int id) {
            var entity = Context.Set<TEntity>().Find(id);
            if(entity is ISoftDelete) if((entity as ISoftDelete).IsDeleted) return null;
            return entity;
        }

        private void SoftDelete(ISoftDelete entity)
        {
            Guard.ArgumentNotNull(entity, "Soft Delete Entity");
            entity.IsDeleted = true;
            entity.Deleted = DateTime.UtcNow;
        }

        public virtual void Delete(TEntity entity) {
            Guard.ArgumentNotNull(entity, "Delete Entity");
            Context.Set<TEntity>().Attach(entity);
            if (entity is ISoftDelete){
                SoftDelete(entity as ISoftDelete);
            }else{
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public virtual void Update(TEntity entity) {
            Guard.ArgumentNotNull(entity, "Update Entity");
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        protected void AttachDetachedCollection<T>(ICollection<T> entities) where T : class {
            foreach(T entity in entities) {
                Context.Set<T>().Attach(entity);
                Context.Entry(entity).State = EntityState.Unchanged;
            }
        }

        public void Attach(TEntity entity) {
            Context.Set<TEntity>().Attach(entity);
        }

        internal void Attach(ICollection<TEntity> entities)
        {
            throw new NotImplementedException();
            // Context.Set<TEntity>().Attach(entities);
        }

        internal delegate void EntityEvent(TEntity entity);

        public IEventManager EventManager{get;private set;}


        public virtual void Create(ICollection<TEntity> entities)
        {
            Attach(entities);
        }

        public virtual void Delete(ICollection<TEntity> entities) {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}