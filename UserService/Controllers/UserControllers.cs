using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Business.User.Interfaces;
using UserService.Models.Dto.Requests;

namespace UserService.Controllers;

[Route("userService")]
[ApiController]
public class UserControllers: ControllerBase
{
	[Authorize(Roles = "Admin")]
	[HttpPost("create")]
	public async Task<IActionResult> CreateAsync(
	 [FromServices] ICreateUserCommand command,
	 [FromBody] CreateUserRequest request)
	{
		string createdBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		var response = await command.ExecuteAsync(request, createdBy);

		if (response.Errors.Any())
		{
			return BadRequest(response.Errors);
		}

		return Ok(response.Body);
	}

	[Authorize]
	[HttpPatch("update/userInfo")]
	public async Task<IActionResult> UpdateUserAsync(
		[FromServices] IUpdateUserInfoCommand command,
		[FromQuery] Guid guid,
		[FromBody] UpdateUserRequest request)
	{
		string modifiedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (User.Identity.Name != guid.ToString() && !User.IsInRole("Admin"))
		{
			return Unauthorized("You do not have permission to update this user.");
		}

 		var result = await command.ExecuteAsync(guid, request, modifiedBy);

		if (result.Errors.Any())
		{
			return BadRequest(result.Errors);
		}

		return Ok(result.Body);
	}

	[Authorize]
	[HttpPut("update/password")]
	public async Task<IActionResult> UpdatePasswordAsync(
		[FromServices] IUpdatePasswordCommand command,
		[FromQuery] Guid guid,
		[FromBody] UpdatePasswordRequest request)
	{
		string modifiedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (User.Identity.Name != guid.ToString() && !User.IsInRole("Admin"))
		{
			return Unauthorized("You do not have permission to update this user.");
		}

		var result = await command.ExecuteAsync(guid, request, modifiedBy);

		if (result.Errors.Any())
		{
			return BadRequest(result.Errors);
		}

		return Ok("Password updated successfully.");
	}

	[Authorize]
	[HttpPut("update/login")]
	public async Task<IActionResult> UpdateLoginAsync(
		[FromServices] IUpdateLoginCommand command,
		[FromQuery] Guid guid,
		[FromBody] UpdateLoginRequest request)
	{
		string modifiedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (User.Identity.Name != guid.ToString() && !User.IsInRole("Admin"))
		{
			return Unauthorized("You do not have permission to update this user.");
		}

		var result = await command.ExecuteAsync(guid, request, modifiedBy);

		if (result.Errors.Any())
		{
			return BadRequest(result.Errors);
		}

		return Ok("Login updated successfully.");
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("get/allActiveUsers")]
	public async Task<IActionResult> GetAllActiveUsersAsync(
		[FromServices] IGetAllActiveUsersCommand command)
	{
		var result = await command.ExecuteAsync();

		if (result.Errors.Any())
		{
			return BadRequest(result.Errors);
		}

		return Ok(result);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("get/login")]
	public async Task<IActionResult> GetUserByLoginAsync(
		[FromServices] IGetUserByLoginCommand command,
		[FromQuery] string login)
	{
		var result = await command.ExecuteAsync(login);

		if (result.Errors.Any())
		{
			return BadRequest(result.Errors);
		}

		return Ok(result);
	}

	[Authorize]
	[HttpGet("get/byLoginAndPassword")]
	public async Task<IActionResult> GetUserByLoginAndPasswordAsync(
		[FromServices] IGetUserByLoginAndPasswordCommand command,
		[FromQuery] GetLoginRequest request)
	{
		if (User.Identity.Name != request.Login)
		{
			return Unauthorized("You do not have the rights to receive this user's data.");
		}

		var result = await command.ExecuteAsync(request);

		if (result.Errors.Any())
		{
			return BadRequest(result.Errors);
		}

		return Ok(result);
	}

	[Authorize(Roles = "Admin")]
	[HttpGet("get/usersOlderThan")]
	public async Task<IActionResult> GetUsersOlderThanAsync(
		[FromServices] IGetUsersOlderThanCommand command,
		[FromQuery] int age)
	{
		var result = await command.ExecuteAsync(age);

		if (result.Errors.Any())
		{
			return BadRequest(result.Errors);
		}

		return Ok(result);
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete("delete")]
	public async Task<IActionResult> DeleteUserAsync(
		[FromServices] IDeleteUserCommand command,
		[FromQuery] Guid guid, bool deleteHard)
	{
		string revokedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		var result = await command.ExecuteAsync(guid, deleteHard, revokedBy);

		if (result.Errors.Any())
		{
			return BadRequest(result.Errors);
		}

		return Ok("User soft deleted successfully.");
	}

	[Authorize(Roles = "Admin")]
	[HttpPut("restore")]
	public async Task<IActionResult> RestoreUserAsync(
		[FromServices] IRestoreUserCommand command,
		[FromQuery] Guid guid)
	{
		string modifiedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		var result = await command.ExecuteAsync(guid, modifiedBy);

		if (result.Errors.Any())
		{
			return BadRequest(result.Errors);
		}

		return Ok("User restored successfully.");
	}
}
