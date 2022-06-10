using AspNetCore.Identity.MongoDbCore.Models;
using AutoMapper;
using E_Commerce.Api.DataLayer.Models;
using E_Commerce.Api.DTOs;

namespace E_Commerce.Api.Profiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<PhoneNumberDto, PhoneNumber>();
        CreateMap<CustomerAddressDto, CustomerAddress>();
        CreateMap<RegisterDto, MongoIdentityUser<string>>();
    }
}