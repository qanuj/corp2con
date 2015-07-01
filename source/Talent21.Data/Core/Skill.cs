using System.Collections;
using System.Collections.Generic;
using e10.Shared.Data.Abstraction;

namespace Talent21.Data.Core
{
    public class Skill : Dictionary
    {
        public IList<Job> Jobs { get; set; }
        public IList<Candidate> Candidates { get; set; }
    }
}