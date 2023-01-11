namespace ExpenseTracker.DataAccess.Models
{
	public class Expense
	{
		public Guid Id { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public string? Description { get; set; }

		public Guid CreatedBy { get; set; }
		public DateTime CreatedDateTime { get; set; } = DateTime.Now;

	}
}
