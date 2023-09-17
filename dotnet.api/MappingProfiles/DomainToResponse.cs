using AutoMapper;
using dotnet.api.Dtos.Items;
using dotnet.api.Models;

namespace dotnet.api.MappingProfiles;

public class DomainToResponse : Profile
{
   public DomainToResponse()
	 {
			CreateMap<Item, ItemDto>();
	 }
}