using AutoMapper;
using E_Commerce.Api.DataLayer.Models;
using E_Commerce.Api.DTOs;

namespace E_Commerce.Api.Profiles;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ProductReadDto>();
    }
}