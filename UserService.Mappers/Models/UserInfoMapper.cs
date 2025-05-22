using Users.Models.Db;
using UserService.Mappers.Models.Interfaces;
using UserService.Models.Dto.Models;

namespace UserService.Mappers.Models;

public class UserInfoMapper : IUserInfoMapper
{
	public List<UserInfo>? Map(List<DbUser> users)
	{
		return users?.Select(u => new UserInfo
		{
			Guid = u.Guid,
			Login = u.Login,
			Name = u.Name,
			Gender = u.Gender,
			Birthday = u.Birthday,
			Admin = u.Admin,
			CreatedOn = u.CreatedOn,
			CreatedBy = u.CreatedBy,
			ModifiedOn = u.ModifiedOn,
			ModifiedBy = u.ModifiedBy,
			RevokedOn = u.RevokedOn,
			RevokedBy = u.RevokedBy,
		}).ToList();
	}
}