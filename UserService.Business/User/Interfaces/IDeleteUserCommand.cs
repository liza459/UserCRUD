using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface IDeleteUserCommand
{
	Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, bool deleteHard, string revokedBy);
}