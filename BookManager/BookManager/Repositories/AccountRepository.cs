﻿using BookManager.Data;
using BookManager.Helpers;
using BookManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookManager.Repositories
{
	public class AccountRepository : IAccountRepository
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly IConfiguration configuration;
		private readonly RoleManager<IdentityRole> roleManager;

		public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.configuration = configuration;
			this.roleManager = roleManager;
		}

		public async Task<string> SignInAsync(SignInModel model)
		{
			var user = await userManager.FindByEmailAsync(model.Email);
			var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

			if (user == null || !passwordValid)
			{
				return string.Empty;
			}

			var userRoles = await userManager.GetRolesAsync(user);

			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, model.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			foreach (var role in userRoles)
			{
				authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
			}

			var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(
				issuer: configuration["JWT:ValidIssuer"],
				audience: configuration["JWT:ValidAudience"],
				claims: authClaims,
				signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public async Task<IdentityResult> SignUpAsync(SignUpModel model)
		{
			var user = new ApplicationUser
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				UserName = model.Email,
			};
			var result = await userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				if (!await roleManager.RoleExistsAsync(AppRole.Customer))
					await roleManager.CreateAsync(new IdentityRole(AppRole.Customer));

				await userManager.AddToRoleAsync(user, AppRole.Customer);

			}
			return result;
		}
	}
}
