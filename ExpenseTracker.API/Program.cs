using Autofac;
using Autofac.Extensions.DependencyInjection;
using ExpenseTracker.API.DIContainer;
using ExpenseTracker.BusinessLogic;
using ExpenseTracker.DataAccess;
using ExpenseTracker.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
	containerBuilder.RegisterInstance(new LoggerFactory())
				.As<ILoggerFactory>();

	containerBuilder.RegisterGeneric(typeof(Logger<>))
		   .As(typeof(ILogger<>))
		   .SingleInstance();

	containerBuilder.Register(c =>
	{
		var config = c.Resolve<IConfiguration>();

		var opt = new DbContextOptionsBuilder<ExpenseTrackerDbContext>();
		opt.UseSqlServer(config.GetSection("ConnectionStrings:ExpenseTrackerConnectionString").Value);

		return new ExpenseTrackerDbContext(opt.Options);
	}).InstancePerLifetimeScope();
	containerBuilder.RegisterType<ExpenseServiceEntityFramework>().As<IExpenseService>();
	containerBuilder.RegisterType<ExpensesModule>().As<IExpensesModule>();
});

//container.BeginLifetimeScope();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
