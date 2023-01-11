using ExpenseTracker.DataAccess;
using Expense = ExpenseTracker.DataAccess.Models.Expense;

namespace ExpenseTracker.BusinessLogic
{
	public class ExpensesModule : IExpensesModule
	{
		private readonly IExpenseService _expenseService;

		public ExpensesModule(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}
		public List<Expense> GetExpenses()
		{
			return _expenseService.GetExpenses();
		}

		public Expense GetExpenseById(Guid id)
		{
			return _expenseService.GetExpenseById(id);
		}

		public Guid InsertExpense(Expense expense)
		{
			return _expenseService.InsertExpense(expense);
		}

		public void UpdateExpense(Expense expense)
		{
			_expenseService.UpdateExpense(expense);
		}
		public void DeleteExpense(Guid id)
		{
			_expenseService.DeleteExpense(id);

		}
	}
}
