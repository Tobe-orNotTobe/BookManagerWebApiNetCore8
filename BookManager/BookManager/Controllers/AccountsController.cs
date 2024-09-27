using BookManager.Models;
using BookManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IAccountRepository accountRepo;

		public AccountsController(IAccountRepository repo)
		{
			accountRepo = repo;
		}

		[HttpPost("SignUp")]
		public async Task<IActionResult> SignUp(SignUpModel model)
		{
			var result = await accountRepo.SignUpAsync(model);
			if (result.Succeeded)
			{
				return Ok(result.Succeeded);
			}
			return Unauthorized();

		}

		[HttpPost("SignIn")]
		public async Task<IActionResult> SignIn(SignInModel model)
		{
			var result = await accountRepo.SignInAsync(model);

			if (string.IsNullOrEmpty(result))
				return Unauthorized();

			return Ok(result);
		}
	}
}
