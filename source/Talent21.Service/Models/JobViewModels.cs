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

    public class JobSearchResultViewModel
    {

        public string Code { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Company { get; set; }

        public int Rate { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int Id { get; set; }

        public IEnumerable<DictionaryViewModel> Skills { get; set; }
        public bool IsFavorite { get; set; }

        public bool IsApplied { get; set; }
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
        public string Location { get; set; }
        public int Applied { get; set; }
        public string Company { get; set; }
    }

    public class DeleteJobViewModel : IdModel { }
    public class CancelJobViewModel : IdModel { }
    public class PublishJobViewModel : IdModel { }
}