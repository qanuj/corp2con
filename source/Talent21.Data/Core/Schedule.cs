using System;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Schedule : Entity
    {
       /// <summary>
       /// 
       /// </summary>
        public Contractor Contractor { get; set; }
        public int ContractorId { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public bool IsAvailable { get; set; }

        public string Description { get; set; }
    }
}