using System;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Schedule : Entity
    {
       /// <summary>
       /// 
       /// </summary>
        public Candidate Candidate { get; set; }
        public int CandidateId { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public bool IsAvailable { get; set; }
    }
}