using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Products.Presentation.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;
    protected readonly IMapper Mapper;

    protected ApiController(ISender sender, IMapper mapper) => (Sender, Mapper) = (sender, mapper);
}
