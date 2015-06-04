namespace Talent21.Data.Core
{
    public class ContractSchedule : Schedule
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public bool? IsBench { get; set; }
    }
}