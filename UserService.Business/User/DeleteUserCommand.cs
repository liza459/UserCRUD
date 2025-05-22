using UserService.Business.User.Interfaces;
using UserService.Data.Interfaces;
using UserService.Models.Dto.Response;

namespace UserService.Business.User;

public class DeleteUserCommand : IDeleteUserCommand
{
	private readonly IUserRepository _userRepository;

	public DeleteUserCommand(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, bool deleteHard, string revokedBy)
	{
		var success = deleteHard
			? await _userRepository.HardDeleteAsync(userGuid)
			: await _userRepository.SoftDeleteAsync(userGuid, revokedBy);

		return new OperationResultResponse<bool> { Body = success };
	}
}

