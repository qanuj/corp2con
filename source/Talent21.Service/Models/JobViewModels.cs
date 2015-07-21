using System;
using System.Collections.Generic;
using System.ComponentModel;
using Lucene.Net.Linq.Mapping;
using Talent21.Data.Core;

namespace Talent21.Service.Models
{
    public class SearchJobViewModel
    {
        public string Location { get; set; }
        public string Skills { get; set; }
        public string Keywords { get; set; }
    }

    public class CreateJobViewModel : DictionaryViewModel
    {
        public string Description { get; set; }
        public int LocationId { get; set; }
        public IEnumerable<JobSkillEditViewModel> Skills { get; set; }

        public int Rate { get; set; } //in 10's thousand.

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }

    public class JobSearchResultViewModel
    {
        [Field("text", Store = StoreMode.No)]
        public string SearchText
        {
            get { return string.Join(" ", new[] { Code, Title, Description, Location, Company }); }
        }

        [NumericField]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Company { get; set; }

        [NumericField]
        public int Rate { get; set; }

        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }

        [Field(Converter = typeof(SkillConverter))]
        public IEnumerable<DictionaryViewModel> Skills { get; set; }

    }

    public class SkillConverter : TypeConverter
    {
        
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