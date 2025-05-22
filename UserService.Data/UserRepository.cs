using Microsoft.EntityFrameworkCore;
using Users.Models.Db;
using UserService.Data.Interfaces;
using UserService.Data.Provider.Sql.Ef;

namespace UserService.Data;

public class UserRepository: IUserRepository
{
	private readonly UserServiceDbContext _provider;
	public UserRepository(UserServiceDbContext provider)
	{
		_provider = provider;
	}

	public async Task<bool> DoesUserExistAsync(Guid guid)
	{
		return await _provider.Users.AnyAsync(u => u.RevokedOn == null && u.Guid == guid);
	}

	public async Task<DbUser?> GetAsync(Guid guid)
	{
		return await _provider.Users.FirstOrDefaultAsync(u => u.RevokedOn == null && u.Guid == guid);
	}

	public async Task<Guid?> CreateAsync(DbUser dbUser)
	{
		await _provider.Users.AddAsync(dbUser);
		await _provider.SaveChangesAsync();

		return dbUser.Guid;
	}

	public async Task<bool> DoesExistLoginAsync(string login)
	{
		return await _provider.Users.AnyAsync(u => u.RevokedOn == null && u.Login == login.Trim());
	}

	public async Task<bool> UpdateUserAsync(DbUser dbUser)
	{
		var existingUser = await _provider.Users.FirstOrDefaultAsync(u => u.Guid == dbUser.Guid && u.RevokedOn == null);
		if (existingUser == null)
			return false;

		existingUser.Name = dbUser.Name.Trim();
		existingUser.Gender = dbUser.Gender;
		existingUser.Birthday = dbUser.Birthday;

		await _provider.SaveChangesAsync();

		return true;
	}

	public async Task<bool> UpdatePasswordAsync(Guid guid, string newPassword)
	{
		var user = await _provider.Users.FirstOrDefaultAsync(u => u.Guid == guid && u.RevokedOn == null);
		if (user == null)
			return false;

		user.Password = newPassword;
		await _provider.SaveChangesAsync();

		return true;
	}

	public async Task<bool> UpdateLoginAsync(Guid guid, string newLogin)
	{
		var user = await _provider.Users.FirstOrDefaultAsync(u => u.Guid == guid && u.RevokedOn == null);
		if (user == null || await DoesExistLoginAsync(newLogin))
			return false;

		user.Login = newLogin.Trim();
		await _provider.SaveChangesAsync();

		return true;
	}

	public async Task<bool> SoftDeleteAsync(Guid guid, string revokedBy)
	{
		var user = await _provider.Users.FirstOrDefaultAsync(u => u.Guid == guid && u.RevokedOn == null);
		if (user == null)
			return false;

		user.RevokedOn = DateTime.UtcNow;
		user.RevokedBy = revokedBy;

		await _provider.SaveChangesAsync();

		return true;
	}

	public async Task<bool> HardDeleteAsync(Guid guid)
	{
		var user = await _provider.Users.FirstOrDefaultAsync(u => u.Guid == guid);
		if (user == null)
			return false;

		_provider.Users.Remove(user);
		await _provider.SaveChangesAsync();

		return true;
	}

	public async Task<bool> RestoreUserAsync(Guid guid, string modifiedBy)
	{
		var user = await _provider.Users.FirstOrDefaultAsync(u => u.Guid == guid);
		if (user == null)
			return false;

		user.ModifiedOn = DateTime.UtcNow;
		user.ModifiedBy = modifiedBy;
		user.RevokedOn = null;
		user.RevokedBy = string.Empty;

		await _provider.SaveChangesAsync();

		return true;
	}

	public async Task<List<DbUser>> GetActiveUsersAsync()
	{
		return await _provider.Users.Where(u => u.RevokedOn == null).OrderByDescending(u => u.CreatedOn).ToListAsync();
	}

	public async Task<DbUser?> GetUserByLoginAsync(string login)
	{
		return await _provider.Users.FirstOrDefaultAsync(u => u.RevokedOn == null && u.Login == login.Trim());
	}

	public async Task<DbUser?> GetUserByLoginAndPasswordAsync(string login, string password)
	{
		return await _provider.Users.FirstOrDefaultAsync(u => u.RevokedOn == null && u.Login == login.Trim() && u.Password == password);
	}

	public async Task<List<DbUser?>> GetUsersByAgeAsync(int age)
	{
		var dateThreshold = DateTime.UtcNow.AddYears(-age);
		return await _provider.Users.Where(u => u.RevokedOn == null && u.Birthday <= dateThreshold).ToListAsync();
	}
}