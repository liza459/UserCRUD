using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Data.Provider.Sql.Ef;
using UserService.Models.Dto.Requests;

namespace UserService.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
	private readonly UserServiceDbContext _db;
	private readonly IConfiguration _config;

	public AuthController(UserServiceDbContext db, IConfiguration config)
	{
		_db = db;
		_config = config;
	}

	[HttpPost("login")]
	public IActionResult Login([FromBody] GetLoginRequest request)
	{
		var user = _db.Users.FirstOrDefault(u => u.Login == request.Login && u.RevokedOn == null);

		if (user == null || user.Password != request.Password)
			return Unauthorized("Invalid login or password");

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Guid.ToString()),
			new Claim(ClaimTypes.Name, user.Login),
			new Claim(ClaimTypes.Role, user.Admin ? "Admin" : "User")
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _config["Jwt:Issuer"],
			audience: _config["Jwt:Audience"],
			claims: claims,
			expires: DateTime.UtcNow.AddHours(2),
			signingCredentials: creds);

		return Ok(new
		{
			token = new JwtSecurityTokenHandler().WriteToken(token)
		});
	}
}