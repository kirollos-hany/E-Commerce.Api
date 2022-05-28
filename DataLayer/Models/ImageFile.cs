namespace E_Commerce.Api.DataLayer.Models;

public class ImageFile : BaseModel
{
    public ImageFile(string url)
    {
        Url = url;
    }
    public string Url { get;  set; }
}