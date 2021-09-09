using System;
using System.ComponentModel.DataAnnotations;

namespace BelowDeckAPI.Models
{
    public record Login
    (
        [Required(ErrorMessage = "Username is requried")] string UserName,
        [Required(ErrorMessage = "Password is required!")] string Password
    );
}
