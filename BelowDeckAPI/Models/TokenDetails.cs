using System;
using System.Collections.Generic;
using BelowDeckAPI.Models.Persistence;

namespace BelowDeckAPI.Models
{
    public record TokenDetails
    (
        string Token,
        DateTime Expiration,
        User User,
        IList<string> Role
    );
}
