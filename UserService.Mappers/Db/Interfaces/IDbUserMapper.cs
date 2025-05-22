using Users.Models.Db;
using UserService.Models.Dto.Requests;

namespace UserService.Mappers.Db.Interfaces;

public interface IDbUserMapper
{
	DbUser Map(CreateUserRequest request, string createdBy);
}