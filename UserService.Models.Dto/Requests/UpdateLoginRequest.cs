using System.ComponentModel.DataAnnotations;

namespace UserService.Models.Dto.Requests;

public class UpdateLoginRequest
{
	[Required]
	[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Login must contain only Latin letters and numbers.")]
	public string Login { get; set; }
}
