using Users.Models.Db;
using UserService.Mappers.Db.Interfaces;
using UserService.Models.Dto.Requests;

namespace UserService.Mappers.Db;

public class DbUserMapper : IDbUserMapper
{
	public DbUser Map(CreateUserRequest request, string createdBy)
	{
		return request is null
		  ? null
		  : new DbUser
		  {
			  Guid = Guid.NewGuid(),
			  Login = request.Login,
			  Password = request.Password,
			  Name = request.Name,
			  Gender = request.Gender,
			  Birthday = request.Birthday,
			  Admin = request.Admin,
			  CreatedOn = DateTime.UtcNow,
			  CreatedBy = createdBy,
			  ModifiedBy = "",
			  RevokedBy = ""
		  };
	}
}