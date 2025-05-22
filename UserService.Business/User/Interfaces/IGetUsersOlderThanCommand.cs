using UserService.Models.Dto.Models;
using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface IGetUsersOlderThanCommand
{
	Task<OperationResultResponse<List<UserInfo>>> ExecuteAsync(int age);
}
