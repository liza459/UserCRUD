using Users.Models.Db;
using UserService.Models.Dto.Models;

namespace UserService.Mappers.Models.Interfaces;

public interface IUserInfoMapper
{
	List<UserInfo?> Map(List<DbUser> users);
}