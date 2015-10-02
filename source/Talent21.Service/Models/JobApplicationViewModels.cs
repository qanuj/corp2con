using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Talent21.Data.Core;
namespace Talent21.Service.Models
{
    public class VisitViewModel
    {
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Browser { get; set; }
        public string Referer { get; set; }
        public string OperatingSystem { get; set; }
        public bool IsMobile { get; set; }
    }

    public class JobApplicationViewModel
    {
        public IEnumerable<JobApplicationHistoryViewModel> Actions { get; set; }
        public int Id { get; set; }
    }
    public class JobApplicationCompanyViewModel : ContractorViewModel
    {
        public IEnumerable<JobApplicationHistoryViewModel> Actions { get; set; }
        public DictionaryEditViewModel Job { get; set; }
        public string Folder { get; set; }
        public int JobId { get; set; }
        public int AppicationId { get; set; }
    }

    public class AvailableRatedCandidateProfileViewModel : CandidateProfileViewModel
    {
        public int Rating { get; set; }
        public DateTime Availability { get; set; }
    }

    public class CandidateProfileViewModel : PictureViewModel
    {
        public string Skill { get; set; }
        public int Rate { get; set; }
        public int ExperienceInMonths { get; set; }
        public int ExperienceInYears { get; set; }
    }

    public class PictureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }

    public class JobPublicViewModel
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? Cancelled { get; set; }
        public DateTime? Published { get; set; }

        public IEnumerable<JobSkillEditViewModel> Skills { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public DateTime End { get; set; }
        public int Rate { get; set; }
        public DateTime Start { get; set; }
        public IEnumerable<JobLocationEditViewModel> Locations { get; set; }
        public bool IsWorkingFromHome { get; set; }
        public int Positions { get; set; }
        public string JobCode { get; set; }
        public string PictureUrl { get; set; }
    }

    public class JobApplicationContractorViewModel : JobApplicationViewModel
    {
        public string Company { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? Cancelled { get; set; }
        public DateTime? Published { get; set; }

        public IEnumerable<JobSkillEditViewModel> Skills { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public DateTime End { get; set; }
        public int Rate { get; set; }
        public DateTime Start { get; set; }
        public IEnumerable<JobLocationEditViewModel> Locations { get; set; }
        public bool IsWorkingFromHome { get; set; }
        public int Positions { get; set; }
    }

    public class JobBasedJobApplicationHistoryViewModel
    {
        public int Id { get; set; }
        public IEnumerable<JobApplicationHistoryViewModel> History { get; set; }
    }

    public class JobApplicationHistoryViewModel
    {
        public JobActionEnum Act { get; set; }
        public DateTime Created { get; set; }
        public string CreateBy { get; set; }
        public int ApplicationId { get; set; }
    }

    public class ContractorActJobApplicationViewModel
    {
        public int ContractorId { get; set; }
        public int JobId { get; set; }
        public JobActionEnum Act { get; set; }
    }

    public class JobApplicationCreateViewModel
    {
        public int Id { get; set; }
    }

    public class CompanyActJobApplicationViewModel : CreateJobApplicationHistoryViewModel
    {
        public JobActionEnum Act { get; set; }

        public CompanyActJobApplicationViewModel()
        {
        }

        public CompanyActJobApplicationViewModel(int id, JobActionEnum act)
        {
            this.Id = id;
            this.Act = act;
        }
        public CompanyActJobApplicationViewModel(CreateJobApplicationHistoryViewModel model, JobActionEnum act):this(model.Id,act)
        {
            this.Notes = model.Notes;
        }
    }


    public class JobInviteViewModel
    {
        public int ContractorId { get; set; }
        public int JobId { get; set; }
    }

    public class FolderMoveViewModel
    {
        public int Id { get; set; }
        public string Folder { get; set; }
    }

    public class JobDeclineViewModel
    {
        public int JobId { get; set; }
        public string Reason { get; set; }
    }
    public class CreateJobApplicationHistoryViewModel
    {
        public int Id { get; set; }
        public string Notes { get; set; }
    }

    public class DeleteJobApplicationHistoryViewModel
    {
        public int Id { get; set; }
        public string Notes { get; set; }
    }
}
