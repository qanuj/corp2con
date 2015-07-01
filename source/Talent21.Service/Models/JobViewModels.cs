using System;
using System.Collections.Generic;
using Talent21.Data.Core;

namespace Talent21.Service.Models
{
    public class CreateJobViewModel : DictionaryViewModel
    {
        public string Description { get; set; }
        public int LocationId { get; set; }
        public IEnumerable<SkillDictionaryViewModel> Skills { get; set; }

        public int Rate { get; set; } //in 10's thousand.

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }

    public class EditJobViewModel : CreateJobViewModel
    {
        public int Id { get; set; }

        public bool IsCancelled { get; set; }
        public DateTime? Cancelled { get; set; }

        public bool IsPublished { get; set; }
        public DateTime? Published { get; set; }
    }

    public class JobViewModels : EditJobViewModel
    {
        public int CompanyId { get; set; }
        public string Location { get; set; }
        public int Applied { get; set; }
        public string Company { get; set; }
    }
}