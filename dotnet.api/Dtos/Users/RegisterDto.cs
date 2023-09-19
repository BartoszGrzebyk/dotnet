using System.ComponentModel.DataAnnotations;

namespace dotnet.api.Dtos.Users;

public class RegisterDto 
{
	[Required]
	[EmailAddress]
	public string Email {get; set;}
	[Required]
	public string Password {get; set;}
}