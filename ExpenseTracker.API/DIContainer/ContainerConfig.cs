using Autofac;
using ExpenseTracker.BusinessLogic;
using ExpenseTracker.DataAccess;
using ExpenseTracker.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ExpenseTracker.API.DIContainer
{
	public static class ContainerConfig
	{
		public static IContainer ConfigureWithEntityFramework()
		{
			var builder = new ContainerBuilder();

			// Configure Entity Framework
			builder.Register(c =>
			{
				var config = c.Resolve<IConfiguration>();

				var opt = new DbContextOptionsBuilder<ExpenseTrackerDbContext>();
				opt.UseSqlServer(config.GetSection("ConnectionStrings:ExpenseTrackerConnectionString").Value);

				return new ExpenseTrackerDbContext(opt.Options);
			}).InstancePerLifetimeScope();

			builder.ConfigureGeneralServices();
			return builder.Build();
		}
		public static void ConfigureGeneralServices(this ContainerBuilder builder)
		{
			builder.RegisterType<ExpenseServiceEntityFramework>().As<IExpenseService>();
			builder.RegisterType<ExpensesModule>().As<IExpensesModule>();
		}
	}
}
