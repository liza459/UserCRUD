using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User.Interfaces;

public interface ICreateUserCommand
{
	Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateUserRequest request, string createdBy);
}