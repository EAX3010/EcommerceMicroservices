using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Exceptions
{
    internal class DomainException : Exception
    {
        public DomainException(string message) 
            : base($"Domain Exception: {message} throws from domain layer")
        {
        }
    }
}
