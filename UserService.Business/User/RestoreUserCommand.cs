using UserService.Business.User.Interfaces;
using UserService.Data.Interfaces;
using UserService.Models.Dto.Response;

namespace UserService.Business.User;

public class RestoreUserCommand : IRestoreUserCommand
{
	private readonly IUserRepository _userRepository;

	public RestoreUserCommand(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, string modifiedBy)
	{
		var success = await _userRepository.RestoreUserAsync(userGuid, modifiedBy);
		return new OperationResultResponse<bool> { Body = success };
	}
}
