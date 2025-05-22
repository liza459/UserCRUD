using Microsoft.Extensions.DependencyInjection;
using Users.Models.Db;

namespace UserService.Data.Provider.Sql.Ef;

public class UserSeeder
{
	public static void SeedInitialUser(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<UserServiceDbContext>();

		if (!dbContext.Users.Any())
		{
			var user = new DbUser
			{
				Guid = Guid.NewGuid(),
				Login = "admin",
				Password = "admin",
				Name = "Admin",
				Gender = 2,
				Birthday = null,
				Admin = true,
				CreatedOn = DateTime.UtcNow,
				CreatedBy = "system",
				ModifiedOn = null,
				ModifiedBy = "",
				RevokedOn = null,
				RevokedBy = ""
			};

			dbContext.Users.Add(user);
			dbContext.SaveChanges();
		}
	}
}