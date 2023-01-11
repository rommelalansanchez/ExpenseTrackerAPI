using ExpenseTracker.DataAccess.Models;

namespace ExpenseTracker.DataAccess
{
	public interface IExpenseServiceEntityFramework
	{
		void DeleteExpense(Guid id);
		Expense GetExpenseById(Guid id);
		List<Expense> GetExpenses();
		Guid InsertExpense(Expense expense);
		void UpdateExpense(Expense expense);
	}
}