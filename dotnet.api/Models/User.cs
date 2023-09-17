using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.api.Models;

[Table("users")]
public class User
{
	[Column("user_id")]
	public int Id {get; set;}
	[Column("email")]
	public string Email {get; set;} = string.Empty;
	[Column("password")]
	public string Password {get; set;} = string.Empty;
	[Column("role")]
	public RoleType Role {get; set;}
}

public enum RoleType{
	Admin,
	User
}