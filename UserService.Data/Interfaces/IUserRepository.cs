using Users.Models.Db;

namespace UserService.Data.Interfaces;

public interface IUserRepository
{
	Task<bool> DoesUserExistAsync(Guid id);
	Task<DbUser> GetAsync(Guid guid);
	Task<Guid?> CreateAsync(DbUser dbUser);
	Task<bool> DoesExistLoginAsync(string login);
	Task<bool> UpdateUserAsync(DbUser dbUser);
	Task<bool> UpdatePasswordAsync(Guid guid, string newPassword);
	Task<bool> UpdateLoginAsync(Guid guid, string newLogin);
	Task<bool> SoftDeleteAsync(Guid guid, string revokedBy);
	Task<bool> HardDeleteAsync(Guid guid);
	Task<bool> RestoreUserAsync(Guid guid, string modifiedBy);
	Task<List<DbUser>> GetActiveUsersAsync();
	Task<DbUser?> GetUserByLoginAsync(string login);
	Task<DbUser?> GetUserByLoginAndPasswordAsync(string login, string password);
	Task<List<DbUser>> GetUsersByAgeAsync(int age);
}
