using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

        protected Entity()
        {
            Created = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }
    }

    public abstract class Dictionary : Entity, IDictionary
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }

    public class Role : IdentityRole<string, UserRole>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="roleName"></param>
        public Role(string roleName)
            : this()
        {
            Name = roleName;
        }
    }

    public class UserRole : IdentityUserRole
    {

    }

    public class User : IdentityUser<string, IdentityUserLogin, UserRole, IdentityUserClaim>, IUser
    {
        /// <summary>
        ///     Constructor which creates a new Guid for the Id
        /// </summary>
        public User()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Constructor that takes a userName
        /// </summary>
        /// <param name="userName"></param>
        public User(string userName)
            : this()
        {
            UserName = userName;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}