using UserService.Business.User.Interfaces;
using UserService.Data.Interfaces;
using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User;

public class UpdatePasswordCommand : IUpdatePasswordCommand
{
	private readonly IUserRepository _userRepository;

	public UpdatePasswordCommand(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, UpdatePasswordRequest request, string modifiedBy)
	{
		var user = await _userRepository.GetAsync(userGuid);
		if (user == null || user.RevokedOn != null)
		{
			return new OperationResultResponse<bool> { Errors = new List<string> { "User is not found or has been revoked." } };
		}

		user.Password = request.NewPassword;
		user.ModifiedBy = modifiedBy;
		user.ModifiedOn = DateTime.UtcNow;

		var success = await _userRepository.UpdateUserAsync(user);
		return new OperationResultResponse<bool> { Body = success };
	}
}
