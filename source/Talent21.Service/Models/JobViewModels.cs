using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Lucene.Net.Linq.Mapping;
using Talent21.Data.Core;

namespace Talent21.Service.Models
{
    public class SearchQueryViewModel
    {
        public string Locations { get; set; }
        public string Companies { get; set; }
        public string Skills { get; set; }
        public string Keywords { get; set; }
        public string Folder { get; set; }
        public string Industries { get; set; }
        public string Functionals { get; set; }

        public int? Experience { get; set; }
        public int? Rate { get; set; }
        public int? CompanyId { get; set; }
        public int? JobId { get; set; }

        public bool? ContractExtendable { get; set; }
        public bool? ContractToHire { get; set; }
        public bool? WorkFromHome { get; set; }
        public bool? IsApplied { get; set; }
        public bool? IsBench { get; set; }

        public ContractorTypeEnum? ConsultantTypes { get; set; }
        public ContractTypeEnum? ContractTypes { get; set; }
        public DateTime? Starting { get; set; }
        public DateTime? Available { get; set; }
    }

    public class BenchRateUpdateViewModel
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public RateEnum RateType { get; set; }
    }

    public class InviteViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class InviteCodeViewModel: InviteViewModel
    {
        public InviteCodeViewModel() { }
        public InviteCodeViewModel(InviteViewModel invite)
        {
            this.Name = invite.Name;
            this.Email = invite.Email;
        }
        public string Code { get; set; }
        public int? CompanyId { get; set; }
    }

    public class CreateJobViewModel : DictionaryViewModel
    {
        public string Description { get; set; }
        public IEnumerable<JobSkillEditViewModel> Skills { get; set; }
        public IEnumerable<JobLocationEditViewModel> Locations { get; set; }
        public int Rate { get; set; } //in 10's thousand.

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsWorkingFromHome { get; set; }
        public int Positions { get; set; }
        public int ExperienceStart { get; set; }
        public int ExperienceEnd { get; set; }
        public bool IsContractExtendable { get; set; }
        public bool IsContractToHire { get; set; }
    }

    [Flags]
    public enum AvailableEnum
    {
        Now=1,
        NextWeek=2,
        NextMonth=4,
        Later=8
    }
    
    public class CompanyFolderViewModel
    {
        internal int CompanyId { get; set; }
        public string Folder { get; set; }
    }

    public class ContractorSearchResultViewModel : ContractorViewModel
    {
        public DateTime Availability { get; set; }
        public int? Days { get; set; }
        public AvailableEnum Available { get; set; }
        
        public string Company { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsHighlight { get; set; }
        public bool IsAdvertised { get; set; }
        public bool IsHome { get; set; }
        public bool IsBench { get; set; }
    }

    public class JobSearchResultViewModel
    {
        [Field("text", Store = StoreMode.No)]
        public string SearchText
        {
            get { return string.Join(" ", new[] { Code, Title, Description, Company }); }
        }

        [NumericField]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Company { get; set; }

        public string PictureUrl { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string WebSite { get; set; }
        public string Industry { get; set; }


        public bool IsContractExtendable { get; set; }
        public bool IsContractToHire { get; set; }

        [NumericField]
        public int Rate { get; set; }

        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }

        [Field(Converter = typeof(SkillConverter))]
        public IEnumerable<DictionaryViewModel> Skills { get; set; }

        public string About { get; set; }
        public string Email { get; set; }
        public IEnumerable<DictionaryViewModel> Locations { get; set; }

        public DictionaryViewModel Location
        {
            get { return Locations.FirstOrDefault(); }
        }
        public int CompanyId { get; set; }
        public bool IsWorkingFromHome { get; set; }
        public int Positions { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        
        public bool IsPromoted { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsHighlight { get; set; }
        public bool IsAdvertised { get; set; }
        public bool IsHome { get; set; }
        public int ExperienceStart { get; set; }
        public int ExperienceEnd { get; set; }
    }

    public class SkillConverter : TypeConverter
    {

    }

    public class EditJobViewModel : CreateJobViewModel
    {
        public int Id { get; set; }

        public bool IsCancelled { get; set; }
        public DateTime? Cancelled { get; set; }

        public bool IsPublished { get; set; }
        public DateTime? Published { get; set; }
        
    }
    public class JobSearchFilterViewModel
    {
        public IEnumerable<FilterLabel<int>> Companies { get; set; }
        public IEnumerable<FilterLabel<int>> Skills { get; set; }
        public IEnumerable<FilterLabel<int>> Industries { get; set; }
        public IEnumerable<FilterLabel<int>> Functionals { get; set; }
        public IEnumerable<FilterLabel<int, ContractorTypeEnum>> ConsultantTypes { get; set; }
        public IEnumerable<FilterLabel<int, ContractTypeEnum>> ContractTypes { get; set; }
        public IEnumerable<FilterLabel<int, bool?>> ContractExtendable { get; set; }
        public IEnumerable<FilterLabel<int, bool?>> WorkFromHome { get; set; }
        public IEnumerable<FilterLabel<int, bool?>> ContractToHire { get; set; }
        public IEnumerable<FilterLabel<int>> Locations { get; set; }
        public MinMax Experience { get; set; }
        public IEnumerable<FilterLabel<int,int>> Rate { get; set; }
        public IEnumerable<FilterLabel<int, DateTime>> Starting { get; set; }
    }

    public class ContractorSearchFilterViewModel
    {
        public IEnumerable<FilterLabel<int>> Companies { get; set; }
        public IEnumerable<FilterLabel<int>> Skills { get; set; }
        public IEnumerable<FilterLabel<int>> Industries { get; set; }
        //public IEnumerable<CountLabel<int, bool>> Bench { get; set; }
        public IEnumerable<FilterLabel<int>> Functionals { get; set; }
        public IEnumerable<FilterLabel<int, ContractorTypeEnum>> ConsultantTypes { get; set; }
        public IEnumerable<FilterLabel<int, ContractTypeEnum>> ContractTypes { get; set; }
        public IEnumerable<FilterLabel<int>> Locations { get; set; }
        public MinMax Experience { get; set; }
        public IEnumerable<FilterLabel<int, int>> Rate { get; set; }
        public IEnumerable<FilterLabel<int, DateTime>> Available { get; set; }
    }

    public class JobTinyViewModel : EditJobViewModel
    {
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
    }

    public class JobViewModel : EditJobViewModel
    {
        public int CompanyId { get; set; }
        public int Applied { get; set; }
        public string Company { get; set; }

        public DictionaryViewModel Location
        {
            get { return Locations.FirstOrDefault(); }
        }

        public DateTime? Expiry { get; set; }
        public int NewApplications { get; set; }
    }

    public class DeleteJobViewModel : IdModel { }
    public class CancelJobViewModel : IdModel { }
    public class PublishJobViewModel : IdModel { }

    public class PromoteJobViewModel : IdModel
    {
        public PromotionEnum Promotion { get; set; }
    }

    public class CompanySearchViewModel
    {
        public string Skills { get; set; }
        public string Locations { get; set; }
        public int Count { get; set; }
        public PromotionEnum? Promotion { get; set; }
    }

    public class FeaturedCompanyViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string PictureUrl { get; set; }
        public int Jobs { get; set; }
        public string WebSite { get; set; }
    }

    public class FeaturedContractorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int Experience { get; set; }
        public IEnumerable<string> Skills { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsHighlight { get; set; }
        public bool IsAdvertised { get; set; }
        public bool IsHome { get; set; }
    }

    public class StatsViewModel
    {
        public int Members
        {
            get { return Companies + Contractors; } 
        }

        public int Jobs { get; set; }

        public int Companies { get; set; }
        public int Contractors { get; set; }
    }
}