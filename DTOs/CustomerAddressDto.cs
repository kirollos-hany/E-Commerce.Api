using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.DTOs;

public class CustomerAddressDto
{
    [Required] public string Country { get; set; } = string.Empty;

    [Required] public string City { get; set; } = string.Empty;

    [Required] public string State { get; set; } = string.Empty;

    [Required] public string Address { get; set; } = string.Empty;
}