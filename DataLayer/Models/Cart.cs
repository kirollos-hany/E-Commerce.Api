using MongoDB.Bson.Serialization.Attributes;

namespace E_Commerce.Api.DataLayer.Models;

public class Cart : BaseModel
{
    public Cart(IEnumerable<CartItem> items, string customerId)
    {
        Items = items;
        CustomerId = customerId;
        CalculatePrice();
    }

    private void CalculatePrice()
    {
        TotalPrice = Items.Select(i => i.Product.Price).Sum();
    }

    [BsonRequired] public IEnumerable<CartItem> Items { get;  set; }

    [BsonIgnore] public decimal TotalPrice { get;  set; }

    [BsonRequired] public string CustomerId { get;  set; }

    public void Update(IEnumerable<CartItem> items)
    {
        Items = items;
        CalculatePrice();
        SetUpdated();
    }
}