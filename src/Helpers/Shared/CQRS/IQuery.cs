#region

using MediatR;

#endregion

namespace Shared.CQRS
{
    public interface IQuery<out TRespond> : IRequest<TRespond>
        where TRespond : notnull
    {
    }
}