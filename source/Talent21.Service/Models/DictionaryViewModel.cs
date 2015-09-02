using System;
using Talent21.Data.Core;

namespace Talent21.Service.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public bool IsSuccess { get; set; }
        public int Credit { get; set; }
        public string Code { get; set; }
        public string PaymentCapture { get; set; }
        public string Name { get; set; }

        public float Amount { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public string Mode { get; set; }
        public bool IsFailed { get; set; }
    }
    public class DictionaryViewModel  
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }
    public class DictionaryEditViewModel : DictionaryViewModel
    {
        public int Id { get; set; }
    }

    public class ContractorSkillViewModel : ContractorSkillEditViewModel
    {
    }

    public class JobSkillViewModel : JobSkillEditViewModel
    {
    }

    public class ContractorSkillEditViewModel : ContractorSkillCreateViewModel
    {
        public int Id { get; set; }
    }

    public class JobLocationEditViewModel : DictionaryViewModel
    {
        public int Id { get; set; }
    }
    public class JobSkillEditViewModel : JobSkillCreateViewModel
    {
        public int Id { get; set; }
    }

    public class JobSkillCreateViewModel : DictionaryViewModel
    {
        public LevelEnum Level { get; set; }
    }

    public class ContractorSkillCreateViewModel : DictionaryViewModel
    {
        public LevelEnum Level { get; set; }
        public ProficiencyEnum Proficiency { get; set; }
        public int ExperienceInMonths { get; set; }
    }

    public class ContractorSkillDeleteViewModel : IdModel
    {
    }
}