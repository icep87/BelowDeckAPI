using System;
using Microsoft.AspNetCore.Identity;

namespace BelowDeckAPI.Models.Persistence
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
