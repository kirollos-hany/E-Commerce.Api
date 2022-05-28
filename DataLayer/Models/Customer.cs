using MongoDB.Bson.Serialization.Attributes;

namespace E_Commerce.Api.DataLayer.Models;

public class Customer : BaseModel
{
    public Customer(string firstName, string lastName, string email, PhoneNumber phoneNumber, ImageFile image,
        DateTime dateOfBirth, CustomerAddress address)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Image = image;
        DateOfBirth = dateOfBirth;
        Address = address;
    }

    [BsonRequired] public string FirstName { get;  set; }
    [BsonRequired] public string LastName { get;  set; }
    [BsonRequired] public string Email { get;  set; }
    [BsonRequired] public PhoneNumber PhoneNumber { get;  set; }
    [BsonRequired] public ImageFile Image { get;  set; }
    [BsonRequired] public DateTime DateOfBirth { get;  set; }

    [BsonRequired] public CustomerAddress Address { get;  set; }

    public void Update(string firstName, string lastName, string email, PhoneNumber phoneNumber, ImageFile image,
        DateTime dateOfBirth, CustomerAddress address)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Image = image;
        DateOfBirth = dateOfBirth;
        Address = address;
        SetUpdated();
    }
}