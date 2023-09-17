using System.ComponentModel.DataAnnotations;

namespace dotnet.api.Dtos.Items;

public class CreateItemDto 
{
	[Required]
	public string Name {get; set;}
	[Required]
	public string Description {get; set;}
}