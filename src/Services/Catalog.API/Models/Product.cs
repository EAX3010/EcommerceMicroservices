namespace Catalog.API.Models;

public class Product
{
    //Mapping
    public Product()
    {
    }

    public Product(Guid _Id, string _Name, List<string> _Category, string _Description, string _ImageUrl,
        decimal _Price)
    {
        Id = _Id;
        Name = _Name;
        Category = _Category;
        Description = _Description;
        ImageUrl = _ImageUrl;
        Price = _Price;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Category { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
}