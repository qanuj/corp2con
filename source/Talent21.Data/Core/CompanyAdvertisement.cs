namespace Talent21.Data.Core
{
    public class CompanyAdvertisement : Advertisement
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}