namespace Talent21.Data.Core
{
    public class Contact : Person
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}