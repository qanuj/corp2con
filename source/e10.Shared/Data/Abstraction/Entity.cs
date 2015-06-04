using System;

namespace e10.Shared.Data.Abstraction
{
    public abstract class Entity : IEntity, ISoftDelete, IState, IDictionary
    {
        public int Id { get; set; }
        public DateTime? Deleted { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Title { get; set; }
    }
}