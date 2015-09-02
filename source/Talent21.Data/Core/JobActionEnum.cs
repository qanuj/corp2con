using System;

namespace Talent21.Data.Core
{
    [Flags]
    public enum JobActionEnum
    {
        Application=0,
        Favorite=1,
        Reported=2,
        Rejected=3,
        Shortlist=4,
        Invited=5,
        Decline=6,
        Revoke=7
    }
}