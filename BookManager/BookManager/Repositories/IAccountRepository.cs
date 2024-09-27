using BookManager.Models;
using Microsoft.AspNetCore.Identity;

namespace BookManager.Repositories
{
	public interface IAccountRepository
	{
		public Task<IdentityResult> SignUpAsync(SignUpModel model);
		public Task<string> SignInAsync(SignInModel model);
	}
}
