using System;

namespace Talent21.Data.Core
{
    [Flags]
    public enum JobActionEnum
    {
        Application,
        Favorite,
        Reported,
        Rejected,
        Shortlist
    }
}