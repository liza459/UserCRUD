using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface IUpdateLoginCommand
{
	Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, UpdateLoginRequest request, string modifiedBy);
}