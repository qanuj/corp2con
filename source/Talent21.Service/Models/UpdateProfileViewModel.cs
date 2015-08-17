using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Talent21.Data.Core;

namespace Talent21.Service.Models
{
    public class CompanyEditViewModel : PersonViewModel
    {
        public string CompanyName { get; set; }
        public OrganizationTypeEnum OrganizationType { get; set; }
        public int? IndustryId { get; set; }
    }

    public class CompanyCreateViewModel : CompanyEditViewModel
    {
        public string OwnerId { get; set; }
    }

    public class CompanyAggregateReport
    {
        public string Skill { get; set; }
        public string Location { get; set; }
        public MinMax<DateTime> Duration { get; set; }
        public MinMax Salary { get; set; }
    }

    public class MinMax : MinMax<int>
    {
    }
    public class MinMax<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }
    }

    public class CompanyDashboardViewModel
    {
        public int Views { get; set; }
        public int Jobs { get; set; }
        public int Applications { get; set; }
        public int Contractors { get; set; }
        public CompanyAggregateReport Aggregate { get; set; }
    }

    public class CompanyViewModel : CompanyEditViewModel
    {
        public DictionaryViewModel Industry { get; set; }
        public string OwnerId { get; set; }

        public int Complete { get; set; }
    }


    public class ContractorEditViewModel : PersonViewModel
    {
        public int ExperienceMonths { get; set; }
        public int ExperienceYears { get; set; }

        public int Rate { get; set; }
        public RateEnum RateType { get; set; }

        public string Nationality { get; set; }
        public int? FunctionalAreaId { get; set; }
        public int? IndustryId { get; set; }
        public ContractorTypeEnum ConsultantType { get; set; }
        public ContractTypeEnum ContractType { get; set; }
        public GenderEnum Gender { get; set; }

        public IEnumerable<ContractorSkillViewModel> Skills { get; set; }
    }

    public class ContractorCreateViewModel : ContractorEditViewModel
    {
        public string OwnerId { get; set; }
    }

    public class ContractorDashboardViewModel
    {
        public int Jobs { get; set; }
        public int Views { get; set; }
        public int Week { get; set; }
        public int Month { get; set; }
    }

    public class ContractorViewModel : ContractorEditViewModel
    {
        internal string OwnerId { get; set; }
        public string LocationCode { get; set; }

        public int Complete { get; set; }
    }

}
