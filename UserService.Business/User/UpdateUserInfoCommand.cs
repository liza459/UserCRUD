using UserService.Business.User.Interfaces;
using UserService.Data.Interfaces;
using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User;

public class UpdateUserInfoCommand : IUpdateUserInfoCommand
{
	private readonly IUserRepository _userRepository;

	public UpdateUserInfoCommand(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<OperationResultResponse<bool>> ExecuteAsync(Guid userGuid, UpdateUserRequest request, string modifiedBy)
	{
		if (request.Birthday.HasValue)
		{
			var dt = request.Birthday.Value;
			if (dt.Kind == DateTimeKind.Unspecified)
			{
				dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);
			}
			request.Birthday = dt.ToUniversalTime();
		}

		var user = await _userRepository.GetAsync(userGuid);
		if (user == null || user.RevokedOn != null)
		{
			return new OperationResultResponse<bool> { Errors = new List<string> { "User is not found or has been revoked." } };
		}

		user.Name = string.IsNullOrEmpty(request.Name) ? user.Name: request.Name;
		user.Gender = request.Gender.HasValue ? request.Gender.Value: user.Gender;
		user.Birthday = request.Birthday.HasValue ? request.Birthday.Value : user.Birthday;

		user.ModifiedBy = modifiedBy;
		user.ModifiedOn = DateTime.UtcNow;

		var success = await _userRepository.UpdateUserAsync(user);
		return new OperationResultResponse<bool> { Body = success };
	}
}