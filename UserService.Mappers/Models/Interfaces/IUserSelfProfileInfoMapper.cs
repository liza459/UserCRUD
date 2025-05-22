using Users.Models.Db;
using UserService.Models.Dto.Models;

namespace UserService.Mappers.Models.Interfaces;

public interface IUserSelfProfileInfoMapper
{
	UserSelfProfileInfo? Map(DbUser users);
}
