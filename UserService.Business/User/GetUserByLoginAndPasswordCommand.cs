using UserService.Business.User.Interfaces;
using UserService.Data.Interfaces;
using UserService.Mappers.Models.Interfaces;
using UserService.Models.Dto.Models;
using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User;

public class GetUserByLoginAndPasswordCommand : IGetUserByLoginAndPasswordCommand
{
	private readonly IUserRepository _userRepository;
	private readonly IUserSelfProfileInfoMapper _mapper;

	public GetUserByLoginAndPasswordCommand(
		IUserRepository userRepository,
		IUserSelfProfileInfoMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<OperationResultResponse<UserSelfProfileInfo>> ExecuteAsync(GetLoginRequest request)
	{
		var response = await _userRepository.GetUserByLoginAndPasswordAsync(request.Login, request.Password);
		if (response == null)
		{
			return new OperationResultResponse<UserSelfProfileInfo> { Errors = new List<string> { "User not found or invalid credentials." } };
		}

		UserSelfProfileInfo user = _mapper.Map(response);

		return new OperationResultResponse<UserSelfProfileInfo> { Body = user };
	}
}

