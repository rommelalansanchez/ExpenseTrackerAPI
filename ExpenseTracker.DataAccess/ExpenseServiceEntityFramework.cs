using AutoMapper;
using ExpenseTracker.DataAccess.EntityFramework;
using ExpenseTracker.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.DataAccess
{
	public class ExpenseServiceEntityFramework : IExpenseService, IExpenseServiceEntityFramework
	{
		//private readonly ILogger _logger;
		private readonly ExpenseTrackerDbContext _dbContext;
		private readonly IMapper _mapper;

		public ExpenseServiceEntityFramework(/*ILogger logger,*/ ExpenseTrackerDbContext dbContext)
		{
			//this._logger = logger;
			this._dbContext = dbContext;
			var mapperConfig = new MapperConfiguration(m =>
			{
				m.AddProfile(new Mapper.MappingProfile());
			});
			_mapper = mapperConfig.CreateMapper();

		}
		public List<Expense> GetExpenses()
		{
			return _dbContext.Expenses.Select(e => _mapper.Map<Expense>(e)).ToList();
		}

		public Expense GetExpenseById(Guid id)
		{
			var exp = _dbContext.Expenses.Where(x => x.Id == id).Select(e => _mapper.Map<Expense>(e)).FirstOrDefault();
			if (exp == null)
			{
				//_logger.LogInformation("Expense with id \"{id}\" was not found.", id);
			}

			return exp;
		}

		public Guid InsertExpense(Expense expense)
		{
			expense.Id = Guid.NewGuid();
			var newExpense = _mapper.Map<EntityFramework.Entities.Expense>(expense);
			_dbContext.Expenses.Add(newExpense);
			return expense.Id;
		}

		public void UpdateExpense(Expense expense)
		{
			var exp = _dbContext.Expenses.Where(x => x.Id == expense.Id).Select(e => _mapper.Map<Expense>(e)).FirstOrDefault();

			if (exp == null)
			{
				//_logger.LogInformation("Expense with id \"{id}\" was not found.", expense.Id);
			}
			else
			{
				exp.Amount = expense.Amount;
				exp.Date = expense.Date;
				exp.Description = expense.Description;
				_dbContext.SaveChanges();
			}

		}
		public void DeleteExpense(Guid id)
		{
			var exp = _dbContext.Expenses.Where(x => x.Id == id).Select(e => _mapper.Map<EntityFramework.Entities.Expense>(e)).FirstOrDefault();
			if (exp == null)
			{
				//_logger.LogInformation("Expense with id \"{id}\" was not found.", id);
			}
			else
			{
				_dbContext.Expenses.Remove(exp);
				_dbContext.SaveChanges();
			}
		}
	}
}
