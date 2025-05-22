using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface IUpdatePasswordCommand
{
	Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, UpdatePasswordRequest request, string modifiedBy);
}