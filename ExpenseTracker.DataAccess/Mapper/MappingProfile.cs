using AutoMapper;

namespace ExpenseTracker.DataAccess.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Models.Expense, EntityFramework.Entities.Expense>();
			CreateMap<EntityFramework.Entities.Expense, Models.Expense>();
		}
	}
}
