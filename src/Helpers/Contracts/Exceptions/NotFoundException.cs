namespace Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string name, object Id) : base($"Entity {name} {Id} was not Found") { }
    }

}
