using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talent21.Service.Models
{
    public class IndustryDictionaryEditViewModel : DictionaryEditViewModel { }
    public class IndustryDictionaryViewModel : IndustryDictionaryEditViewModel { }
    public class IndustryDictionaryCreateViewModel : IndustryDictionaryEditViewModel { }
    public class IndustryDeleteViewModel : IdModel { }

    public class LocationDictionaryEditViewModel : DictionaryEditViewModel
    {
        public string PinCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }
    public class LocationDictionaryViewModel : LocationDictionaryEditViewModel { }
    public class LocationDictionaryCreateViewModel : LocationDictionaryEditViewModel { }
    public class LocationDeleteViewModel : IdModel { }

    public class SkillDictionaryEditViewModel : DictionaryEditViewModel { }
    public class SkillDictionaryViewModel : SkillDictionaryEditViewModel{ }
    public class SkillDictionaryCreateViewModel : SkillDictionaryEditViewModel { }
    public class SkillDeleteViewModel : IdModel { }

    public class FunctionalAreaDictionaryEditViewModel : DictionaryEditViewModel { }
    public class FunctionalAreaDictionaryViewModel : FunctionalAreaDictionaryEditViewModel { }
    public class FunctionalAreaDictionaryCreateViewModel : FunctionalAreaDictionaryEditViewModel { }
    public class FunctionalAreaDeleteViewModel : IdModel { }

}
