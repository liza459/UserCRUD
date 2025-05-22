using UserService.Models.Dto.Models;
using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface IGetAllActiveUsersCommand
{
	Task<OperationResultResponse<List<UserInfo>>> ExecuteAsync();
}
