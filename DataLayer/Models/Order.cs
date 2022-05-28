using System.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace E_Commerce.Api.DataLayer.Models;

public class Order : BaseModel
{
    public Order(Cart customerCart, OrderStatuses status, PaymentMethods paymentMethod)
    {
        CustomerCart = customerCart;
        Status = status;
        PaymentMethod = paymentMethod;
    }

    [BsonRequired] public Cart CustomerCart { get; set; }

    [BsonRequired]
    [BsonRepresentation(BsonType.String)]
    public OrderStatuses Status { get; set; }

    [BsonRequired]
    [BsonRepresentation(BsonType.String)]
    public PaymentMethods PaymentMethod { get; set; }

    public void UpdateStatus(OrderStatuses status)
    {
        Status = status;
        SetUpdated();
    }
}