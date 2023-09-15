using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.api.Data;

[Table("users")]
public class User
{
	[Column("user_id")]
	public int Id {get; set;}
	[Column("email")]
	public string Email {get; set;}
	[Column("password")]
	public string Password {get; set;}
}