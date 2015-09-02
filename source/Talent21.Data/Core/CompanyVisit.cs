namespace Talent21.Data.Core
{
    public class CompanyVisit : Visit
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}