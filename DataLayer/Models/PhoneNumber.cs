namespace E_Commerce.Api.DataLayer.Models;

public class PhoneNumber
{
    public PhoneNumber(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
    }

    public string CountryCode { get;  set; }

    public string Number { get;  set; }

    public void Update(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
    }

    public override string ToString()
    {
        return $"{CountryCode}-{Number}";
    }
}