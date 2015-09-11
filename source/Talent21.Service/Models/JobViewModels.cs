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
        public string Location { get; set; }
        public string Skills { get; set; }
        public string Keywords { get; set; }
        public string Folder { get; set; }
        public string Industry { get; set; }
        public string Functional { get; set; }
        public int xFrom { get; set; }
        public int xTo { get; set; }
        public int RateStart { get; set; }
        public int RateEnd { get; set; }
        public RateEnum? RateType { get; set; }
        public int CompanyId { get; set; }
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
    }

    public enum AvailableEnum
    {
        Now,
        NextWeek,
        NextMonth,
        Later
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

        public int? CompanyId { get; set; }
        public string Company { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsHighlight { get; set; }
        public bool IsAdvertised { get; set; }
        public bool IsHome { get; set; }
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

    public class FeaturedCompanyViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string PictureUrl { get; set; }
        public int Jobs { get; set; }
        public string WebSite { get; set; }
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