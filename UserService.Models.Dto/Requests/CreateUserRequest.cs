using System.ComponentModel.DataAnnotations;

namespace UserService.Models.Dto.Requests;

public class CreateUserRequest
{
	[Required(ErrorMessage = "Login is required.")]
	[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Login must contain only Latin letters and digits.")]
	public string Login { get; set; }

	[Required(ErrorMessage = "Password is required.")]
	[RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Password must contain only Latin letters and digits.")]
	public string Password { get; set; }

	[Required(ErrorMessage = "Name is required.")]
	[RegularExpression("^[a-zA-Zа-яА-ЯёЁ]+$", ErrorMessage = "Name must contain only Latin or Cyrillic letters.")]
	public string Name { get; set; }

	[Range(0, 2, ErrorMessage = "Gender must be 0 (female), 1 (male), or 2 (unknown).")]
	public int Gender { get; set; } = 2;

	public DateTime? Birthday { get; set; }

	public bool Admin { get; set; }
}
