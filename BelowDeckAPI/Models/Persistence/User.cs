using System;
using Microsoft.AspNetCore.Identity;

namespace BelowDeckAPI.Models.Persistence
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Profile_Image { get; set; }
        public string location { get; set; }



    }
}
