namespace UserService.Models.Dto.Models;

public class AdminUserProfileInfo
{
	public Guid Guid { get; set; }
	public string Login { get; set; }
	public string Name { get; set; }
	public int Gender { get; set; }
	public DateTime? Birthday { get; set; }
	public bool IsActiv { get; set; }
}
