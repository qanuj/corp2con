using System.Collections;
using System.Collections.Generic;
using e10.Shared.Data.Abstraction;
using System;

namespace Talent21.Data.Core
{
    public class JobApplication : Entity
    {
        public Contractor Contractor { get; set; }
        public int ContractorId { get; set; }

        public Job Job { get; set; }
        public int JobId { get; set; }

        public IList<JobApplicationHistory> History { get; set; }

        public bool IsRevoked { get; set; }
        public DateTime? Revoked { get; set; }
        public string Folder { get; set; }

        public JobApplication()
        {
            this.History=new List<JobApplicationHistory>();
        }

        public string DownloadableName()
        {
            return string.Format("{0}_{1}_{2}_{3}yrs{4}", 
                Contractor.FirstName,
                Contractor.LastName,
                Contractor.Experience.Years,
                Contractor.Experience.Months, Contractor.Company!=null ? "_"+Contractor.Company.CompanyName:"").Replace(" ", "_").Replace(".", "_");
        }
    }

    public class JobApplicationHistory : Entity
    {
        public JobApplication Application { get; set; }
        public int ApplicationId { get; set; }

        public JobActionEnum Act { get; set; }
        public string Notes { get; set; }
    }
}