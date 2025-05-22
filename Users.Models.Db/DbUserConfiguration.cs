using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Models.Db;

public class DbUserConfiguration : IEntityTypeConfiguration<DbUser>
{
	public void Configure(EntityTypeBuilder<DbUser> builder)
	{
		builder.ToTable(DbUser.TableName);
		builder.HasKey(u => u.Guid);
		builder.HasIndex(u => u.Login).IsUnique();
	}
}