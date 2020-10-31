using AutoMapper;
using PluseDemoProject.Areas.DbQueryModel;
using PluseDemoProject.Areas.ViewModel;

namespace PluseDemoProject.AutoMapperProfiles
{
	public class UserListProfileProfile : Profile
	{
		public UserListProfileProfile()
		{
			CreateMap<UserListDbQueryModel, UserModel>();
		}
	}
}
