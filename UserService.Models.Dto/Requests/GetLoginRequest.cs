using System.ComponentModel;

namespace UserService.Models.Dto.Requests;

public class GetLoginRequest
{
	[DefaultValue("admin")]
	public string Login { get; set; }

	[DefaultValue("admin")]
	public string Password { get; set; }
}
