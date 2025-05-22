using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface IRestoreUserCommand
{
	Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, string ModifiedBy);
}