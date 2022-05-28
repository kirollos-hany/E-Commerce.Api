namespace E_Commerce.Api.DataLayer.Models;

public class ImageFile : BaseModel
{
    public ImageFile(string path)
    {
        Path = path;
    }
    public string Path { get;  set; }
}