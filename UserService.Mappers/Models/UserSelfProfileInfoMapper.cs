using Users.Models.Db;
using UserService.Mappers.Models.Interfaces;
using UserService.Models.Dto.Models;

namespace UserService.Mappers.Models;

public class UserSelfProfileInfoMapper : IUserSelfProfileInfoMapper
{
	public UserSelfProfileInfo? Map(DbUser user)
	{
		return user is null
		  ? null
		  : new UserSelfProfileInfo
		  {
				Guid = user.Guid,
				Login = user.Login,
				Name = user.Name,
				Gender = user.Gender,
				Birthday = user.Birthday,
				Admin = user.Admin,
		  };
	}
}