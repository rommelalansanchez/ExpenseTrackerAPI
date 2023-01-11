using ExpenseTracker.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ExpenseTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExpenseController : ControllerBase
	{
		private readonly IExpensesModule _expensesModule;

		public ExpenseController(IExpensesModule expensesModule)
		{
			this._expensesModule = expensesModule;
		}

		[HttpGet(Name ="GetExpensesMethod")]
		public List<DataAccess.Models.Expense> GetExpenses()
		{
			return _expensesModule.GetExpenses();
		}
	}
}
