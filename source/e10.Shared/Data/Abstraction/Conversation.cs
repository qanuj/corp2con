using System.Data.Entity.Core.Objects.DataClasses;
using System.Security.Cryptography.X509Certificates;

namespace e10.Shared.Data.Abstraction
{
    public class Conversation : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}