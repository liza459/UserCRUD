using System.ComponentModel.DataAnnotations;

namespace UserService.Models.Dto.Requests;

public class UpdateUserRequest
{
	[RegularExpression(@"^[a-zA-ZА-Яа-я]*$", ErrorMessage = "Name must contain only Latin or Russian letters.")]
	public string Name { get; set; }

	[Range(0, 2, ErrorMessage = "Gender must be 0 (female), 1 (male), or 2 (unknown).")]
	public int? Gender { get; set; }

	public DateTime? Birthday { get; set; }
}