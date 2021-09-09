using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BelowDeckAPI.Models;
using BelowDeckAPI.Models.Persistence;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BelowDeckAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Authentication : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public Authentication(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            //check that the model is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Get the user information by userName
            var user = await userManager.FindByNameAsync(model.UserName);


            //If user is found and password matches 
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                //Create claims for the token, we will add UserName to the claim
                var claims = new List<Claim>
                {
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //Signingkey
                SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ClientSecrets:Value"]));

                //Let's prepare data for the token creation
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Issuer = configuration["ClientSecrets:Issuer"],
                    Audience = configuration["ClientSecrets:Audience"],
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(claims)
                };

                var token = tokenHandler.CreateToken(tokenDescription);

                //Return token if everything is okey 
                return new OkObjectResult(new TokenDetails(
                    Token: new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration: token.ValidTo,
                    User: user,
                    Role: userRoles

                ));
            } 

            return new BadRequestObjectResult($"Incorrect username or password for the user {model.UserName}");

        }
    }
}
