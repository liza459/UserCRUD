using UserService.Models.Dto.Models;
using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface IGetUserByLoginAndPasswordCommand
{
	Task<OperationResultResponse<UserSelfProfileInfo>> ExecuteAsync(GetLoginRequest request);
}
