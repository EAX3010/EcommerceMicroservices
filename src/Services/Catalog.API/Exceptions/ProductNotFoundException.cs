namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : Shared.Exceptions.NotFoundException
    {
        public ProductNotFoundException(string message) : base(message) { }
        public ProductNotFoundException(string name, object Id) : base($"Entity {name} {Id} was not Found") { }
    }
}
