using System.ComponentModel.DataAnnotations;

namespace dotnet.api.Dtos.Items;

public class UpdateItemDto 
{
	[Range(1, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
	public int Id {get; set;}
	[Required]
	public string Name {get; set;}
	[Required]
	public string Description {get; set;}
}