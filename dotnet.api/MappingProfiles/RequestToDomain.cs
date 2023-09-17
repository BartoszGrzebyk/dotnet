using AutoMapper;
using dotnet.api.Dtos.Items;
using dotnet.api.Models;

namespace dotnet.api.MappingProfiles;

public class RequestToDomain : Profile
{
	public RequestToDomain()
	{
		CreateMap<CreateItemDto, Item>();
		CreateMap<UpdateItemDto, Item>();
	}
}