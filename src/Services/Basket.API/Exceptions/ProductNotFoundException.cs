using Shared.Exceptions;

namespace Basket.API.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string message) : base(message)
    {
    }

    public BasketNotFoundException(string name, object Id) : base($"Entity {name} {Id} was not Found")
    {
    }
}