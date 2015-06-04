using System;

namespace e10.Shared.Data.Abstraction
{
    public abstract class Entity : IEntity, ISoftDelete, IState
    {
        public int Id { get; set; }
        public DateTime? Deleted { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }

    public abstract class Dictionary : Entity, IDictionary
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }
}