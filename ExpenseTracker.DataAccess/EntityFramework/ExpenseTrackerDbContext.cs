using ExpenseTracker.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.DataAccess.EntityFramework
{
	public class ExpenseTrackerDbContext : DbContext
	{
		public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
		{
		}

		public DbSet<Expense> Expenses { get; set; }
	}
}
