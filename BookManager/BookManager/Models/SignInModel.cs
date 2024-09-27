﻿using System.ComponentModel.DataAnnotations;

namespace BookManager.Models
{
	public class SignInModel
	{
		[Required, EmailAddress]
		public string Email { get; set; } = null!;
		[Required]
		public string Password { get; set; } = null!;
	}
}
