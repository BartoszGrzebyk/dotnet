using AutoMapper;
using dotnet.api.Dtos.Users;
using dotnet.api.Models;
using dotnet.api.Repositories.Interfaces;
using dotnet.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.api.Controllers;

public class AuthController : BaseController
{
		private readonly IAuthService authService;

    public AuthController(IMapper mapper, IAuthService authService) : base(mapper)
    {
			this.authService = authService;
    }

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(RegisterDto registerDto)
		{
			try
			{
				var result = await authService.RegisterAsync(registerDto);

				var userDto = mapper.Map<UserDto>(result);

				return Ok(userDto);
			}
			catch (Exception error)
			{
				return BadRequest(error.Message);
			}
		}

		[HttpPost("login")]
		public async Task<ActionResult<User>> Login(LoginDto loginDto)
		{
			try
			{
				var result = await authService.LoginAsync(loginDto);

				return Ok(result);
			}
			catch (Exception error)
			{
				return BadRequest(error.Message);
			}
		}
}