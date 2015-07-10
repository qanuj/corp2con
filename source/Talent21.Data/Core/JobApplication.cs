using System.Collections;
using System.Collections.Generic;
using e10.Shared.Data.Abstraction;
using System;

namespace Talent21.Data.Core
{
    public class JobApplication : Entity
    {
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }

        public Job Job { get; set; }
        public int JobId { get; set; }

        public IList<JobApplicationHistory> History { get; set; }

        public bool IsRevoked { get; set; }
        public DateTime Revoked { get; set; }
        public string Folder { get; set; }

        public JobApplication()
        {
            this.History=new List<JobApplicationHistory>();
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