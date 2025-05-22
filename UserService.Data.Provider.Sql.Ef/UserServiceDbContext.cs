using Microsoft.EntityFrameworkCore;
using Users.Models.Db;

namespace UserService.Data.Provider.Sql.Ef;

public class UserServiceDbContext: DbContext
{
	public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
	{
	}

	public DbSet<DbUser> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new DbUserConfiguration());
		
	}
}
