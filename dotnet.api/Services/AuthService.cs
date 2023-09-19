using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dotnet.api.Dtos.Users;
using dotnet.api.Models;
using dotnet.api.Repositories.Interfaces;
using dotnet.api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace dotnet.api.Services;

public class AuthService : IAuthService
{
		private readonly IUserRepository userRepository;
		private readonly IConfiguration configuration;

		public AuthService(IUserRepository userRepository, IConfiguration configuration)
		{
			this.userRepository = userRepository;
			this.configuration = configuration;
		}

    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await userRepository.GetUserByEmailAsync(loginDto.Email);
        if(user is null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) throw new Exception("Wrong credentials");

				string token = CreateToken(user);

				return token;
    }

    public async Task<User> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await userRepository.GetUserByEmailAsync(registerDto.Email);
        if(existingUser is not null) throw new Exception("Email in use");

				string passHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

				var user = new User {
					Email = registerDto.Email,
					Password = passHash,
				};
        
        return await userRepository.AddUserAsync(user);
    }

		public string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim> {
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, "Admin")
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				configuration.GetSection("AppSettings:Token").Value!
			));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: credentials
			);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
}