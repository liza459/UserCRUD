using System.ComponentModel.DataAnnotations;

namespace UserService.Models.Dto.Requests;

public class UpdatePasswordRequest
{
	[Required]
	[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Password must contain only Latin letters and numbers.")]
	public string NewPassword { get; set; }
}