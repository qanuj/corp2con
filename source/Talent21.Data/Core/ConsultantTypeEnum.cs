using System;

namespace Talent21.Data.Core
{
    [Flags]
    public enum ContractorTypeEnum
    {
        IndependentConsultant=0,
        AlignedToConsultingOrganistion=1,
        RetiredProfessional=2
    }
}