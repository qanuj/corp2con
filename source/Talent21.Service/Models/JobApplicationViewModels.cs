using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Talent21.Data.Core;
namespace Talent21.Service.Models
{
    public class JobApplicationCompanyViewModel : JobApplicationViewModel
    {
        public ContractorViewModel Contractor { get; set; }
    }

    public class JobApplicationViewModel
    {
        public int Id { get; set; }
        public IEnumerable<JobApplicationHistoryViewModel> Actions { get; set; }
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

    public class JobApplicationContractorViewModel : JobApplicationViewModel
    {
        public JobViewModel Job { get; set; }
    }

    public class JobApplicationHistoryViewModel
    {
        public JobActionEnum Act { get; set; }
        public DateTime Created { get; set; }
        public string CreateBy { get; set; }
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

    public class MoveJobApplicationViewModel
    {
        public int Id { get; set; }
        public string Folder { get; set; }
    }

    public class CreateJobApplicationHistoryViewModel
    {
        public int Id { get; set; }
        public string Notes { get; set; }
    }
}
