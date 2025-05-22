using Users.Models.Db;
using UserService.Models.Dto.Models;

namespace UserService.Mappers.Models.Interfaces;

public interface IAdminUserProfileInfoMapper
{
	AdminUserProfileInfo? Map(DbUser user);
}
