#region

using Shared.Exceptions;

#endregion

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(string message) : base(message)
        {
        }

        public ProductNotFoundException(string name, object Id) : base($"Entity {name} {Id} was not Found")
        {
        }
    }
}