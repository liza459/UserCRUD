using UserService.Business.User.Interfaces;
using UserService.Data.Interfaces;
using UserService.Mappers.Db.Interfaces;
using UserService.Models.Dto.Requests;
using UserService.Models.Dto.Response;

namespace UserService.Business.User;

public class CreateUserCommand : ICreateUserCommand
{
	private readonly IDbUserMapper _mapper;
	private readonly IUserRepository _userRepository;

	public CreateUserCommand(
	  IDbUserMapper mapper,
	  IUserRepository userRepository)
	{
		_mapper = mapper;
		_userRepository = userRepository;
	}

	public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateUserRequest request, string createdBy)
	{
		OperationResultResponse<Guid?> response = new();

		if (request.Birthday.HasValue)
		{
			request.Birthday = DateTime.SpecifyKind(request.Birthday.Value, DateTimeKind.Utc);
		}

		if (await _userRepository.DoesExistLoginAsync(request.Login))
		{
			return new OperationResultResponse<Guid?> { Errors = new List<string> { "Login is already taken." } };
		}

		response.Body = await _userRepository.CreateAsync(_mapper.Map(request, createdBy));

		if (response.Body is null)
		{
			response.Errors = new List<string> { "User with this login already exists." };
		}

		return response;
	}
}
