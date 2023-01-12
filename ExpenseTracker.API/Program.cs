using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using ExpenseTracker.API.DIContainer;
using ExpenseTracker.BusinessLogic;
using ExpenseTracker.DataAccess;
using ExpenseTracker.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Console;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{

	//Configure native logger 
	//containerBuilder.RegisterInstance(new LoggerFactory())
	//			.As<ILoggerFactory>();

	//containerBuilder.RegisterGeneric(typeof(Logger<>))
	//	   .As(typeof(ILogger<>))
	//	   .SingleInstance();

	//Configure Serilog
	containerBuilder.Register<Serilog.ILogger>((c, p) =>
	{
		var loggerConf = new LoggerConfiguration()
		.WriteTo.File(new JsonFormatter(),
		"important-logs.json",
		restrictedToMinimumLevel: LogEventLevel.Warning)

		// Add a log file that will be replaced by a new log file each day
		.WriteTo.File("all-daily-.logs",
		rollingInterval: RollingInterval.Day)

		  //.WriteTo.RollingFile(
		  //	AppDomain.CurrentDomain.GetData("DataDirectory")?.ToString() + "/Log-{Date}.txt")
		  .CreateLogger();

		return loggerConf;
	}).SingleInstance();

	// Configure Automap
	containerBuilder.Register<IMapper>((a, b) =>
	{
		var mapperConfig = new MapperConfiguration(m =>
		{
			m.AddProfile(new ExpenseTracker.DataAccess.Mapper.MappingProfile());
		});

		return mapperConfig.CreateMapper();
	}).SingleInstance();


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
