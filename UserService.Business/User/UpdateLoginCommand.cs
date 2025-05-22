using UserService.Business.User.Interfaces;
using UserService.Data.Interfaces;
using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User;

public class UpdateLoginCommand : IUpdateLoginCommand
{
	private readonly IUserRepository _userRepository;

	public UpdateLoginCommand(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, UpdateLoginRequest request, string modifiedBy)
	{
		var user = await _userRepository.GetAsync(userGuid);
		if (user == null || user.RevokedOn != null)
		{
			return new OperationResultResponse<bool> { Errors = new List<string> { "User is not found or has been revoked." } };
		}

		if (await _userRepository.DoesExistLoginAsync(request.Login))
		{
			return new OperationResultResponse<bool> { Errors = new List<string> { "Login is already taken." } };
		}

		user.Login = request.Login;
		user.ModifiedBy = modifiedBy;
		user.ModifiedOn = DateTime.UtcNow;

		var success = await _userRepository.UpdateUserAsync(user);
		return new OperationResultResponse<bool> { Body = success };
	}
}

