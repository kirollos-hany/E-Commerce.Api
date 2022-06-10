using AutoMapper;
using E_Commerce.Api.DataLayer.Database;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers;

[ApiController]
[Route("api/{controller}")]
public class CartController : ControllerBase
{
    private readonly IDbContext _dbContext;

    private readonly IMapper _mapper;

    public CartController(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
}