using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Data
{
	public class BookManagerContext : IdentityDbContext<ApplicationUser>
	{
		public BookManagerContext(DbContextOptions<BookManagerContext> opt) : base(opt) { }

		#region DbSet
		public DbSet<Book>? Books { get; set; }
		#endregion
	}
}
