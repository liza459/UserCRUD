using UserService.Business.User.Interfaces;
using UserService.Data.Interfaces;
using UserService.Mappers.Models.Interfaces;
using UserService.Models.Dto.Models;
using UserService.Models.Dto.Response;

namespace UserService.Business.User;

public class GetUserByLoginCommand : IGetUserByLoginCommand
{
	private readonly IUserRepository _userRepository;
	private readonly IAdminUserProfileInfoMapper _mapper;


	public GetUserByLoginCommand(
		IUserRepository userRepository,
		IAdminUserProfileInfoMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<OperationResultResponse<AdminUserProfileInfo>> ExecuteAsync(string login)
	{
		var response = await _userRepository.GetUserByLoginAsync(login);
		if (response == null)
		{
			return new OperationResultResponse<AdminUserProfileInfo> { Errors = new List<string> { "User not found or invalid credentials." } };
		}

		AdminUserProfileInfo user = _mapper.Map(response);

		return new OperationResultResponse<AdminUserProfileInfo> { Body = user };
	}
}
