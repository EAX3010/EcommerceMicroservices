using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace Contracts.CQRS
{
    public interface ICommand : IRequest<Unit>
    {

    }
    public interface ICommand<out TRespond> : IRequest<TRespond>
    {

    }
}
