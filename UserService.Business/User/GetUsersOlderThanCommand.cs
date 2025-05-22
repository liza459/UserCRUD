using UserService.Business.User.Interfaces;
using UserService.Data.Interfaces;
using UserService.Mappers.Models.Interfaces;
using UserService.Models.Dto.Models;
using UserService.Models.Dto.Response;

namespace UserService.Business.User;

public class GetUsersOlderThanCommand : IGetUsersOlderThanCommand
{
	private readonly IUserRepository _userRepository;
	private readonly IUserInfoMapper _mapper;

	public GetUsersOlderThanCommand(
	  IUserRepository userRepository,
	  IUserInfoMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<OperationResultResponse<List<UserInfo>>> ExecuteAsync(int age)
	{
		var response = await _userRepository.GetUsersByAgeAsync(age);
		var users = _mapper.Map(response);

		return new OperationResultResponse<List<UserInfo>> { Body = users };
	}
}