using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public abstract class Person : Entity, IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AlternateNumber { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }

        public string PictureUrl { get; set; }

        public Location Location { get; set; }
        public int? LocationId { get; set; }

    }

}
