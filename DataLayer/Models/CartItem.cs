namespace E_Commerce.Api.DataLayer.Models;

public class CartItem : BaseModel
{
    public CartItem(Product product, uint quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get;  set; }

    public uint Quantity { get;  set; }

    public void UpdateQty(uint quantity)
    {
        Quantity = quantity;
        SetUpdated();
    }
}