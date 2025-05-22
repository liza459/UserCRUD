using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using UserService.Business.User;
using UserService.Business.User.Interfaces;
using UserService.Data;
using UserService.Data.Interfaces;
using UserService.Data.Provider.Sql.Ef;
using UserService.Mappers.Db;
using UserService.Mappers.Db.Interfaces;
using UserService.Mappers.Models;
using UserService.Mappers.Models.Interfaces;

namespace UserCRUD;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(key),
					RoleClaimType = ClaimTypes.Role
				};
			});

		builder.Services.AddAuthorization();
		builder.Services.AddHttpContextAccessor();
		builder.Services.AddControllers();
		builder.Services.AddDbContext<UserServiceDbContext>(options =>
	        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));
		builder.Services.AddEndpointsApiExplorer();

		builder.Services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new() { Title = "UserService API", Version = "v1" });

			options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
				Scheme = "bearer",
				BearerFormat = "JWT",
				In = Microsoft.OpenApi.Models.ParameterLocation.Header,
				Description = "¬ведите JWT токен в формате: Bearer {токен}"
			});

			options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
			{
				{	
					new Microsoft.OpenApi.Models.OpenApiSecurityScheme
					{
						Reference = new Microsoft.OpenApi.Models.OpenApiReference
						{
							Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});
		});

		builder.Services.AddScoped<ICreateUserCommand, CreateUserCommand>();
		builder.Services.AddScoped<IUpdateUserInfoCommand, UpdateUserInfoCommand>();
		builder.Services.AddScoped<IUpdatePasswordCommand, UpdatePasswordCommand>();
		builder.Services.AddScoped<IUpdateLoginCommand, UpdateLoginCommand>();
		builder.Services.AddScoped<IGetAllActiveUsersCommand, GetAllActiveUsersCommand>();
		builder.Services.AddScoped<IGetUserByLoginCommand, GetUserByLoginCommand>();
		builder.Services.AddScoped<IGetUserByLoginAndPasswordCommand, GetUserByLoginAndPasswordCommand>();
		builder.Services.AddScoped<IGetUsersOlderThanCommand, GetUsersOlderThanCommand>();
		builder.Services.AddScoped<IDeleteUserCommand, DeleteUserCommand>();
		builder.Services.AddScoped<IRestoreUserCommand, RestoreUserCommand>();

		builder.Services.AddScoped<IUserRepository, UserRepository>();

		builder.Services.AddScoped<IUserInfoMapper, UserInfoMapper>();
		builder.Services.AddScoped<IAdminUserProfileInfoMapper, AdminUserProfileInfoMapper>();
		builder.Services.AddScoped<IUserSelfProfileInfoMapper, UserSelfProfileInfoMapper>();
		builder.Services.AddScoped<IDbUserMapper, DbUserMapper>();

		var app = builder.Build();

		using (var scope = app.Services.CreateScope())
		{
			var dbContext = scope.ServiceProvider.GetRequiredService<UserServiceDbContext>();
			dbContext.Database.Migrate();
		}

		UserSeeder.SeedInitialUser(app.Services);

		if (app.Environment.IsDevelopment())
        {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

        app.UseHttpsRedirection();

		app.UseAuthentication();

		app.UseAuthorization();

		app.MapControllers();

        app.Run();
    }
}
