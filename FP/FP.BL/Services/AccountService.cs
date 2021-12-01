using FP.Entities;
using FP.Interfaces.Account;
using FP.Interfaces.Common.ConfigurationModels;
using FP.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FP.BL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IJwtConfiguration jwtConfiguration;

        public AccountService(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration,
            IJwtConfiguration jwtConfiguration)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.jwtConfiguration = jwtConfiguration;
        }

        public async Task<LoginResponseViewModel> LoginAsync(LoginRequestViewModel credentials)
        {
            var user = await GetUserByName(credentials.Username);

            var result = await signInManager.PasswordSignInAsync(user, credentials.Password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception("Wrong email or password!");
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: this.configuration["JWT:ValidIssuer"],
                audience: this.configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var loginResponse = new LoginResponseViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
             };

            return loginResponse;
        }

        private async Task<AppUser> GetUserByName(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == default)
            {
                throw new Exception($"User {username} doesn't exist!");
            }

            return user;
        }

        public async Task<RegisterResponseViewModel> RegisterAsync(RegisterRequestViewModel registrationDetails)
        {
            var userExists = await userManager.FindByNameAsync(registrationDetails.Username);

            var registerUserExistResponse = new RegisterResponseViewModel
            {
                Status = "Error",
                Message = "User already exists!"
            };

            var registerUserDetailsResponse = new RegisterResponseViewModel
            {
                Status = "Error",
                Message = "User creation failed! Please check user details and try again."
            };

            var registerResponse = new RegisterResponseViewModel
            {
                Status = "Success",
                Message = "User created successfully!"
            };

            if (userExists != null)
            {
                return registerUserExistResponse;
            }

            AppUser user = new AppUser()
            {
                Email = registrationDetails.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registrationDetails.Username
            };
            var result = await userManager.CreateAsync(user, registrationDetails.Password);

            if (!result.Succeeded)
                return registerUserDetailsResponse;

            return registerResponse;
        }
    }
}
