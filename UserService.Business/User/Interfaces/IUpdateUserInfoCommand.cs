using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface IUpdateUserInfoCommand
{
	Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, UpdateUserRequest request, string modifiedBy);
}