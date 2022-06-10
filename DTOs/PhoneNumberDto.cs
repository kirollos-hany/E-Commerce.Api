using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.DTOs;

public class PhoneNumberDto
{
    [MaxLength(3, ErrorMessage = "Country code can't exceed 3 characters")]
    public string CountryCode { get;  set; }
    
    [MinLength(11, ErrorMessage = "Number can't exceed 11 digits")]
    public string Number { get;  set; }
}