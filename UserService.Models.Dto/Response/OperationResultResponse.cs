using System.ComponentModel.DataAnnotations;

namespace UserService.Models.Dto.Response;

public class OperationResultResponse<T>
{
	public T Body { get; set; }

	[Required]
	public List<string> Errors { get; set; } = new List<string>();

	public OperationResultResponse(T body = default(T), List<string>? errors = null)
	{
		Body = body;
		Errors = errors ?? new List<string>();
	}
}
