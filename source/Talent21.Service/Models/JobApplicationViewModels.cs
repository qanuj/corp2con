using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Talent21.Data.Core;
namespace Talent21.Service.Models
{
    public class JobApplicationViewModel
    {
        public int Id { get; set; }
        public IEnumerable<JobApplicationHistoryViewModel> Actions { get; set; }
        public ContractorViewModel Contractor { get; set; }
    }

    public class JobApplicationHistoryViewModel
    {
        public JobActionEnum Act { get; set; }
        public DateTime Created { get; set; }
        public string CreateBy { get; set; }
    }

    public class CandidateActJobApplicationViewModel
    {
        public int CandidateId { get; set; }
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
