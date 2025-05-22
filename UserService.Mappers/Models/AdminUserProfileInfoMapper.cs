using Users.Models.Db;
using UserService.Mappers.Models.Interfaces;
using UserService.Models.Dto.Models;

namespace UserService.Mappers.Models;

public class AdminUserProfileInfoMapper : IAdminUserProfileInfoMapper
{
	public AdminUserProfileInfo? Map(DbUser user)
	{
		return user is null
		  ? null
		  : new AdminUserProfileInfo
		{
			Guid = user.Guid,
			Login = user.Login,
			Name = user.Name,
			Gender = user.Gender,
			Birthday = user.Birthday,
			IsActive = user.RevokedOn == null,
		};
	}
}