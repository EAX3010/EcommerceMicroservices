using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CQRS
{
    public interface IQuery<out TRespond> : IRequest<TRespond> 
        where TRespond : notnull
    {
    }
}
