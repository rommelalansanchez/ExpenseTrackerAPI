using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.DataAccess.EntityFramework.Entities
{
	[Table("Expense")]
	public class Expense
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public string? Description { get; set; }

		public Guid CreatedBy { get; set; }
		public DateTime CreatedDateTime { get; set; } = DateTime.Now;
	}
}
