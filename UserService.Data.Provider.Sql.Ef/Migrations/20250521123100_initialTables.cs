using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Users.Models.Db;

namespace UserService.Data.Provider.Sql.Ef.Migrations;

[DbContext(typeof(UserServiceDbContext))]
[Migration("20250521123100_initialTables")]

public class InitialTables : Migration
{
	private void CreateUsersTable(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: DbUser.TableName,
			columns: table => new
			{
				Guid = table.Column<Guid>(nullable: false),
				Login = table.Column<string>(nullable: false),
				Password = table.Column<string>(nullable: false),
				Name = table.Column<string>(nullable:false),
				Gender = table.Column<int>(nullable:false),
				Birthday = table.Column<DateTime>(nullable: true),
				Admin = table.Column<bool>(nullable: false),
				CreatedOn = table.Column<DateTime>(nullable: false),
				CreatedBy = table.Column<string>(nullable:false),
				ModifiedOn = table.Column<DateTime>(nullable: true),
				ModifiedBy = table.Column<string>(nullable: true),
				RevokedOn = table.Column<DateTime>(nullable: true),
				RevokedBy = table.Column<string>(nullable: true),
			},
			constraints: table =>
			{
				table.PrimaryKey($"PK_{DbUser.TableName}", u => u.Guid);
				table.UniqueConstraint($"UX_{DbUser.TableName}_Login_unique", x => x.Login);
			});
	}

	protected override void Up(MigrationBuilder migrationBuilder)
	{
		CreateUsersTable(migrationBuilder);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(DbUser.TableName);
	}
}
