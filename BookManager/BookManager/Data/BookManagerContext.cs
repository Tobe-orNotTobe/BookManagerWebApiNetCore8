using Microsoft.EntityFrameworkCore;

namespace BookManager.Data
{
	public class BookManagerContext : DbContext
	{
		public BookManagerContext(DbContextOptions<BookManagerContext> opt) : base(opt) { }

		#region DbSet
		public DbSet<Book>? Books { get; set; }
		#endregion
	}
}
