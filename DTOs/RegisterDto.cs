using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.DTOs;

public class RegisterDto
{
    [Required] public string FirstName { get; set; } = string.Empty;
    [Required] public string LastName { get; set; } = string.Empty;
    [EmailAddress] [Required] public string Email { get; set; } = string.Empty;
    [Required] public PhoneNumberDto PhoneNumber { get; set; } = new();

    public string ImageId { get; set; } = string.Empty;
    [Required] public DateOnly DateOfBirth { get; set; }
    [Required] public CustomerAddressDto Address { get; set; } = new();
    [Required] public string Password { get; set; } = string.Empty;
    [Required] [Compare(nameof(Password))] public string ConfirmPassword { get; set; } = string.Empty;
    [Required] public string UserName { get; set; } = string.Empty;
}