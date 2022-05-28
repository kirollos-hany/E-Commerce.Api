namespace E_Commerce.Api.DataLayer.Models;

public class CustomerAddress
{
    public CustomerAddress(string country, string city, string state, string address)
    {
        Country = country;
        City = city;
        State = state;
        Address = address;
    }

    public string Country { get;  set; }

    public string City { get;  set; }

    public string State { get;  set; }

    public string Address { get;  set; }

    public void Update(string country, string city, string state, string address)
    {
        Country = country;
        City = city;
        State = state;
        Address = address;
    }
}