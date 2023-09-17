using AutoMapper;
using dotnet.api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.api.Controllers;

[Route("[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
		protected readonly IMapper mapper;

    public BaseController(IMapper mapper)
    {
			this.mapper = mapper;
    }
}