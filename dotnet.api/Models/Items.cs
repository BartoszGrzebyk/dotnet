using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet.api.Models;

[Table("items")]
public class Item
{
	[Column("item_id")]
	public int Id {get; set;}
	[Column("name")]
	public string Name {get; set;} = string.Empty;
	[Column("description")]
	public string Description {get; set;} = string.Empty;
}