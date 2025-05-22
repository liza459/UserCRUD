using UserService.Models.Dto.Models;
using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface IGetUserByLoginCommand
{
	Task<OperationResultResponse<AdminUserProfileInfo>> ExecuteAsync(string login);
}